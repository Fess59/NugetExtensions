using FessooFramework.Objects;
using FessooFramework.Objects.Message;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FessooFramework.Tools.Web
{
    public class ServiceRequestConfig<TRequest, TResponse> : ServiceRequestConfigBase
        where TRequest : RequestMessageBase
        where TResponse : ResponseMessageBase
    {
        #region Property
        private Func<TRequest,TResponse> _Execute { get; set; }
        #endregion
        #region Constructor
        public ServiceRequestConfig() : base(typeof(TRequest))
        {

        }
        #endregion
        #region Methods
        public static ServiceRequestConfig<TRequest,TResponse> New(Func<TRequest,TResponse> action)
        {
            return new ServiceRequestConfig<TRequest, TResponse>()
            {
                _Execute = action
            };
        }
        public override object Execute(object obj)
        {
            var result = default(TResponse);
            if (_Execute == null)
                throw new NullReferenceException($"ServiceRequestConfig для типа {typeof(TRequest).ToString()} не корретно определён метод");
            result = _Execute((TRequest)obj);
            if (result == null)
                throw new NullReferenceException($"ServiceResponseConfig для типа {typeof(TRequest).ToString()} не корретно определён метод, Response не должен быть NULL. Треубеться вернуть ответ в любом случае");
            return result;
        }
        #endregion
    }
}
