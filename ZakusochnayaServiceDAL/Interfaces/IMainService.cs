﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZakusochnayaServiceDAL.BindingModel;
using ZakusochnayaServiceDAL.BindingModels;
using ZakusochnayaServiceDAL.ViewModels;

namespace ZakusochnayaServiceDAL.Interfaces
{
    public interface IMainService
    {
        List<ZakazViewModel> GetList();
        List<ZakazViewModel> GetFreeOrders();
        void CreateOrder(ZakazBindingModel model);
        void TakeOrderInWork(ZakazBindingModel model);
        void FinishOrder(ZakazBindingModel model);
        void PayOrder(ZakazBindingModel model);
        void PutComponentOnStock(SkladElementBindingModel model);
    }
}
