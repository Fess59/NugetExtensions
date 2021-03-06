﻿using FessooFramework.Objects;
using FessooFramework.Objects.Message;
using FessooFramework.Tools.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace FessooFramework.Tools.Web
{
    public abstract class BaseServiceClient : SystemObject
    {
        #region Property
        public abstract string Address { get;}
        public abstract TimeSpan PostTimeout { get; }
        public abstract string HashUID { get; }
        public abstract string SessionUID { get; }

        private static string ClassName { get; set; }
        #endregion
        #region Methods
        public BaseServiceClient()
        {
            if (_Configurations == null)
            {
                _Configurations = Configurations;
                ClassName = this.GetType().ToString();
            }
        }
        #endregion
        #region Service methods
        public TResponse Execute<TRequest, TResponse>(TRequest request)
           where TRequest : RequestMessageBase
          where TResponse : ResponseMessageBase
        {
            var result = default(TResponse);
            DCT.DCT.Execute(c =>
            {
                if (c._SessionInfo.HashUID == "")
                    c._SessionInfo.HashUID = HashUID;
                if (c._SessionInfo.SessionUID == "")
                    c._SessionInfo.SessionUID = SessionUID;

                var message = ServiceMessage.New(request);
                message.FullName = typeof(TRequest).FullName;
                var response = WebHelper.SendPOST<ServiceMessage>(message, Address + @"/Execute");
                result = response.Desirialize<TResponse>();
                ExecuteResponse(result);
            });
            return result;
        }
        public void Execute<TRequest, TResponse>(TRequest request, Action<TResponse> complite)
          where TRequest : RequestMessageBase
         where TResponse : ResponseMessageBase
        {
            var result = default(TResponse);
            DCT.DCT.ExecuteAsyncQueue<TResponse>(c =>
            {
                var message = ServiceMessage.New(request);
                message.FullName = typeof(TRequest).FullName;
                var response = WebHelper.SendPOST<ServiceMessage>(message, Address + @"/Execute");
                result = response.Desirialize<TResponse>();
                ExecuteResponse(result);
                return result;
            }, (c,a)=>complite(a));
        }
        public bool Ping()
        {
            var result = false;
            DCT.DCT.Execute(c =>
            {
                var response = WebHelper.SendPOST<bool>("Ping", Address + @"/Ping");
                result = response;
            });
            return result;
        }
        #endregion
        #region Configuration
        protected abstract IEnumerable<ServiceResponseConfigBase> Configurations { get; }
        private static IEnumerable<ServiceResponseConfigBase> _Configurations { get; set; }
        private static bool ExecuteResponse<TResponse>(TResponse obj)
        {
            var result = false;
            DCT.DCT.Execute(c =>
            {
                if (!_Configurations.Any())
                {
                    var configuration = _Configurations.SingleOrDefault(q => q.CurrentType == typeof(TResponse));
                    if (configuration != null)
                        configuration.Execute(obj);
                    //throw new Exception($"ServiceClient - для клиента службы {ClassName}, нет ни одной реализации для {typeof(TResponse).ToString()}");
                }
            });
            return result;
        }
        #endregion

    }
}
