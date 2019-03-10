using System.Collections.Generic;
using ZakusochnayaServiceDAL.BindingModel;
using ZakusochnayaServiceDAL.ViewModel;

namespace ZakusochnayaServiceDAL.Interfaces
{
    public interface ISkladService
    {
        List<SkladViewModel> GetList();
        SkladViewModel GetElement(int id);
        void AddElement(SkladBindingModel model);
        void UpdElement(SkladBindingModel model);
        void DelElement(int id);
    }
}
