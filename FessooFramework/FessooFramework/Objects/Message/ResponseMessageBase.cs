using FessooFramework.Tools.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FessooFramework.Objects.Message
{
    public class ResponseMessageBase : MessageObject
    {
        public int State { get; set; }
        public string Comment { get; set; }
        public string Error { get; set; }
        public MessageState StateEnum
        {
            get => EnumHelper.GetValue<MessageState>(State);
            set => State = (int)value;
        }
    }
    public enum MessageState
    {
        None = 0,
        NotSuccesfull = 1,
        Succesfull = 2,
        Error = 3,
        Blocked = 4,//Все запросы клиента не обрабатываеются
        SessionNotValid = 5, //Клиенту требуется получить новую сессию MainAPI.Auth - ошибка при получении новой сессии означает разлогирование для клиента
    }
}
