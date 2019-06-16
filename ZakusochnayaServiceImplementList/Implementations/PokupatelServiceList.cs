using ZakusochnayaModel;
using ZakusochnayaServiceDAL.BindingModel;
using ZakusochnayaServiceDAL.Interfaces;
using ZakusochnayaServiceDAL.ViewModel;
using System;
using System.Collections.Generic;
using ZakusochnayaServiceDAL;
using System.Linq;

namespace ZakusochnayaServiceImplementList.Implementations
{
    public class PokupatelServiceList : IPokupatelService
    {
        private DataListSingleton source;
        public PokupatelServiceList()
        {
            source = DataListSingleton.GetInstance();
        }
        public List<PokupatelViewModel> GetList()
        {
            List<PokupatelViewModel> result = source.Pokupatels.Select(rec => new
PokupatelViewModel
            {
                Id = rec.Id,
                PokupatelFIO = rec.PokupatelFIO
            })
.ToList();
            return result;
        }
        public PokupatelViewModel GetElement(int id)
        {
            Pokupatel element = source.Pokupatels.FirstOrDefault(rec => rec.Id == id);
            if (element != null)
            {
                return new PokupatelViewModel
                {
                    Id = element.Id,
                    PokupatelFIO = element.PokupatelFIO
                };
            }
            throw new Exception("Элемент не найден");
        }
        public void AddElement(PokupatelBindingModel model)
        {
            Pokupatel element = source.Pokupatels.FirstOrDefault(rec => rec.PokupatelFIO ==
 model.PokupatelFIO);
            if (element != null)
            {
                throw new Exception("Уже есть клиент с таким ФИО");
            }
            int maxId = source.Pokupatels.Count > 0 ? source.Pokupatels.Max(rec => rec.Id) : 0;
            source.Pokupatels.Add(new Pokupatel
            {
                Id = maxId + 1,
                PokupatelFIO = model.PokupatelFIO
            });
        }
        public void UpdElement(PokupatelBindingModel model)
        {
            Pokupatel element = source.Pokupatels.FirstOrDefault(rec => rec.PokupatelFIO ==
 model.PokupatelFIO && rec.Id != model.Id);
            if (element != null)
            {
                throw new Exception("Уже есть клиент с таким ФИО");
            }
            element = source.Pokupatels.FirstOrDefault(rec => rec.Id == model.Id);
            if (element == null)
            {
                throw new Exception("Элемент не найден");
            }
            element.PokupatelFIO = model.PokupatelFIO;
        }
        public void DelElement(int id)
        {
            Pokupatel element = source.Pokupatels.FirstOrDefault(rec => rec.Id == id);
            if (element != null)
            {
                source.Pokupatels.Remove(element);
            }
            else
            {
                throw new Exception("Элемент не найден");
            }
        }
    }
}