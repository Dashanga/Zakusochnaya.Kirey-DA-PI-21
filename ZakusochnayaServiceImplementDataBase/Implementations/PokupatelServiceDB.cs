using ZakusochnayaModel;
using System;
using System.Collections.Generic;
using System.Linq;
using ZakusochnayaServiceDAL;
using ZakusochnayaServiceDAL.ViewModel;
using ZakusochnayaServiceDAL.BindingModel;

namespace ZakusochnayaServiceImplementDataBase
{
    public class PokupatelServiceDB : IPokupatelService
    {
        private ZakusochnayaDbContext context;
        public PokupatelServiceDB(ZakusochnayaDbContext context)
        {
            this.context = context;
        }
        public List<PokupatelViewModel> GetList()
        {
            List<PokupatelViewModel> result = context.Pokupatels.Select(rec => new
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
            Pokupatel element = context.Pokupatels.FirstOrDefault(rec => rec.Id == id);
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
            Pokupatel element = context.Pokupatels.FirstOrDefault(rec => rec.PokupatelFIO ==
            model.PokupatelFIO);
            if (element != null)
            {
                throw new Exception("Уже есть клиент с таким ФИО");
            }
            context.Pokupatels.Add(new Pokupatel
            {
                PokupatelFIO = model.PokupatelFIO
            });
            context.SaveChanges();
        }
        public void UpdElement(PokupatelBindingModel model)
        {
            Pokupatel element = context.Pokupatels.FirstOrDefault(rec => rec.PokupatelFIO ==
            model.PokupatelFIO && rec.Id != model.Id);
            if (element != null)
            {
                throw new Exception("Уже есть клиент с таким ФИО");
            }
            element = context.Pokupatels.FirstOrDefault(rec => rec.Id == model.Id);
            if (element == null)
            {
                throw new Exception("Элемент не найден");
            }
            element.PokupatelFIO = model.PokupatelFIO;
            context.SaveChanges();
        }
        public void DelElement(int id)
        {
            Pokupatel element = context.Pokupatels.FirstOrDefault(rec => rec.Id == id);
            if (element != null)
            {
                context.Pokupatels.Remove(element);
                context.SaveChanges();
            }
            else
            {
                throw new Exception("Элемент не найден");
            }
        }
    }
}
