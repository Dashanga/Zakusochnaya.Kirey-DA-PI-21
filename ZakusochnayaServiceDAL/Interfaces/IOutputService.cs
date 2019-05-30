using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZakusochnayaServiceDAL.Attributies;
using ZakusochnayaServiceDAL.BindingModels;
using ZakusochnayaServiceDAL.ViewModels;

namespace ZakusochnayaServiceDAL.Interfaces
{
    [CustomInterface("Интерфейс для работы с продуктами")]
    public interface IOutputService
    {
        [CustomMethod("Метод для получения списка продуктов")]
        List<OutputViewModel> GetList();
        [CustomMethod("Метод для получения продукта по id")]
        OutputViewModel GetElement(int id);
        [CustomMethod("Метод для получения добавления продуктов")]
        void AddElement(OutputBindingModel model);
        [CustomMethod("Метод для изменения продукта")]
        void UpdElement(OutputBindingModel model);
        [CustomMethod("Метод для удаления продукта")]
        void DelElement(int id);
    }
}
