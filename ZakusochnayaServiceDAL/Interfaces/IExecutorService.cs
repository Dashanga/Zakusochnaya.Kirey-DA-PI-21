using System.Collections.Generic;
using ZakusochnayaServiceDAL.Attributies;
using ZakusochnayaServiceDAL.BindingModel;
using ZakusochnayaServiceDAL.ViewModel;

namespace ZakusochnayaServiceDAL.Interfaces
{
    [CustomInterface("Интерфейс для работы с исполнителями")]
    public interface IExecutorService
    {
        [CustomMethod("Метод получения списка исполнителей")]
        List<ExecutorViewModel> GetList();
        [CustomMethod("Метод получения исполнителя по id")]
        ExecutorViewModel GetElement(int id);
        [CustomMethod("Метод добавления исполнителя")]
        void AddElement(ExecutorBindingModel model);
        [CustomMethod("Метод изменения данных по исполнителю")]
        void UpdElement(ExecutorBindingModel model);
        [CustomMethod("Метод удаления исполнителя")]
        void DelElement(int id);
        [CustomMethod("Метод получения наимение занятого исполнителя")]
        ExecutorViewModel GetFreeWorker();
    }
}
