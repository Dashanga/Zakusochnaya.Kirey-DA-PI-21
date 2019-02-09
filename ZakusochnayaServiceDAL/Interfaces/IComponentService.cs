using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZakusochnayaServiceDAL.BindingModel;
using ZakusochnayaServiceDAL.ViewModel;

namespace ZakusochnayaServiceDAL.Interfaces
{
    public interface IComponentService
    {
        List<ComponentViewModel> GetList();
       ComponentViewModel GetElement(int id);
        void AddElement(ComponentBindingModel model);
        void UpdElement(ComponentBindingModel model);
        void DelElement(int id);
    }
}
