using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZakusochnayaServiceDAL.BindingModels;
using ZakusochnayaServiceDAL.ViewModels;

namespace ZakusochnayaServiceDAL.Interfaces
{
    public interface IOutputService
    {
        List<OutputViewModel> GetList();
        OutputViewModel GetElement(int id);
        void AddElement(OutputBindingModel model);
        void UpdElement(OutputBindingModel model);
        void DelElement(int id);
    }
}
