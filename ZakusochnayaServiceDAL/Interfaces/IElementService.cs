using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZakusochnayaServiceDAL.BindingModel;
using ZakusochnayaServiceDAL.ViewModel;

namespace ZakusochnayaServiceDAL.Interfaces
{
    public interface IElementService
    {
        List<ElementViewModel> GetList();
       ElementViewModel GetElement(int id);
        void AddElement(ElementBindingModel model);
        void UpdElement(ElementBindingModel model);
        void DelElement(int id);
    }
}
