using System.Collections.Generic;
using ZakusochnayaServiceDAL.ViewModel;
using ZakusochnayaServiceDAL.BindingModel;
using ZakusochnayaServiceDAL.Attributies;

namespace ZakusochnayaServiceDAL
{
    [CustomInterface("Интерфейс для работы с клиентами")]
    public interface IPokupatelService
    {
        [CustomMethod("Метод получения списка клиентов")]
        List<PokupatelViewModel> GetList();
        [CustomMethod("Метод получения клиента по id")]
        PokupatelViewModel GetElement(int id);
        [CustomMethod("Метод добавления клиента")]
        void AddElement(PokupatelBindingModel model);
        [CustomMethod("Метод изменения данных по клиенту")]
        void UpdElement(PokupatelBindingModel model);
        [CustomMethod("Метод удаления клиента")]
        void DelElement(int id);
    }
}
