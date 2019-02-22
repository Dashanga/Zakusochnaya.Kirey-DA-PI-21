using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZakusochnayaServiceDAL.BindingModel;
using ZakusochnayaServiceDAL.ViewModels;

namespace ZakusochnayaServiceDAL.Interfaces
{
    public interface IOrderStatusService
    {
        List<OrderStatusViewModel> GetList();
        OrderStatusViewModel GetElement(int id);
        void AddElement(OrderStatusBindingModel model);
        void UpdElement(OrderStatusBindingModel model);
        void DelElement(int id);
    }
}
