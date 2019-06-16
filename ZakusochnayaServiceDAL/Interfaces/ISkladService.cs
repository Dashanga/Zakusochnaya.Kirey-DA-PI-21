using System.Collections.Generic;
using ZakusochnayaServiceDAL.Attributies;
using ZakusochnayaServiceDAL.BindingModel;
using ZakusochnayaServiceDAL.ViewModel;

namespace ZakusochnayaServiceDAL.Interfaces
{
    [CustomInterface("Интерфейс для работы со складами")]
    public interface ISkladService
    {
        [CustomMethod("Метод для получения списка складов")]
        List<SkladViewModel> GetList();
        [CustomMethod("Метод для получения склада по id")]
        SkladViewModel GetElement(int id);
        [CustomMethod("Метод для добавления склада")]
        void AddElement(SkladBindingModel model);
        [CustomMethod("Метод для изменения склада")]
        void UpdElement(SkladBindingModel model);
        [CustomMethod("Метод для удаления склада")]
        void DelElement(int id);
    }
}
