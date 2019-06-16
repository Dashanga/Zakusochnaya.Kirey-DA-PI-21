using ZakusochnayaModel;
using ZakusochnayaServiceDAL.Interfaces;
using System;
using System.Collections.Generic;
using ZakusochnayaServiceDAL.BindingModels;
using ZakusochnayaServiceDAL.ViewModels;

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
            List<ZakazViewModel> result = new List<ZakazViewModel>();
            for (int i = 0; i < source.Zakazs.Count; ++i)
            {
                string pokuptelFIO = string.Empty;
                for (int j = 0; j < source.Pokupatels.Count; ++j)
                {
                    if (source.Pokupatels[j].Id == source.Zakazs[i].PokupatelId)
                    {
                        pokuptelFIO = source.Pokupatels[j].PokupatelFIO;
                        break;
                    }
                }
                string OutputName = string.Empty;
                for (int j = 0; j < source.Outputs.Count; ++j)
                {
                    if (source.Outputs[j].Id == source.Zakazs[i].OutputId)
                    {
                        OutputName = source.Outputs[j].OutputName;
                        break;
                    }
                }
                result.Add(new ZakazViewModel
                {
                    Id = source.Zakazs[i].Id,
                    PokupatelId = source.Zakazs[i].PokupatelId,
                    PokupatelFIO = pokuptelFIO,
                    OutputId = source.Zakazs[i].OutputId,
                    OutputName = OutputName,
                    Number = source.Zakazs[i].Number,
                    Summa = source.Zakazs[i].Summa,
                    DateCreate = source.Zakazs[i].DateCreate.ToLongDateString(),
                    DateImplement = source.Zakazs[i].DateImplement?.ToLongDateString(),
                    Status = source.Zakazs[i].Status.ToString()
                });
            }
            return result;
        }
        public void CreateOrder(ZakazBindingModel model)
        {
            int maxId = 0;
            for (int i = 0; i < source.Zakazs.Count; ++i)
            {
                if (source.Zakazs[i].Id > maxId)
                {
                    maxId = source.Pokupatels[i].Id;
                }
            }
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
            int index = -1;
            for (int i = 0; i < source.Zakazs.Count; ++i)
            {
                if (source.Zakazs[i].Id == model.Id)
                {
                    index = i;
                    break;
                }
            }
            if (index == -1)
            {
                throw new Exception("Элемент не найден");
            }
            if (source.Zakazs[index].Status != ZakazStatus.Принят)
            {
                throw new Exception("Заказ не в статусе \"Принят\"");
            }
            source.Zakazs[index].DateImplement = DateTime.Now;
            source.Zakazs[index].Status = ZakazStatus.Выполняется;
        }
        public void FinishOrder(ZakazBindingModel model)
        {
            int index = -1;
            for (int i = 0; i < source.Zakazs.Count; ++i)
            {
                if (source.Pokupatels[i].Id == model.Id)
                {
                    index = i;
                    break;
                }
            }
            if (index == -1)
            {
                throw new Exception("Элемент не найден");
            }
            if (source.Zakazs[index].Status != ZakazStatus.Выполняется)
            {
                throw new Exception("Заказ не в статусе \"Выполняется\"");
            }
            source.Zakazs[index].Status = ZakazStatus.Готов;
        }
        public void PayOrder(ZakazBindingModel model)
        {
            int index = -1;
            for (int i = 0; i < source.Zakazs.Count; ++i)
            {
                if (source.Pokupatels[i].Id == model.Id)
                {
                    index = i;
                    break;
                }
            }
            if (index == -1)
            {
                throw new Exception("Элемент не найден");
            }
            if (source.Zakazs[index].Status != ZakazStatus.Готов)
            {
                throw new Exception("Заказ не в статусе \"Готов\"");
            }
            source.Zakazs[index].Status = ZakazStatus.Оплачен;
        }
    }
}