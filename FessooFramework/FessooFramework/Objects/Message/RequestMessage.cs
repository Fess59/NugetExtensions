using FessooFramework.Objects.Data;
using FessooFramework.Tools.Web;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FessooFramework.Objects.Message
{
    public abstract class RequestMessage<TRequest, TResponse> : RequestMessageBase
        where TRequest : RequestMessageBase
        where TResponse : ResponseMessageBase
    {
        public override object _Execute(ServiceBaseAPI service, object obj)
        {
            return service.ExecuteResponse<TRequest, TResponse>((TRequest)obj);
        }
    }

    public class DataRequestMessage<TRequest, TResponse> : RequestMessage<TRequest, TResponse>
        where TRequest : RequestMessageBase
        where TResponse : ResponseMessageBase
    {
        public string CurrentType { get; set; }
    }
}
