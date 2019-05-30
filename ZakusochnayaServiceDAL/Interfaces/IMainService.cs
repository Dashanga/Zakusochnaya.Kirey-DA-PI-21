using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZakusochnayaServiceDAL.Attributies;
using ZakusochnayaServiceDAL.BindingModel;
using ZakusochnayaServiceDAL.BindingModels;
using ZakusochnayaServiceDAL.ViewModels;

namespace ZakusochnayaServiceDAL.Interfaces
{
    [CustomInterface("Интерфейс для работы с заказами")]
    public interface IMainService
    {
        [CustomMethod("Метод получения списка заказов")]
        List<ZakazViewModel> GetList();
        [CustomMethod("Метод получения списка свободных заказов")]
        List<ZakazViewModel> GetFreeOrders();
        [CustomMethod("Метод создания заказа")]
        void CreateOrder(ZakazBindingModel model);
        [CustomMethod("Метод принятия заказа")]
        void TakeOrderInWork(ZakazBindingModel model);
        [CustomMethod("Метод выполнения заказа")]
        void FinishOrder(ZakazBindingModel model);
        [CustomMethod("Метод оплаты заказа")]
        void PayOrder(ZakazBindingModel model);
        [CustomMethod("Метод добавления ингредиентов на склад")]
        void PutComponentOnSklad(SkladElementBindingModel model);
    }
}
