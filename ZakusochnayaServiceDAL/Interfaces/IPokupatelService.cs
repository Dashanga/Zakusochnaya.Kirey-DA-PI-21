using System.Collections.Generic;
using ZakusochnayaServiceDAL.ViewModel;
using ZakusochnayaServiceDAL.BindingModel;

namespace ZakusochnayaServiceDAL
{
    public interface IPokupatelService
    {
        List<PokupatelViewModel> GetList();
        PokupatelViewModel GetElement(int id);
        void AddElement(PokupatelBindingModel model);
        void UpdElement(PokupatelBindingModel model);
        void DelElement(int id);
    }
}
