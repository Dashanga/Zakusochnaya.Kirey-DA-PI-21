using ZakusochnayaModel;
using ZakusochnayaServiceDAL.BindingModels;
using ZakusochnayaServiceDAL.Interfaces;
using ZakusochnayaServiceDAL.ViewModels;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.Entity;
using System.Data.Entity.SqlServer;
using System.Linq;
using System.Net;
using System.Net.Mail;
using ZakusochnayaServiceDAL.BindingModel;

namespace ZakusochnayaServiceImplementDataBase.Implementations
{
    public class MainServiceDB : IMainService
    {
        private ZakusochnayaDbContext context;
        public MainServiceDB(ZakusochnayaDbContext context)
        {
            this.context = context;
        }
        public List<ZakazViewModel> GetList()
        {
            List<ZakazViewModel> result = context.Zakazs.Select(rec => new ZakazViewModel
            {
                Id = rec.Id,
                PokupatelId = rec.PokupatelId,
                OutputId = rec.OutputId,
                ExecutorId = rec.ExecutorId,
                DateCreate = SqlFunctions.DateName("dd", rec.DateCreate) + " " +
            SqlFunctions.DateName("mm", rec.DateCreate) + " " +
            SqlFunctions.DateName("yyyy", rec.DateCreate),
                DateImplement = rec.DateImplement == null ? "" :
            SqlFunctions.DateName("dd",
           rec.DateImplement.Value) + " " +
            SqlFunctions.DateName("mm",
           rec.DateImplement.Value) + " " +
            SqlFunctions.DateName("yyyy",
           rec.DateImplement.Value),
                Status = rec.Status.ToString(),
                Number = rec.Number,
                Summa = rec.Summa,
                PokupatelFIO = rec.Pokupatel.PokupatelFIO,
                OutputName = rec.Output.OutputName,
                ExecutorName = rec.Executor.ExecutorFIO
            })
            .ToList();
            return result;
        }
        public List<ZakazViewModel> GetFreeOrders()
        {
            List<ZakazViewModel> result = context.Zakazs
            .Where(x => x.Status == ZakazStatus.Принят || x.Status ==
           ZakazStatus.НедостаточноРесурсов)
            .Select(rec => new ZakazViewModel
            {
                Id = rec.Id
            })
            .ToList();
            return result;
        }
        public void CreateOrder(ZakazBindingModel model)
        {
            var order = new Zakaz
            {
                PokupatelId = model.PokupatelId,
                OutputId = model.OutputId,
                ExecutorId = model.ExecutorId,
                DateCreate = DateTime.Now,
                Number = model.Number,
                Summa = model.Summa,
                Status = ZakazStatus.Принят
            };
            context.Zakazs.Add(order);
            context.SaveChanges();
            var client = context.Pokupatels.FirstOrDefault(x => x.Id == model.PokupatelId);
            //throw new Exception(client.Mail);
            SendEmail(client.Mail, "Оповещение по заказам", string.Format("Заказ №{0} от {1} создан успешно", order.Id, order.DateCreate.ToShortDateString()));
            
        }
        public void TakeOrderInWork(ZakazBindingModel model)
        {
            using (var transaction = context.Database.BeginTransaction())
            {
                Zakaz element = context.Zakazs.FirstOrDefault(rec => rec.Id == model.Id);
                try
                {
                    if (element == null)
                    {
                        throw new Exception("Элемент не найден");
                    }
                    if (element.Status != ZakazStatus.Принят && element.Status !=
                    ZakazStatus.НедостаточноРесурсов)
                    {
                        throw new Exception("Заказ не в статусе \"Принят\"");
                    }
                    var productComponents = context.OutputElements.Include(rec =>
                    rec.Element).Where(rec => rec.OutputId == element.OutputId);
                    // списываем
                    foreach (var productComponent in productComponents)
                    {
                        int countOnStocks = productComponent.Number * element.Number;
                        var stockComponents = context.SkladElements.Where(rec =>
                        rec.ElementId == productComponent.ElementId);
                        foreach (var stockComponent in stockComponents)
                        {
                            // компонентов на одном слкаде может не хватать
                            if (stockComponent.Number >= countOnStocks)
                            {
                                stockComponent.Number -= countOnStocks;
                                countOnStocks = 0;
                                context.SaveChanges();
                                break;
                            }
                            else
                            {
                                countOnStocks -= stockComponent.Number;
                                stockComponent.Number = 0;
                                context.SaveChanges();
                            }
                        }
                        if (countOnStocks > 0)
                        {
                            throw new Exception("Не достаточно компонента " +
                           productComponent.Element.ElementName + " требуется " + productComponent.Number + ", не хватает " + countOnStocks);
                         }
                    }
                    element.ExecutorId = model.ExecutorId;
                    element.DateImplement = DateTime.Now;
                    element.Status = ZakazStatus.Выполняется;
                    context.SaveChanges();
                    SendEmail(element.Pokupatel.Mail, "Оповещение по заказам",
                    string.Format("Заказ №{0} от {1} передеан в работу", element.Id,
                    element.DateCreate.ToShortDateString()));
                    transaction.Commit();
                }
                catch (Exception)
                {
                    transaction.Rollback();
                    element.Status = ZakazStatus.НедостаточноРесурсов;
                    context.SaveChanges();
                    transaction.Commit();
                    throw;
                }
            }
        }
        public void FinishOrder(ZakazBindingModel model)
        {
            Zakaz element = context.Zakazs.FirstOrDefault(rec => rec.Id == model.Id);
            if (element == null)
            {
                throw new Exception("Элемент не найден");
            }
            if (element.Status != ZakazStatus.Выполняется)
            {
                throw new Exception("Заказ не в статусе \"Выполняется\"");
            }
            element.Status = ZakazStatus.Готов;
            context.SaveChanges();
            SendEmail(element.Pokupatel.Mail, "Оповещение по заказам", string.Format("Заказ №{ 0} от { 1} передан на оплату", element.Id, element.DateCreate.ToShortDateString()));
        }
        public void PayOrder(ZakazBindingModel model)
        {
            Zakaz element = context.Zakazs.FirstOrDefault(rec => rec.Id == model.Id);
            if (element == null)
            {
                throw new Exception("Элемент не найден");
            }
            if (element.Status != ZakazStatus.Готов)
            {
                throw new Exception("Заказ не в статусе \"Готов\"");
            }
            element.Status = ZakazStatus.Оплачен;
            context.SaveChanges();
            SendEmail(element.Pokupatel.Mail, "Оповещение по заказам", string.Format("Заказ №{ 0} от { 1} оплачен успешно", element.Id, element.DateCreate.ToShortDateString()));
        }
        public void PutComponentOnSklad(SkladElementBindingModel model)
        {
            SkladElement element = context.SkladElements.FirstOrDefault(rec =>
           rec.SkladId == model.SkladId && rec.ElementId == model.ElementId);
            if (element != null)
            {
                element.Number += model.Number;
            }
            else
            {
                context.SkladElements.Add(new SkladElement
                {
                    SkladId = model.SkladId,
                    ElementId = model.ElementId,
                    Number = model.Number
                });
            }
            context.SaveChanges();
        }
        private void SendEmail(string mailAddress, string subject, string text)
        {
            MailMessage objMailMessage = new MailMessage();
            SmtpClient objSmtpClient = null;
            try
            {
                objMailMessage.From = new
                MailAddress(ConfigurationManager.AppSettings["MailLogin"]);
                objMailMessage.To.Add(new MailAddress(mailAddress));
                objMailMessage.Subject = subject;
                objMailMessage.Body = text;
                objMailMessage.SubjectEncoding = System.Text.Encoding.UTF8;
                objMailMessage.BodyEncoding = System.Text.Encoding.UTF8;
                objSmtpClient = new SmtpClient("smtp.gmail.com", 587);
                objSmtpClient.UseDefaultCredentials = false;
                objSmtpClient.EnableSsl = true;
                objSmtpClient.DeliveryMethod = SmtpDeliveryMethod.Network;
                objSmtpClient.Credentials = new
               NetworkCredential(ConfigurationManager.AppSettings["MailLogin"],
               ConfigurationManager.AppSettings["MailPassword"]);
                objSmtpClient.Send(objMailMessage);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                objMailMessage = null;
                objSmtpClient = null;
            }
        }
    }
}