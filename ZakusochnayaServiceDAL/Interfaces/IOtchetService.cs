using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZakusochnayaServiceDAL.Attributies;
using ZakusochnayaServiceDAL.BindingModel;
using ZakusochnayaServiceDAL.ViewModel;

namespace ZakusochnayaServiceDAL.Interfaces
{
    [CustomInterface("Методя для работы с отчетами")]
    public interface IOtchetService
    {
        [CustomMethod("Метод для сохранения прайс-листа")]
        void SaveProductPrice(OtchetBindingModel model);
        [CustomMethod("Метод для получения загрузки склада")]
        List<SkladsLoadViewModel> GetStocksLoad();
        [CustomMethod("Метод для сохранения загрузки складов")]
        void SaveStocksLoad(OtchetBindingModel model);
        [CustomMethod("Метод для получения заказов")]
        List<PokupatelZakazsModel> GetClientOrders(OtchetBindingModel model);
        [CustomMethod("Метод для сохранения заказов")]
        void SaveClientOrders(OtchetBindingModel model);
    }
}
