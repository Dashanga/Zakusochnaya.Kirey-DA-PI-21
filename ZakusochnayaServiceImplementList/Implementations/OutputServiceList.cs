using ZakusochnayaModel;
using ZakusochnayaServiceDAL.Interfaces;
using System;
using System.Collections.Generic;
using ZakusochnayaServiceDAL.ViewModels;
using ZakusochnayaServiceDAL.BindingModels;

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
            List<OutputViewModel> result = new List<OutputViewModel>();
            for (int i = 0; i < source.Outputs.Count; ++i)
            {
                // требуется дополнительно получить список компонентов для изделия и их количество
            List<OutputElementViewModel> outputElements = new
            List<OutputElementViewModel>();
                for (int j = 0; j < source.OutputElements.Count; ++j)
                {
                    if (source.OutputElements[j].OutputId == source.Outputs[i].Id)
                    {
                        string elementName = string.Empty;
                        for (int k = 0; k < source.Elements.Count; ++k)
                        {
                            if (source.OutputElements[j].ElementId ==
                            source.Elements[k].Id)
                            {
                                elementName = source.Elements[k].ElementName;
                                break;
                            }
                        }
                        outputElements.Add(new OutputElementViewModel
                        {
                            Id = source.OutputElements[j].Id,
                            OutputId = source.OutputElements[j].OutputId,
                            ElementId = source.OutputElements[j].ElementId,
                            ElementName = elementName,
                            Number = source.OutputElements[j].Number
                        });
                    }
                }
                result.Add(new OutputViewModel
                {
                    Id = source.Outputs[i].Id,
                    OutputName = source.Outputs[i].OutputName,
                    Cost = source.Outputs[i].Cost,
                    OutputElements = outputElements
                });
            }
            return result;
        }
        public OutputViewModel GetElement(int id)
        {
            for (int i = 0; i < source.Outputs.Count; ++i)
            {
                // требуется дополнительно получить список компонентов для изделия и их количество
            List<OutputElementViewModel> outputElements = new
            List<OutputElementViewModel>();
                for (int j = 0; j < source.OutputElements.Count; ++j)
                {
                    if (source.OutputElements[j].OutputId == source.Outputs[i].Id)
                    {
                        string elementName = string.Empty;
                        for (int k = 0; k < source.Elements.Count; ++k)
                        {
                            if (source.OutputElements[j].ElementId ==
                            source.Elements[k].Id)
                            {
                                elementName = source.Elements[k].ElementName;
                                break;
                            }
                        }
                        outputElements.Add(new OutputElementViewModel
                        {
                            Id = source.OutputElements[j].Id,
                            OutputId = source.OutputElements[j].OutputId,
                            ElementId = source.OutputElements[j].ElementId,
                            ElementName = elementName,
                            Number = source.OutputElements[j].Number
                        });
                    }
                }
                if (source.Outputs[i].Id == id)
                {
                    return new OutputViewModel
                    {
                        Id = source.Outputs[i].Id,
                        OutputName = source.Outputs[i].OutputName,
                        Cost = source.Outputs[i].Cost,
                        OutputElements = outputElements
                    };
                }
            }
            throw new Exception("Элемент не найден");
        }
        public void AddElement(OutputBindingModel model)
        {
            int maxId = 0;
            for (int i = 0; i < source.Outputs.Count; ++i)
            {
                if (source.Outputs[i].Id > maxId)
                {
                    maxId = source.Outputs[i].Id;
                }
                if (source.Outputs[i].OutputName == model.OutputName)
                {
                    throw new Exception("Уже есть продукт с таким названием");
                }
            }
            source.Outputs.Add(new Output
            {
                Id = maxId + 1,
                OutputName = model.OutputName,
                Cost = model.Cost
            });
            // компоненты для изделия
            int maxPCId = 0;
            for (int i = 0; i < source.OutputElements.Count; ++i)
            {
                if (source.OutputElements[i].Id > maxPCId)
                {
                    maxPCId = source.OutputElements[i].Id;
                }
            }
            // убираем дубли по компонентам
            for (int i = 0; i < model.OutputElements.Count; ++i)
            {
                for (int j = 1; j < model.OutputElements.Count; ++j)
                {
                    if (model.OutputElements[i].ElementId ==
                    model.OutputElements[j].ElementId)
                    {
                        model.OutputElements[i].Number +=
                        model.OutputElements[j].Number;
                        model.OutputElements.RemoveAt(j--);
                    }
                }
            }
            // добавляем компоненты
            for (int i = 0; i < model.OutputElements.Count; ++i)
            {
                source.OutputElements.Add(new OutputElement
                {
                    Id = ++maxPCId,
                    OutputId = maxId + 1,
                    ElementId = model.OutputElements[i].ElementId,
                    Number = model.OutputElements[i].Number
                });
            }
        }
        public void UpdElement(OutputBindingModel model)
        {
            int index = -1;
            for (int i = 0; i < source.Outputs.Count; ++i)
            {
                if (source.Outputs[i].Id == model.Id)
                {
                    index = i;
                }
                if (source.Outputs[i].OutputName == model.OutputName &&
                source.Outputs[i].Id != model.Id)
                {
                    throw new Exception("Уже есть продукт с таким названием");
                }
            }
            if (index == -1)
            {
                throw new Exception("Элемент не найден");
            }
            source.Outputs[index].OutputName = model.OutputName;
            source.Outputs[index].Cost = model.Cost;
            int maxPCId = 0;
            for (int i = 0; i < source.OutputElements.Count; ++i)
            {
                if (source.OutputElements[i].Id > maxPCId)
                {
                    maxPCId = source.OutputElements[i].Id;
                }
            }
            // обновляем существуюущие компоненты
            for (int i = 0; i < source.OutputElements.Count; ++i)
            {
                if (source.OutputElements[i].OutputId == model.Id)
                {
                    bool flag = true;
                    for (int j = 0; j < model.OutputElements.Count; ++j)
                    {
                        // если встретили, то изменяем количество
                        if (source.OutputElements[i].Id ==
                        model.OutputElements[j].Id)
                        {
                            source.OutputElements[i].Number =
                            model.OutputElements[j].Number;
                            flag = false;
                            break;
                        }
                    }
                    // если не встретили, то удаляем
                    if (flag)
                    {
                        source.OutputElements.RemoveAt(i--);
                    }
                }
            }
            // новые записи
            for (int i = 0; i < model.OutputElements.Count; ++i)
            {
                if (model.OutputElements[i].Id == 0)
                {
                    // ищем дубли
                    for (int j = 0; j < source.OutputElements.Count; ++j)
                    {
                        if (source.OutputElements[j].OutputId == model.Id &&
                        source.OutputElements[j].ElementId ==
                        model.OutputElements[i].ElementId)
                        {
                            source.OutputElements[j].Number +=
                            model.OutputElements[i].Number;
                            model.OutputElements[i].Id =
                            source.OutputElements[j].Id;
                            break;
                        }
                    }
                    // если не нашли дубли, то новая запись
                    if (model.OutputElements[i].Id == 0)
                    {
                        source.OutputElements.Add(new OutputElement
                        {
                            Id = ++maxPCId,
                            OutputId = model.Id,
                            ElementId = model.OutputElements[i].ElementId,
                            Number = model.OutputElements[i].Number
                        });
                    }
                }
            }
        }
        public void DelElement(int id)
        {
            // удаяем записи по компонентам при удалении изделия
            for (int i = 0; i < source.OutputElements.Count; ++i)
            {
                if (source.OutputElements[i].OutputId == id)
                {
                    source.OutputElements.RemoveAt(i--);
                }
            }
            for (int i = 0; i < source.Outputs.Count; ++i)
            {
                if (source.Outputs[i].Id == id)
                {
                    source.Outputs.RemoveAt(i);
                    return;
                }
            }
            throw new Exception("Элемент не найден");
        }
    }
}