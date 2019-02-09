using System.Collections.Generic;
using ZakusochnayaServiceDAL.ViewModel;
using ZakusochnayaServiceDAL.BindingModel;

namespace ZakusochnayaServiceDAL
{
    public interface IClientService
    {
        List<ClientViewModel> GetList();
        ClientViewModel GetElement(int id);
        void AddElement(ClientBindingModel model);
        void UpdElement(ClientBindingModel model);
        void DelElement(int id);
    }
}
