using ZakusochnayaModel;
using ZakusochnayaServiceDAL.BindingModels;
using ZakusochnayaServiceDAL.Interfaces;
using ZakusochnayaServiceDAL.ViewModels;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.SqlServer;
using System.Linq;
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
                OutputName = rec.Output.OutputName
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
            context.Zakazs.Add(new Zakaz
            {
                PokupatelId = model.PokupatelId,
                OutputId = model.OutputId,
                DateCreate = DateTime.Now,
                Number = model.Number,
                Summa = model.Summa,
                Status = ZakazStatus.Принят
            });
            context.SaveChanges();
        }
        public void TakeOrderInWork(ZakazBindingModel model)
        {
            using (var transaction = context.Database.BeginTransaction())
            {
                try
                {
                    Zakaz element = context.Zakazs.FirstOrDefault(rec => rec.Id ==
                    model.Id);
                    if (element == null)
                    {
                        throw new Exception("Элемент не найден");
                    }
                    if (element.Status != ZakazStatus.Принят)
                    {
                        throw new Exception("Заказ не в статусе \"Принят\"");
                    }
                    var outputElements = context.OutputElements.Include(rec =>
                    rec.Element).Where(rec => rec.OutputId == element.OutputId);
                    // списываем
                    foreach (var outputElement in outputElements)
                    {
                        int countOnStocks = outputElement.Number * element.Number;
                        var skladElements = context.SkladElements.Where(rec =>
                        rec.ElementId == outputElement.ElementId);
                        foreach (var skladElement in skladElements)
                        {
                            // компонентов на одном слкаде может не хватать
                            if (skladElement.Number >= countOnStocks)
                            {
                                skladElement.Number -= countOnStocks;
                                countOnStocks = 0;
                                context.SaveChanges();
                                break;
                            }
                            else
                            {
                                countOnStocks -= skladElement.Number;
                                skladElement.Number = 0;
                                context.SaveChanges();
                            }
                        }
                        if (countOnStocks > 0)
                        {
                            throw new Exception("Не достаточно компонента " +
                            outputElement.Element.ElementName + " требуется " + outputElement.Number + ", не хватает " + countOnStocks);
                        }
                    }
                    element.DateImplement = DateTime.Now;
                    element.Status = ZakazStatus.Выполняется;
                    context.SaveChanges();
                    transaction.Commit();
                }
                catch (Exception)
                {
                    transaction.Rollback();
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
        }
        public void PutComponentOnStock(SkladElementBindingModel model)
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
    }
}
