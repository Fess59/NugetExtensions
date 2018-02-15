using FessooFramework.Objects.Data;
using FessooFramework.Tools.Helpers;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FessooFramework.Objects.Message
{
    public abstract class ResponseMessage<TRequest, TResponse> : ResponseMessageBase
       where TRequest : RequestMessageBase
       where TResponse : ResponseMessageBase
    {

    }

    public class DataResponseMessage<TRequest, TResponse> : ResponseMessage<TRequest, TResponse>
      where TRequest : RequestMessageBase
      where TResponse : ResponseMessageBase
    {
        #region Object
        internal string JSONObject { get; set; }
        internal string JSONObjectType { get; set; }
        public void SetObject(object obj)
        {
            var type = obj.GetType();
            JSONObjectType = type.AssemblyQualifiedName;
            JSONObject = JsonConvert.SerializeObject(obj);
        }
        public TCacheType GetObject<TCacheType>()
        {
            var obj = JsonConvert.DeserializeObject(JSONObject, typeof(TCacheType));
            return (TCacheType)obj;
        }
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
