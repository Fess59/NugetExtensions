using FessooFramework.Objects.Data;
using FessooFramework.Tools.Helpers;
using FessooFramework.Tools.Web;
using Newtonsoft.Json;
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
        #region Property
        public IEnumerable<Guid> Ids { get; set; }
        public string CurrentType { get; set; }
        #endregion
        #region Object collections
        public string JSONObjectCollections { get; set; }
        public string JSONObjectCollectionsType { get; set; }
        public void SetObjectCollections(object obj)
        {
            var type = obj.GetType();
            JSONObjectCollectionsType = type.AssemblyQualifiedName;
            JSONObjectCollections = JsonConvert.SerializeObject(obj);
        }
        public IEnumerable<TCacheType> GetObjectCollections<TCacheType>()
        {
            var obj = JsonConvert.DeserializeObject(JSONObjectCollections, typeof(TCacheType[]));
            return (IEnumerable<TCacheType>)obj;
        }
        #endregion
    }
}
