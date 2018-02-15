﻿using FessooFramework.Objects;
using FessooFramework.Objects.Message;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FessooFramework.Tools.Web
{
    public abstract class ServiceBaseAPI : SystemObject
    {
        #region Property
        public abstract string Name { get; }
        private static string ClassName { get; set; }
        #endregion
        #region Constructor
        public ServiceBaseAPI()
        {
            //Инициализация и первичная загрузка из абстрактного класса
            if (_Configurations == null)
            {
                _Configurations = Configurations;
                ClassName = this.GetType().ToString();
            }
        }
        #endregion
        #region Methods
        public abstract bool Ping(string p);
        public static bool _Ping(string p)
        {
            return true;
        }
        public abstract string Stat();
        public static string _Stat(ServiceBaseAPI obj)
        {
            return $"Service {obj.Name} created in {obj.CreateDate.ToLongTimeString() + Environment.NewLine} Life time - {TimeSpan.FromTicks(DateTime.Now.Ticks - obj.CreateDate.Ticks).ToString()}";
        }
        public abstract ServiceMessage Execute(ServiceMessage data);
        /// <summary>
        /// Обработка и ответ сообщения от клиента
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        public ServiceMessage _Execute( ServiceMessage data)
        {
            var result = default(ServiceMessage);
            DCT.DCT.Execute(c => {
                var obj = (RequestMessageBase)data.Desirialize();
                var response = obj._Execute(this ,obj);
                if (response == null)
                    throw new NullReferenceException($"Request - {data.FullName}. Ошибка ответа - Response не может быть null, если метод вызывает асинхронно, необходимо вернуть ответ о успешно принятом сообщении");
                //var typeResponse = CheckResponseType(serviceInstance, response.GetType().FullName);
                result = ServiceMessage.New(response);
            });
            return result;
        }
        //private static Type CheckRequestType(ServiceBaseAPI serviceInstance, string fullNameClass)
        //{
        //    var type = serviceInstance.GetTypeByFullName(fullNameClass);
        //    if (type == null)
        //        throw new NullReferenceException($"Ошибка при получении Request, тип '{fullNameClass}' - не найден в этом проекте или проектах на которые есть ссылки");
        //    var basedType = type.BaseType.FullName;
        //    if (type.BaseType.FullName != typeof(RequestMessageBase).FullName)
        //        throw new Exception($"Не корректная реализация {fullNameClass}. Объект запроса должен быть унаследован от {typeof(RequestMessageBase).FullName}");
        //    return type;
        //}
        //private static Type CheckResponseType(ServiceBaseAPI serviceInstance, string fullNameClass)
        //{
        //    var type = serviceInstance.GetTypeByFullName(fullNameClass);
        //    if (type == null)
        //        throw new NullReferenceException($"Ошибка при отправке Response, тип '{fullNameClass}' - не найден в этом проекте или проектах на которые есть ссылки");
        //    if (type.BaseType.FullName != typeof(ResponseMessageBase).FullName)
        //        throw new Exception($"Не корректная реализация {fullNameClass}. Объект запроса должен быть унаследован от {typeof(ResponseMessageBase).FullName}");
        //    return type;
        //}
        #endregion
        #region Configurations
        protected abstract IEnumerable<ServiceRequestConfigBase> Configurations { get; }
        private static IEnumerable<ServiceRequestConfigBase> _Configurations { get; set; }
        private IEnumerable<ServiceRequestConfigBase> configurations
        {
            get
            {
                if (_Configurations == null)
                    _Configurations = Configurations;
                return _Configurations;
            }
        }
        internal TResponse ExecuteResponse<TRequest, TResponse>(TRequest obj)
        {
            var result = default(TResponse);
            DCT.DCT.Execute(c =>
            {
                if (!_Configurations.Any())
                    throw new Exception($"ServiceClient - для клиента службы {ClassName}, нет ни одной реализаций Response");
                var configuration = _Configurations.SingleOrDefault(q => q.CurrentType == typeof(TRequest));
                if (configuration == null)
                    throw new Exception($"ServiceClient - для клиента службы {ClassName}, нет ни одной реализации для {typeof(TResponse).ToString()}");
                result = (TResponse)configuration.Execute(obj);
            });
            return result;
        }
        #endregion
    }
}