using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZakusochnayaServiceDAL.BindingModels;
using ZakusochnayaServiceDAL.ViewModels;

namespace ZakusochnayaServiceDAL.Interfaces
{
    public interface IProductComponentService
    {
        List<ProductComponentViewModel> GetList();
        ProductComponentViewModel GetElement(int id);
        void AddElement(ProductComponentBindingModel model);
        void UpdElement(ProductComponentBindingModel model);
        void DelElement(int id);
    }
}
