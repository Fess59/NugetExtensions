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
        #region Send without async
        public IEnumerable<TCacheObject> RSendQueryCollection<TCacheObject>(string code, IEnumerable<TCacheObject> objects = null, IEnumerable<Guid> ids = null, bool hasCollection = true)
        where TCacheObject : CacheObject
        {
            var result = Enumerable.Empty<TCacheObject>();
            DCT.DCT.Execute(c =>
            {
                var request = new RequestGet();
                request.CurrentType = typeof(TCacheObject).AssemblyQualifiedName;
                request.HashUID = c._SessionInfo.HashUID;
                request.SessionUID = c._SessionInfo.SessionUID;
                request.QueryType = code;
                request.HasCollection = hasCollection;
                request.Ids = ids;
                if (objects != null)
                    request.SetObjectCollections(objects);
                var response = Execute<RequestGet, ResponseGet>(request);
                result = response.GetObjectCollections<TCacheObject>().ToArray();
            });
            return result;
        }
        public TCacheObject RSendQueryObject<TCacheObject>(string code, TCacheObject obj = null, Guid? id = null)
           where TCacheObject : CacheObject
        {
            return RSendQueryCollection(code, obj == null ? null : new[] { obj }, id == null ? null : new[] { id.Value }, hasCollection: false).FirstOrDefault();
        }
        #endregion
        #region Send query async
        public void SendQueryCollection<TCacheObject>(string code, Action<IEnumerable<TCacheObject>> callback = null, IEnumerable<TCacheObject> objects = null, IEnumerable<Guid> ids = null, bool hasCollection = true)
            where TCacheObject : CacheObject
        {
            DCT.DCT.ExecuteAsyncQueue(c => RSendQueryCollection(code, objects, ids, hasCollection), 
                complete: (c, r) =>
            {
                if (callback != null)
                    callback(r);
            });
        }
        public void SendQueryObject<TCacheObject>(string code, Action<TCacheObject> callback = null, TCacheObject obj = null, Guid? id = null)
           where TCacheObject : CacheObject
        {
            SendQueryCollection(code, result => callback(result.FirstOrDefault()), obj == null ? null : new[] { obj }, id == null ? null : new[] { id.Value }, hasCollection: false);
        }
        #endregion
        #region CollectionLoad
        public void CollectionLoad<TCacheObject>(Action<IEnumerable<TCacheObject>> callback)
           where TCacheObject : CacheObject
        {
            DCT.DCT.Execute(c =>
            {
                SendQueryCollection("_CollectionLoad", callback);
            });
        }
        public void ObjectLoad<TCacheObject>(Action<TCacheObject> callback, Guid id)
  where TCacheObject : CacheObject
        {
            DCT.DCT.Execute(c =>
            {
                SendQueryObject("_ObjectLoad", callback);
            });
        }
        public void Save<TCacheObject>(Action<TCacheObject> callback, TCacheObject obj)
        where TCacheObject : CacheObject
        {
            DCT.DCT.Execute(c =>
            {
                SendQueryObject("_SaveChanges", callback, obj: obj);
            });
        }
        public void Save<TCacheObject>(Action<IEnumerable<TCacheObject>> callback, IEnumerable<TCacheObject> objs)
         where TCacheObject : CacheObject
        {
            DCT.DCT.Execute(c =>
            {
                SendQueryCollection("_SaveChanges", callback, objects: objs);
            });
        }
        #endregion
    }
}
