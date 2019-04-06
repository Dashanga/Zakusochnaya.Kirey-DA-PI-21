﻿using System;
using System.Collections.Generic;
using System.Linq;
using ZakusochnayaModel;
using ZakusochnayaServiceDAL.BindingModel;
using ZakusochnayaServiceDAL.Interfaces;
using ZakusochnayaServiceDAL.ViewModel;

namespace ZakusochnayaServiceImplementDataBase
{
    public class ElementServiceDB : IElementService
    {
        private ZakusochnayaDbContext context;
        public ElementServiceDB(ZakusochnayaDbContext context)
        {
            this.context = context;
        }
        public List<ElementViewModel> GetList()
        {
            List<ElementViewModel> result = context.Elements.Select(rec => new
            ElementViewModel
            {
                Id = rec.Id,
                ElementName = rec.ElementName
            })
            .ToList();
            return result;
        }
        public ElementViewModel GetElement(int id)
        {
            Element element = context.Elements.FirstOrDefault(rec => rec.Id == id);
            if (element != null)
            {
                return new ElementViewModel
                {
                    Id = element.Id,
                    ElementName = element.ElementName
                };
            }
            throw new Exception("Элемент не найден");
        }
        public void AddElement(ElementBindingModel model)
        {
            Element element = context.Elements.FirstOrDefault(rec => rec.ElementName ==
            model.ElementName);
            if (element != null)
            {
                throw new Exception("Уже есть компонент с таким названием");
            }
            context.Elements.Add(new Element
            {
                ElementName = model.ElementName
            });
            context.SaveChanges();
        }
        public void UpdElement(ElementBindingModel model)
        {
            Element element = context.Elements.FirstOrDefault(rec => rec.ElementName ==
            model.ElementName && rec.Id != model.Id);
            if (element != null)
            {
                throw new Exception("Уже есть компонент с таким названием");
            }
            element = context.Elements.FirstOrDefault(rec => rec.Id == model.Id);
            if (element == null)
            {
                throw new Exception("Элемент не найден");
            }
            element.ElementName = model.ElementName;
            context.SaveChanges();
        }
        public void DelElement(int id)
        {
            Element element = context.Elements.FirstOrDefault(rec => rec.Id == id);
            if (element != null)
            {
                context.Elements.Remove(element);
                context.SaveChanges();
            }
            else
            {
                throw new Exception("Элемент не найден");
            }
        }
    }
}