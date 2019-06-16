using System.Collections.Generic;
using ZakusochnayaServiceDAL.BindingModel;
using ZakusochnayaServiceDAL.ViewModel;

namespace ZakusochnayaServiceDAL.Interfaces
{
    public interface IExecutorService
    {
        List<ExecutorViewModel> GetList();
        ExecutorViewModel GetElement(int id);
        void AddElement(ExecutorBindingModel model);
        void UpdElement(ExecutorBindingModel model);
        void DelElement(int id);
        ExecutorViewModel GetFreeWorker();
    }
}
