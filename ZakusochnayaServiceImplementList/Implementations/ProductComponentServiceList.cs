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
    public class ProductComponentServiceList : IProductComponentService
    {
        private DataListSingleton source;
        public ProductComponentServiceList()
        {
            source = DataListSingleton.GetInstance();
        }
        public List<ProductComponentViewModel> GetList()
        {
            List<ProductComponentViewModel> result = new List<ProductComponentViewModel>();
            for (int i = 0; i < source.ProductComponents.Count; ++i)
            {
                result.Add(new ProductComponentViewModel
                {
                    Id = source.ProductComponents[i].Id,
                    ProductId = source.ProductComponents[i].ProductId,
                    ComponentId = source.ProductComponents[i].ComponentId,
                    Count = source.ProductComponents[i].Count
                });
            }
            return result;
        }
        public ProductComponentViewModel GetElement(int id)
        {
            for (int i = 0; i < source.ProductComponents.Count; ++i)
            {
                if (source.ProductComponents[i].Id == id)
                {
                    return new ProductComponentViewModel
                    {
                        Id = source.ProductComponents[i].Id,
                        ProductId = source.ProductComponents[i].ProductId,
                        ComponentId = source.ProductComponents[i].ComponentId,
                        Count = source.ProductComponents[i].Count
                    };
                }
            }
            throw new Exception("Элемент не найден");
        }
        public void AddElement(ProductComponentBindingModel model)
        {
            int maxId = 0;
            for (int i = 0; i < source.ProductComponents.Count; ++i)
            {
                if (source.ProductComponents[i].Id > maxId)
                {
                    maxId = source.ProductComponents[i].Id;
                }
                if (source.ProductComponents[i].ProductId == model.ProductId &&
                    source.ProductComponents[i].ComponentId == model.ComponentId &&
                    source.ProductComponents[i].Count == model.Count)
                {
                    throw new Exception("Уже существует!");
                }
            }
            source.ProductComponents.Add(new ProductComponent
            {
                Id = maxId + 1,
                ProductId = model.ProductId,
                ComponentId = model.ComponentId,
                Count = model.Count
            });
        }
        public void UpdElement(ProductComponentBindingModel model)
        {
            int index = -1;
            for (int i = 0; i < source.ProductComponents.Count; ++i)
            {
                if (source.ProductComponents[i].Id == model.Id)
                {
                    index = i;
                }
                if (source.ProductComponents[i].ProductId == model.ProductId &&
                  source.ProductComponents[i].ComponentId == model.ComponentId &&
                        source.ProductComponents[i].Count == model.Count &&
                    source.ProductComponents[i].Id != model.Id)
                {
                    throw new Exception("Уже существует!");
                }
            }
            if (index == -1)
            {
                throw new Exception("Элемент не найден");
            }
            source.ProductComponents[index].ProductId = model.ProductId;
            source.ProductComponents[index].ComponentId = model.ComponentId;
            source.ProductComponents[index].Count = model.Count;
        }
        public void DelElement(int id)
        {
            for (int i = 0; i < source.ProductComponents.Count; ++i)
            {
                if (source.ProductComponents[i].Id == id)
                {
                    source.ProductComponents.RemoveAt(i);
                    return;
                }
            }
            throw new Exception("Элемент не найден");
        }
    }
}