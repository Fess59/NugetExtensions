using FessooFramework.Objects;
using FessooFramework.Objects.Message;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FessooFramework.Tools.Web
{
    public class ServiceResponseConfig<TResponse> : ServiceResponseConfigBase
        where TResponse : ResponseMessageBase
    {
        #region Property
        private Action<TResponse> _Execute { get; set; }
        #endregion
        #region Constructor
        public ServiceResponseConfig() : base(typeof(TResponse))
        {

        }
        #endregion
        #region Methods
        public static ServiceResponseConfig<TResponse> New(Action<TResponse> action)
        {
            return new ServiceResponseConfig<TResponse>()
            {
                _Execute = action
            };
        }
        public override void Execute(object obj)
        {
            if (_Execute == null)
                throw new NullReferenceException($"ServiceResponseConfig для типа {typeof(TResponse).ToString()} не корретно определён метод");
            _Execute((TResponse)obj);
        }
        #endregion
    }
}
