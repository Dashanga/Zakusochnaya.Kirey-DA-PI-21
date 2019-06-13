using ZakusochnayaModel;
using ZakusochnayaServiceDAL.BindingModel;
using ZakusochnayaServiceDAL.Interfaces;
using ZakusochnayaServiceDAL.ViewModel;
using System;
using System.Collections.Generic;
using ZakusochnayaServiceDAL;

namespace ZakusochnayaServiceImplementList.Implementations
{
    public class ElementServiceList : IElementService
    {
        private DataListSingleton source;
        public ElementServiceList()
        {
            source = DataListSingleton.GetInstance();
        }
        public List<ElementViewModel> GetList()
        {
            List<ElementViewModel> result = new List<ElementViewModel>();
            for (int i = 0; i < source.Elements.Count; ++i)
            {
                result.Add(new ElementViewModel
                {
                    ElementId = source.Elements[i].ElementId,
                    ElementName = source.Elements[i].ElementName
                });
            }
            return result;
        }
        public ElementViewModel GetElement(int id)
        {
            for (int i = 0; i < source.Elements.Count; ++i)
            {
                if (source.Elements[i].ElementId == id)
                {
                    return new ElementViewModel
                    {
                        ElementId = source.Elements[i].ElementId,
                        ElementName = source.Elements[i].ElementName
                    };
                }
            }
            throw new Exception("Элемент не найден");
        }
        public void AddElement(ElementBindingModel model)
        {
            int maxId = 0;
            for (int i = 0; i < source.Elements.Count; ++i)
            {
                if (source.Elements[i].ElementId > maxId)
                {
                    maxId = source.Elements[i].ElementId;
                }
                if (source.Elements[i].ElementName == model.ElementName)
                {
                    throw new Exception("Уже есть компонент с таким названием");
                }
            }
            source.Elements.Add(new Element
            {
                ElementId = maxId + 1,
                ElementName = model.ElementName
            });
        }
        public void UpdElement(ElementBindingModel model)
        {
            int index = -1;
            for (int i = 0; i < source.Elements.Count; ++i)
            {
                if (source.Elements[i].ElementId == model.ElementId)
                {
                    index = i;
                }
                if (source.Elements[i].ElementName == model.ElementName &&
                source.Elements[i].ElementId != model.ElementId)
                {
                    throw new Exception("Уже есть компонент с таким названием");
                }
            }
            if (index == -1)
            {
                throw new Exception("Элемент не найден");
            }
            source.Elements[index].ElementName = model.ElementName;
        }
        public void DelElement(int id)
        {
            for (int i = 0; i < source.Elements.Count; ++i)
            {
                if (source.Elements[i].ElementId == id)
                {
                    source.Elements.RemoveAt(i);
                    return;
                }
            }
            throw new Exception("Элемент не найден");
        }
    }
}
