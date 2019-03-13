using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZakusochnayaServiceDAL.BindingModel;
using ZakusochnayaServiceDAL.ViewModel;

namespace ZakusochnayaServiceDAL.Interfaces
{
    public interface IOtchetService
    {
        void SaveProductPrice(OtchetBindingModel model);
        List<SkladsLoadViewModel> GetStocksLoad();
        void SaveStocksLoad(OtchetBindingModel model);
        List<PokupatelZakazsModel> GetClientOrders(OtchetBindingModel model);
        void SaveClientOrders(OtchetBindingModel model);
    }
}
