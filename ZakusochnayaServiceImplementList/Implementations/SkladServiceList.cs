using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZakusochnayaModel;
using ZakusochnayaServiceDAL.BindingModel;
using ZakusochnayaServiceDAL.Interfaces;
using ZakusochnayaServiceDAL.ViewModel;

namespace ZakusochnayaServiceImplementList.Implementations
{
    class SkladServiceList : ISkladService
    {
        private DataListSingleton source;
        public SkladServiceList()
        {
            source = DataListSingleton.GetInstance();
        }
        public List<SkladViewModel> GetList()
        {
            List<SkladViewModel> result = source.Sklads
            .Select(rec => new SkladViewModel
            {
                Id = rec.Id,
                SkladName = rec.SkladName,
                SkladElements = source.SkladElements
            .Where(recPC => recPC.SkladId == rec.Id)
            .Select(recPC => new SkladElementViewModel
            {
                Id = recPC.Id,
                SkladId = recPC.SkladId,
                ElementId = recPC.ElementId,
                ElementName = source.Elements
            .FirstOrDefault(recC => recC.Id ==
            recPC.ElementId)?.ElementName,
                Number = recPC.Number
            })
            .ToList()
            })
            .ToList();
            return result;
        }
        public SkladViewModel GetElement(int id)
        {
            Sklad element = source.Sklads.FirstOrDefault(rec => rec.Id == id);
            if (element != null)
            {
                return new SkladViewModel
                {
                    Id = element.Id,
                    SkladName = element.SkladName,
                    SkladElements = source.SkladElements
                .Where(recPC => recPC.SkladId == element.Id)
                .Select(recPC => new SkladElementViewModel
                {
                    Id = recPC.Id,
                    SkladId = recPC.SkladId,
                    ElementId = recPC.ElementId,
                    ElementName = source.Elements
                .FirstOrDefault(recC => recC.Id ==
                recPC.ElementId)?.ElementName,
                    Number = recPC.Number
                })
                .ToList()
                };
            }
            throw new Exception("Элемент не найден");
        }
        public void AddElement(SkladBindingModel model)
        {
            Sklad element = source.Sklads.FirstOrDefault(rec => rec.SkladName ==
            model.SkladName);
            if (element != null)
            {
                throw new Exception("Уже есть склад с таким названием");
            }
            int maxId = source.Sklads.Count > 0 ? source.Sklads.Max(rec => rec.Id) : 0;
            source.Sklads.Add(new Sklad
            {
                Id = maxId + 1,
                SkladName = model.SkladName
            });
        }
        public void UpdElement(SkladBindingModel model)
        {
            Sklad element = source.Sklads.FirstOrDefault(rec =>
            rec.SkladName == model.SkladName && rec.Id !=
            model.Id);
            if (element != null)
            {
                throw new Exception("Уже есть склад с таким названием");
            }
            element = source.Sklads.FirstOrDefault(rec => rec.Id == model.Id);
            if (element == null)
            {
                throw new Exception("Элемент не найден");
            }
            element.SkladName = model.SkladName;
        }
        public void DelElement(int id)
        {
            Sklad element = source.Sklads.FirstOrDefault(rec => rec.Id == id);
            if (element != null)
            {
                // при удалении удаляем все записи о компонентах на удаляемом складе
                source.SkladElements.RemoveAll(rec => rec.SkladId == id);
                source.Sklads.Remove(element);
            }
            else
            {
                throw new Exception("Элемент не найден");
            }
        }
    }
}
