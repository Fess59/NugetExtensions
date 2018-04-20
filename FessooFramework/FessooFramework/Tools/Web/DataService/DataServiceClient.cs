using FessooFramework.Objects.Data;
using FessooFramework.Tools.Helpers;
using FessooFramework.Tools.Web.DataService.ServiceModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FessooFramework.Tools.Web.DataService
{
    public abstract class DataServiceClient : BaseServiceClient
    {
        #region Abstractions
        protected override IEnumerable<ServiceResponseConfigBase> Configurations => new ServiceResponseConfigBase[] { };
        #endregion
        #region Send query
        public void SendQueryCollection<TCacheObject>(Action<IEnumerable<TCacheObject>> callback, string code, string sessionUID = "", string hashUID = "", IEnumerable<TCacheObject> objects = null, IEnumerable<Guid> ids = null, bool hasCollection = true)
            where TCacheObject : CacheObject
        {
            DCT.DCT.ExecuteAsyncQueue(c =>
            {
                var result = Enumerable.Empty<TCacheObject>();
                var request = new RequestGet();
                request.CurrentType = typeof(TCacheObject).AssemblyQualifiedName;
                request.HashUID = hashUID;
                request.SessionUID = sessionUID;
                request.QueryType = code;
                request.HasCollection = hasCollection;
                request.Ids = ids;
                if (objects != null)
                    request.SetObjectCollections(objects);
                var response = Execute<RequestGet, ResponseGet>(request);
                //Console.WriteLine($"SessionUID Reponse = {response.SessionUID}");
                //Console.WriteLine($"HashUID Response = {response.HashUID}");
                result = response.GetObjectCollections<TCacheObject>().ToArray();
                return result;
            }, complete: (c, r) =>
            {
                if (callback == null)
                    ConsoleHelper.Send("DataServiceClient", "В методе CollectionLoad не реализован Callback");
                else
                    callback(r);
            });
        }
        public void SendQueryObject<TCacheObject>(Action<TCacheObject> callback, string code, TCacheObject obj = null, string sessionUID = "", string hashUID = "", Guid? id = null)
           where TCacheObject : CacheObject
        {
            SendQueryCollection(result => callback(result.FirstOrDefault()), code, sessionUID, hashUID, obj == null ? null : new[] { obj }, id == null ? null : new[] { id.Value }, hasCollection: false);
        }
        #endregion
        #region CollectionLoad
        public void CollectionLoad<TCacheObject>(Action<IEnumerable<TCacheObject>> callback)
           where TCacheObject : CacheObject
        {
            DCT.DCT.Execute(c =>
            {
                SendQueryCollection(callback, "_CollectionLoad", sessionUID: c._SessionInfo.SessionUID, hashUID: c._SessionInfo.HashUID);
            });
        }
        public void ObjectLoad<TCacheObject>(Action<TCacheObject> callback, Guid id)
  where TCacheObject : CacheObject
        {
            DCT.DCT.Execute(c =>
            {
                SendQueryObject(callback, "_ObjectLoad", sessionUID: c._SessionInfo.SessionUID, hashUID: c._SessionInfo.HashUID, id: id);
            });
        }
        public void Save<TCacheObject>(Action<TCacheObject> callback, TCacheObject obj)
        where TCacheObject : CacheObject
        {
            DCT.DCT.Execute(c =>
            {
                SendQueryObject(callback, "_SaveChanges", obj:obj, sessionUID: c._SessionInfo.SessionUID, hashUID: c._SessionInfo.HashUID);
            });
        }
        public void Save<TCacheObject>(Action<IEnumerable<TCacheObject>> callback, IEnumerable<TCacheObject> objs)
         where TCacheObject : CacheObject
        {
            DCT.DCT.Execute(c =>
            {
                SendQueryCollection(callback, "_SaveChanges", objects: objs, sessionUID: c._SessionInfo.SessionUID, hashUID: c._SessionInfo.HashUID);
            });
        }
        #endregion





    }
}
