using ZakusochnayaModel;
using ZakusochnayaServiceDAL.Interfaces;
using System;
using System.Collections.Generic;
using ZakusochnayaServiceDAL.BindingModels;
using ZakusochnayaServiceDAL.ViewModels;
using System.Linq;
using ZakusochnayaServiceDAL.BindingModel;

namespace ZakusochnayaServiceImplementList.Implementations
{
    public class MainServiceList : IMainService
    {
        private DataListSingleton source;
        public MainServiceList()
        {
            source = DataListSingleton.GetInstance();
        }
        public List<ZakazViewModel> GetList()
        {
            List<ZakazViewModel> result = source.Zakazs
                .Select(rec => new ZakazViewModel
                {
                    Id = rec.Id,
                    PokupatelId = rec.PokupatelId,
                    OutputId = rec.OutputId,
                    DateCreate = rec.DateCreate.ToLongDateString(),
                    DateImplement = rec.DateImplement?.ToLongDateString(),
                    Status = rec.Status.ToString(),
                    Number = rec.Number,
                    Summa = rec.Summa,
                    PokupatelFIO = source.Pokupatels.FirstOrDefault(recC => recC.Id ==
    rec.PokupatelId)?.PokupatelFIO,
                    OutputName = source.Outputs.FirstOrDefault(recP => recP.Id ==
    rec.OutputId)?.OutputName,
                })
                .ToList();
            return result;
        }
        public void CreateOrder(ZakazBindingModel model)
        {
            int maxId = source.Zakazs.Count > 0 ? source.Zakazs.Max(rec => rec.Id) : 0;
            source.Zakazs.Add(new Zakaz
            {
                Id = maxId + 1,
                PokupatelId = model.PokupatelId,
                OutputId = model.OutputId,
                DateCreate = DateTime.Now,
                Number = model.Number,
                Summa = model.Summa,
                Status = ZakazStatus.Принят
            });
        }
        public void TakeOrderInWork(ZakazBindingModel model)
        {
            Zakaz element = source.Zakazs.FirstOrDefault(rec => rec.Id == model.Id);
            if (element == null)
            {
                throw new Exception("Элемент не найден");
            }
            if (element.Status != ZakazStatus.Принят)
            {
                throw new Exception("Заказ не в статусе \"Принят\"");
            }
            // смотрим по количеству компонентов на складах
            var outputElements = source.OutputElements.Where(rec => rec.OutputId
            == element.OutputId);
            foreach (var outputElement in outputElements)
            {
                int countOnStocks = source.SkladElements
                .Where(rec => rec.ElementId ==
                outputElement.ElementId)
                .Sum(rec => rec.Number);
                if (countOnStocks < outputElement.Number * element.Number)
                {
                    var elementName = source.Elements.FirstOrDefault(rec => rec.Id ==
                    outputElement.ElementId);
                    throw new Exception("Не достаточно компонента " +
                    elementName?.ElementName + " требуется " + (outputElement.Number * element.Number) +
                    ", в наличии " + countOnStocks);
                }
            }
            // списываем
            foreach (var outputElement in outputElements)
            {
                int countOnStocks = outputElement.Number * element.Number;
                var skladElements = source.SkladElements.Where(rec => rec.ElementId
                == outputElement.ElementId);
                foreach (var skladElement in skladElements)
                {
                    // компонентов на одном слкаде может не хватать
                    if (skladElement.Number >= countOnStocks)
                    {
                        skladElement.Number -= countOnStocks;
                        break;
                    }
                    else
                    {
                        countOnStocks -= skladElement.Number;
                        skladElement.Number = 0;
                    }
                }
            }
            element.DateImplement = DateTime.Now;
            element.Status = ZakazStatus.Выполняется;
        }
        public void FinishOrder(ZakazBindingModel model)
        {
            Zakaz element = source.Zakazs.FirstOrDefault(rec => rec.Id == model.Id);
            if (element == null)
            {
                throw new Exception("Элемент не найден");
            }
            if (element.Status != ZakazStatus.Выполняется)
            {
                throw new Exception("Заказ не в статусе \"Выполняется\"");
            }
            element.Status = ZakazStatus.Готов;
        }
        public void PayOrder(ZakazBindingModel model)
        {
            Zakaz element = source.Zakazs.FirstOrDefault(rec => rec.Id == model.Id);
            if (element == null)
            {
                throw new Exception("Элемент не найден");
            }
            if (element.Status != ZakazStatus.Готов)
            {
                throw new Exception("Заказ не в статусе \"Готов\"");
            }
            element.Status = ZakazStatus.Оплачен;
        }
        public void PutComponentOnStock(SkladElementBindingModel model)
        {
            SkladElement element = source.SkladElements.FirstOrDefault(rec =>
            rec.SkladId == model.SkladId && rec.ElementId == model.ElementId);
            if (element != null)
            {
                element.Number += model.Number;
            }
            else
            {
                int maxId = source.SkladElements.Count > 0 ?
                source.SkladElements.Max(rec => rec.Id) : 0;
                source.SkladElements.Add(new SkladElement
                {
                    Id = ++maxId,
                    SkladId = model.SkladId,
                    ElementId = model.ElementId,
                    Number = model.Number
                });
            }
        }
    }
}