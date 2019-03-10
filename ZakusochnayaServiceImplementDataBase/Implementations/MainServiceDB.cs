﻿using ZakusochnayaModel;
using ZakusochnayaServiceDAL.BindingModels;
using ZakusochnayaServiceDAL.Interfaces;
using ZakusochnayaServiceDAL.ViewModels;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.SqlServer;
using System.Linq;

namespace ZakusochnayaServiceImplementDataBase.Implementations
{
    class MainServiceDB : IMainService
    {
        private ZakusochnayaDbContext context;
    public MainServiceDB(ZakusochnayaDbContext context)
    {
        this.context = context;
    }
    public List<ZakazViewModel> GetList()
    {
        List<ZakazViewModel> result = context.Zakazs.Select(rec => new ZakazViewModel
        {
            Id = rec.Id,
            PokupatelId = rec.PokupatelId,
            OutputId = rec.OutputId,
            DateCreate = SqlFunctions.DateName("dd", rec.DateCreate) + " " +
        SqlFunctions.DateName("mm", rec.DateCreate) + " " +
        SqlFunctions.DateName("yyyy", rec.DateCreate),
            DateImplement = rec.DateImplement == null ? "" :
        SqlFunctions.DateName("dd",
        rec.DateImplement.Value) + " " +
        SqlFunctions.DateName("mm",
        rec.DateImplement.Value) + " " +
        SqlFunctions.DateName("yyyy",
        rec.DateImplement.Value),
            Status = rec.Status.ToString(),
            Number = rec.Number,
            Summa = rec.Summa,
            PokupatelFIO = rec.Pokupatel.PokupatelFIO,
            OutputName = rec.Output.OutputName
        })
        .ToList();
        return result;
    }
    public void CreateOrder(OrderBindingModel model)
    {
        context.Orders.Add(new Order
        {
            ClientId = model.ClientId,
            ProductId = model.ProductId,
            DateCreate = DateTime.Now,
            Count = model.Count,
            Sum = model.Sum,
            Status = OrderStatus.Принят
        });
        context.SaveChanges();
    }
    public void TakeOrderInWork(OrderBindingModel model)
    {
        using (var transaction = context.Database.BeginTransaction())
        {
            try
            {
                Order element = context.Orders.FirstOrDefault(rec => rec.Id ==
                model.Id);
                if (element == null)
                {
                    throw new Exception("Элемент не найден");
                }
                if (element.Status != OrderStatus.Принят)
                {
                    throw new Exception("Заказ не в статусе \"Принят\"");
                }
                var productComponents = context.ProductComponents.Include(rec =>
                rec.Component).Where(rec => rec.ProductId == element.ProductId);
                // списываем
                foreach (var productComponent in productComponents)
                {
                    int countOnStocks = productComponent.Count * element.Count;
                    var stockComponents = context.StockComponents.Where(rec =>
                    rec.ComponentId == productComponent.ComponentId);
                    foreach (var stockComponent in stockComponents)
                    {
                        // компонентов на одном слкаде может не хватать
                        if (stockComponent.Count >= countOnStocks)
                        {
                            stockComponent.Count -= countOnStocks;
                            countOnStocks = 0;
                            context.SaveChanges();
                            break;
                        }
                        else
                        {
                            countOnStocks -= stockComponent.Count;
                            stockComponent.Count = 0;
                            context.SaveChanges();
                        }
                    }
                    if (countOnStocks > 0)
                    {
                        throw new Exception("Не достаточно компонента " +
                        productComponent.Component.ComponentName + " требуется " + productComponent.Count + ", не
                        хватает " + countOnStocks);
                    }
                }
                element.DateImplement = DateTime.Now;
                element.Status = OrderStatus.Выполняется;
                context.SaveChanges();
                transaction.Commit();
            }
            catch (Exception)
            {
                transaction.Rollback();
                throw;
            }
        }
    }
    public void FinishOrder(OrderBindingModel model)
    {
        Order element = context.Orders.FirstOrDefault(rec => rec.Id == model.Id);
        if (element == null)
        {
            throw new Exception("Элемент не найден");
        }
        if (element.Status != OrderStatus.Выполняется)
        {
            throw new Exception("Заказ не в статусе \"Выполняется\"");
        }
        element.Status = OrderStatus.Готов;
        context.SaveChanges();
    }
    public void PayOrder(OrderBindingModel model)
    {
        Order element = context.Orders.FirstOrDefault(rec => rec.Id == model.Id);
        if (element == null)
        {
            throw new Exception("Элемент не найден");
        }
        if (element.Status != OrderStatus.Готов)
        {
            throw new Exception("Заказ не в статусе \"Готов\"");
        }
        element.Status = OrderStatus.Оплачен;
        context.SaveChanges();
    }
    public void PutComponentOnStock(StockComponentBindingModel model)
    {
        StockComponent element = context.StockComponents.FirstOrDefault(rec =>
        rec.StockId == model.StockId && rec.ComponentId == model.ComponentId);
        if (element != null)
        {
            element.Count += model.Count;
        }
        else
        {
            context.StockComponents.Add(new StockComponent
            {
                StockId = model.StockId,
                ComponentId = model.ComponentId,
                Count = model.Count
            });
        }
        context.SaveChanges();
    }
}
