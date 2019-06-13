using ZakusochnayaModel;
using ZakusochnayaServiceDAL.BindingModel;
using ZakusochnayaServiceDAL.Interfaces;
using ZakusochnayaServiceDAL.ViewModel;
using System;
using System.Collections.Generic;
using ZakusochnayaServiceDAL;
using ZakusochnayaServiceDAL.ViewModels;
using ZakusochnayaServiceDAL.BindingModels;

namespace ZakusochnayaServiceImplementList.Implementations
{
    public class OutputElementServiceList : IOutputElementService
    {
        private DataListSingleton source;
        public OutputElementServiceList()
        {
            source = DataListSingleton.GetInstance();
        }
        public List<OutputElementViewModel> GetList()
        {
            List<OutputElementViewModel> result = new List<OutputElementViewModel>();
            for (int i = 0; i < source.OutputElements.Count; ++i)
            {
                result.Add(new OutputElementViewModel
                {
                    Id = source.OutputElements[i].Id,
                    OutputId = source.OutputElements[i].OutputId,
                    ElementId = source.OutputElements[i].ElementId,
                    Number = source.OutputElements[i].Number
                });
            }
            return result;
        }
        public OutputElementViewModel GetElement(int id)
        {
            for (int i = 0; i < source.OutputElements.Count; ++i)
            {
                if (source.OutputElements[i].Id == id)
                {
                    return new OutputElementViewModel
                    {
                        Id = source.OutputElements[i].Id,
                        OutputId = source.OutputElements[i].OutputId,
                        ElementId = source.OutputElements[i].ElementId,
                        Number = source.OutputElements[i].Number
                    };
                }
            }
            throw new Exception("Элемент не найден");
        }
        public void AddElement(OutputElementBindingModel model)
        {
            int maxId = 0;
            for (int i = 0; i < source.OutputElements.Count; ++i)
            {
                if (source.OutputElements[i].Id > maxId)
                {
                    maxId = source.OutputElements[i].Id;
                }
                if (source.OutputElements[i].OutputId == model.OutputId &&
                    source.OutputElements[i].ElementId == model.ElementId &&
                    source.OutputElements[i].Number == model.Number)
                {
                    throw new Exception("Уже существует!");
                }
            }
            source.OutputElements.Add(new OutputElement
            {
                Id = maxId + 1,
                OutputId = model.OutputId,
                ElementId = model.ElementId,
                Number = model.Number
            });
        }
        public void UpdElement(OutputElementBindingModel model)
        {
            int index = -1;
            for (int i = 0; i < source.OutputElements.Count; ++i)
            {
                if (source.OutputElements[i].Id == model.Id)
                {
                    index = i;
                }
                if (source.OutputElements[i].OutputId == model.OutputId &&
                  source.OutputElements[i].ElementId == model.ElementId &&
                        source.OutputElements[i].Number == model.Number &&
                    source.OutputElements[i].Id != model.Id)
                {
                    throw new Exception("Уже существует!");
                }
            }
            if (index == -1)
            {
                throw new Exception("Элемент не найден");
            }
            source.OutputElements[index].OutputId = model.OutputId;
            source.OutputElements[index].ElementId = model.ElementId;
            source.OutputElements[index].Number = model.Number;
        }
        public void DelElement(int id)
        {
            for (int i = 0; i < source.OutputElements.Count; ++i)
            {
                if (source.OutputElements[i].Id == id)
                {
                    source.OutputElements.RemoveAt(i);
                    return;
                }
            }
            throw new Exception("Элемент не найден");
        }
    }
}