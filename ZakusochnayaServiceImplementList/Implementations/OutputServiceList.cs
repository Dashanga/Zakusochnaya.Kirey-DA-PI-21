using ZakusochnayaModel;
using ZakusochnayaServiceDAL.Interfaces;
using System;
using System.Collections.Generic;
using ZakusochnayaServiceDAL.ViewModels;
using ZakusochnayaServiceDAL.BindingModels;
using System.Linq;

namespace ZakusochnayaServiceImplementList.Implementations
{
    public class OutputServiceList : IOutputService
    {
        private DataListSingleton source;
        public OutputServiceList()
        {
            source = DataListSingleton.GetInstance();
        }
        public List<OutputViewModel> GetList()
        {
            List<OutputViewModel> result = source.Outputs
                .Select(rec => new OutputViewModel
                {
                    Id = rec.Id,
                    OutputName = rec.OutputName,
                    Cost = rec.Cost,
                    OutputElements = source.OutputElements
                    .Where(recPC => recPC.OutputId == rec.Id)
                    .Select(recPC => new OutputElementViewModel
                    {
                        Id = recPC.Id,
                        OutputId = recPC.OutputId,
                        ElementId = recPC.ElementId,
                        ElementName = source.Elements.FirstOrDefault(recC =>
                        recC.Id == recPC.ElementId)?.ElementName,
                        Number = recPC.Number
                    })
                    .ToList()
                })
                .ToList();
            return result;
        }
        public OutputViewModel GetElement(int id)
        {
            Output element = source.Outputs.FirstOrDefault(rec => rec.Id == id);
            if (element != null)
            {
                return new OutputViewModel
                {
                    Id = element.Id,
                    OutputName = element.OutputName,
                    Cost = element.Cost,
                    OutputElements = source.OutputElements
                        .Where(recPC => recPC.OutputId == element.Id)
                        .Select(recPC => new OutputElementViewModel
                        {
                            Id = recPC.Id,
                            OutputId = recPC.OutputId,
                            ElementId = recPC.ElementId,
                            ElementName = source.Elements.FirstOrDefault(recC =>
    recC.Id == recPC.ElementId)?.ElementName,
                            Number = recPC.Number
                        })
                        .ToList()
                };
            }
            throw new Exception("Элемент не найден");
        }
        public void AddElement(OutputBindingModel model)
        {
            Output element = source.Outputs.FirstOrDefault(rec => rec.OutputName ==
            model.OutputName);
            if (element != null)
            {
                throw new Exception("Уже есть изделие с таким названием");
            }
            int maxId = source.Outputs.Count > 0 ? source.Outputs.Max(rec => rec.Id) :
            0;
            source.Outputs.Add(new Output
            {
                Id = maxId + 1,
                OutputName = model.OutputName,
                Cost = model.Cost
            });
            // компоненты для изделия
            int maxPCId = source.OutputElements.Count > 0 ?
            source.OutputElements.Max(rec => rec.Id) : 0;
            // убираем дубли по компонентам
            var groupElements = model.OutputElements
            .GroupBy(rec => rec.ElementId)
            .Select(rec => new
            {
                ElementId = rec.Key,
                Number = rec.Sum(r => r.Number)
            });
            // добавляем компоненты
            foreach (var groupElement in groupElements)
            {
                source.OutputElements.Add(new OutputElement
                {
                    Id = ++maxPCId,
                    OutputId = maxId + 1,
                    ElementId = groupElement.ElementId,
                    Number = groupElement.Number
                });
            }
        }
        public void UpdElement(OutputBindingModel model)
        {
            Output element = source.Outputs.FirstOrDefault(rec => rec.OutputName ==
 model.OutputName && rec.Id != model.Id);
            if (element != null)
            {
                throw new Exception("Уже есть изделие с таким названием");
            }
            element = source.Outputs.FirstOrDefault(rec => rec.Id == model.Id);
            if (element == null)
            {
                throw new Exception("Элемент не найден");
            }
            element.OutputName = model.OutputName;
            element.Cost = model.Cost;
            int maxPCId = source.OutputElements.Count > 0 ?
            source.OutputElements.Max(rec => rec.Id) : 0;
            // обновляем существуюущие компоненты
            var compIds = model.OutputElements.Select(rec =>
            rec.ElementId).Distinct();
            var updateElements = source.OutputElements.Where(rec => rec.OutputId ==
            model.Id && compIds.Contains(rec.ElementId));
            foreach (var updateElement in updateElements)
            {
                updateElement.Number = model.OutputElements.FirstOrDefault(rec =>
                rec.Id == updateElement.Id).Number;
            }
            source.OutputElements.RemoveAll(rec => rec.OutputId == model.Id &&
            !compIds.Contains(rec.ElementId));
            // новые записи
            var groupElements = model.OutputElements
            .Where(rec => rec.Id == 0)
            .GroupBy(rec => rec.ElementId)
            .Select(rec => new
            {
                ElementId = rec.Key,
                Number = rec.Sum(r => r.Number)
            });
            foreach (var groupElement in groupElements)
            {
                OutputElement elementPC = source.OutputElements.FirstOrDefault(rec
                => rec.OutputId == model.Id && rec.ElementId == groupElement.ElementId);
                if (elementPC != null)
                {
                    elementPC.Number += groupElement.Number;
                }
                else
                {
                    source.OutputElements.Add(new OutputElement
                    {
                        Id = ++maxPCId,
                        OutputId = model.Id,
                        ElementId = groupElement.ElementId,
                        Number = groupElement.Number
                    });
                }
            }
        }
        public void DelElement(int id)
        {
            Output element = source.Outputs.FirstOrDefault(rec => rec.Id == id);
            if (element != null)
            {
                // удаяем записи по компонентам при удалении изделия
                source.OutputElements.RemoveAll(rec => rec.OutputId == id);
                source.Outputs.Remove(element);
            }
            else
            {
                throw new Exception("Элемент не найден");
            }
        }
    }
}