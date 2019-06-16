using System;
using System.Collections.Generic;
using ZakusochnayaServiceDAL.Attributies;
using ZakusochnayaServiceDAL.BindingModel;
using ZakusochnayaServiceDAL.ViewModel;

namespace ZakusochnayaServiceDAL.Interfaces
{
    [CustomInterface("Интерфейс для работы с ингредиентами")]
    public interface IElementService
    {
        [CustomMethod("Метод получения списка ингредиентов")]
        List<ElementViewModel> GetList();
        [CustomMethod("Метод получения ингредиента по id")]
        ElementViewModel GetElement(int id);
        [CustomMethod("Метод добавления ингредиента")]
        void AddElement(ElementBindingModel model);
        [CustomMethod("Метод изменения данных по ингредиенту")]
        void UpdElement(ElementBindingModel model);
        [CustomMethod("Метод удаления ингредиента")]
        void DelElement(int id);
    }
}
