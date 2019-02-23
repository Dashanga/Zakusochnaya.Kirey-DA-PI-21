using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZakusochnayaServiceDAL.BindingModels;
using ZakusochnayaServiceDAL.ViewModels;

namespace ZakusochnayaServiceDAL.Interfaces
{
    public interface IOutputElementService
    {
        List<OutputElementViewModel> GetList();
        OutputElementViewModel GetElement(int id);
        void AddElement(OutputElementBindingModel model);
        void UpdElement(OutputElementBindingModel model);
        void DelElement(int id);
    }
}
