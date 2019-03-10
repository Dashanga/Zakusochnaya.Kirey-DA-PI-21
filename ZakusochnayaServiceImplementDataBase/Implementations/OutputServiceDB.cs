using ZakusochnayaModel;
using ZakusochnayaServiceDAL.BindingModels;
using ZakusochnayaServiceDAL.Interfaces;
using ZakusochnayaServiceDAL.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;

namespace ZakusochnayaServiceImplementDataBase.Implementations
{
    class OutputServiceDB : IOutputService
    {
        private ZakusochnayaDbContext context;
        public OutputServiceDB(ZakusochnayaDbContext context)
        {
            this.context = context;
        }
        public List<OutputViewModel> GetList()
        {
            List<OutputViewModel> result = context.Outputs.Select(rec => new
            OutputViewModel
            {
                Id = rec.Id,
                OutputName = rec.OutputName,
                Cost = rec.Cost,
                OutputElements = context.OutputElements
            .Where(recPC => recPC.OutputId == rec.Id)
            .Select(recPC => new OutputElementViewModel
            {
                Id = recPC.Id,
                OutputId = recPC.OutputId,
                ElementId = recPC.ElementId,
                ElementName = recPC.Element.ElementName,
                Number = recPC.Number
            })
            .ToList()
            })
            .ToList();
            return result;
        }
        public OutputViewModel GetElement(int id)
        {
            Output element = context.Outputs.FirstOrDefault(rec => rec.Id == id);
            if (element != null)
            {
                return new OutputViewModel
                {
                    Id = element.Id,
                    OutputName = element.OutputName,
                    Cost = element.Cost,
                    OutputElements = context.OutputElements
                    .Where(recPC => recPC.OutputId == element.Id)
                    .Select(recPC => new OutputElementViewModel
                    {
                         Id = recPC.Id,
                        OutputId = recPC.OutputId,
                        ElementId = recPC.ElementId,
                        ElementName = recPC.Element.ElementName,
                        Number = recPC.Number
                    })
                    .ToList()
                };
            }
            throw new Exception("Элемент не найден");
        }
        public void AddElement(OutputBindingModel model)
        {
            using (var transaction = context.Database.BeginTransaction())
            {
                try
                {
                    Output element = context.Outputs.FirstOrDefault(rec =>
                    rec.OutputName == model.OutputName);
                    if (element != null)
                    {
                        throw new Exception("Уже есть изделие с таким названием");
                    }
                    element = new Output
                    {
                        OutputName = model.OutputName,
                        Cost = model.Cost
                    };
                    context.Outputs.Add(element);
                    context.SaveChanges();
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
                        context.OutputElements.Add(new OutputElement
                        {
                            OutputId = element.Id,
                            ElementId = groupElement.ElementId,
                            Number = groupElement.Number
                        });
                        context.SaveChanges();
                    }
                    transaction.Commit();
                }
                catch (Exception)
                {
                    transaction.Rollback();
                    throw;
                }
            }
        }
        public void UpdElement(OutputBindingModel model)
        {
            using (var transaction = context.Database.BeginTransaction())
            {
                try
                {
                    Output element = context.Outputs.FirstOrDefault(rec =>
                    rec.OutputName == model.OutputName && rec.Id != model.Id);
                    if (element != null)
                    {
                        throw new Exception("Уже есть изделие с таким названием");
                    }
                    element = context.Outputs.FirstOrDefault(rec => rec.Id == model.Id);
                    if (element == null)
                    {
                        throw new Exception("Элемент не найден");
                    }
                    element.OutputName = model.OutputName;
                    element.Cost = model.Cost;
                    context.SaveChanges();
                    // обновляем существуюущие компоненты
                    var compIds = model.OutputElements.Select(rec =>
                    rec.ElementId).Distinct();
                    var updateElements = context.OutputElements.Where(rec =>
                    rec.OutputId == model.Id && compIds.Contains(rec.ElementId));
                    foreach (var updateElement in updateElements)
                    {
                        updateElement.Number =
                        model.OutputElements.FirstOrDefault(rec => rec.Id == updateElement.Id).Number;
                    }
                    context.SaveChanges();
                    context.OutputElements.RemoveRange(context.OutputElements.Where(rec =>
                    rec.OutputId == model.Id && !compIds.Contains(rec.ElementId)));
                    context.SaveChanges();
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
                        OutputElement elementPC =
                        context.OutputElements.FirstOrDefault(rec => rec.OutputId == model.Id &&
                        rec.ElementId == groupElement.ElementId);
                        if (elementPC != null)
                        {
                            elementPC.Number += groupElement.Number;
                            context.SaveChanges();
                        }
                        else
                        {
                            context.OutputElements.Add(new OutputElement
                            {
                                OutputId = model.Id,
                                ElementId = groupElement.ElementId,
                                Number = groupElement.Number
                            });
                            context.SaveChanges();
                        }
                    }
                    transaction.Commit();
                }
                catch (Exception)
                {
                    transaction.Rollback();
                    throw;
                }
            }
        }
        public void DelElement(int id)
        {
            using (var transaction = context.Database.BeginTransaction())
            {
                try
                {
                    Output element = context.Outputs.FirstOrDefault(rec => rec.Id ==
                    id);
                    if (element != null)
                    {
                        // удаяем записи по компонентам при удалении изделия
                        context.OutputElements.RemoveRange(context.OutputElements.Where(rec =>
                        rec.OutputId == id));
                        context.Outputs.Remove(element);
                        context.SaveChanges();
                    }
                    else
                    {
                        throw new Exception("Элемент не найден");
                    }
                    transaction.Commit();
                }
                catch (Exception)
                {
                    transaction.Rollback();
                    throw;
                }
            }
        }
    }
}
