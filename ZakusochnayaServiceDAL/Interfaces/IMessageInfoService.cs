using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZakusochnayaServiceDAL.Attributies;
using ZakusochnayaServiceDAL.BindingModel;
using ZakusochnayaServiceDAL.ViewModel;

namespace ZakusochnayaServiceDAL.Interfaces
{
    [CustomInterface("Интерфейс для работы с сообщениями")]
    public interface IMessageInfoService
    {
        [CustomMethod("Метод для получения списка сообщений")]
        List<MessageInfoViewModel> GetList();
        [CustomMethod("Метод для получения сообщения по идентификатору")]
        MessageInfoViewModel GetElement(int id);
        [CustomMethod("Метод для добавления сообщения")]
        void AddElement(MessageInfoBindingModel model);
    }
}
