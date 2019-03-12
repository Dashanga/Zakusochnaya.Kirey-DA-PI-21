using System;
using System.Collections.Generic;
using System.Linq;
using ZakusochnayaModel;
using ZakusochnayaServiceDAL.BindingModel;
using ZakusochnayaServiceDAL.Interfaces;
using ZakusochnayaServiceDAL.ViewModel;

namespace ZakusochnayaServiceImplementDataBase.Implementations
{
    public class SkladServiceDB : ISkladService
    {
        private ZakusochnayaDbContext context;
        public SkladServiceDB(ZakusochnayaDbContext context)
        {
            this.context = context;
        }
        public List<SkladViewModel> GetList()
        {
            List<SkladViewModel> result = context.Sklads.Select(rec => new
            SkladViewModel
            {
                Id = rec.Id,
                SkladName = rec.SkladName,
                SkladElements = context.SkladElements
            .Where(recPC => recPC.SkladId == rec.Id)
            .Select(recPC => new SkladElementViewModel
            {
                Id = recPC.Id,
                SkladId = recPC.SkladId,
                ElementId = recPC.ElementId,
                ElementName = recPC.Element.ElementName,
                Number = recPC.Number
            })
            .ToList()
            })
            .ToList();
            return result;
        }
        public SkladViewModel GetElement(int id)
        {
            Sklad element = context.Sklads.FirstOrDefault(rec => rec.Id == id);
            if (element != null)
            {
                return new SkladViewModel
                {
                    Id = element.Id,
                    SkladName = element.SkladName,
                     SkladElements = context.SkladElements
                    .Where(recPC => recPC.SkladId == element.Id)
                    .Select(recPC => new SkladElementViewModel
                    {
                        Id = recPC.Id,
                        SkladId = recPC.SkladId,
                        ElementId = recPC.ElementId,
                        ElementName = recPC.Element.ElementName,
                        Number = recPC.Number
                    })
                    .ToList()
                };
            }
            throw new Exception("Элемент не найден");
        }
        public void AddElement(SkladBindingModel model)
        {
            Sklad element = context.Sklads.FirstOrDefault(rec => rec.SkladName ==
            model.SkladName);
            if (element != null)
            {
                throw new Exception("Уже есть склад с таким названием");
            }
            context.Sklads.Add(new Sklad
            {
                SkladName = model.SkladName
            });
            context.SaveChanges();
        }
        public void UpdElement(SkladBindingModel model)
        {
            Sklad element = context.Sklads.FirstOrDefault(rec => rec.SkladName ==
            model.SkladName && rec.Id != model.Id);
            if (element != null)
            {
                throw new Exception("Уже есть склад с таким названием");
            }
            element = context.Sklads.FirstOrDefault(rec => rec.Id == model.Id);
            if (element == null)
            {
                throw new Exception("Элемент не найден");
            }
            element.SkladName = model.SkladName;
            context.SaveChanges();
        }
        public void DelElement(int id)
        {
            Sklad element = context.Sklads.FirstOrDefault(rec => rec.Id == id);
            if (element != null)
            {
                context.Sklads.Remove(element);
                context.SaveChanges();
            }
            else
            {
                throw new Exception("Элемент не найден");
            }
        }
    }
}
