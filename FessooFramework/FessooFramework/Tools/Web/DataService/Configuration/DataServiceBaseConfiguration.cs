using FessooFramework.Objects;
using FessooFramework.Objects.Data;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FessooFramework.Tools.Web.DataService.Configuration
{
    public abstract class DataServiceConfigurationBase : SystemObject
    {
        public abstract Type DataType { get; }
        public abstract Type CacheType { get; }
        public abstract CacheObject ObjectLoad(Guid Id);
        public abstract IEnumerable<CacheObject> CollectionObjectLoad();
        public abstract IEnumerable<CacheObject> ObjectSave(IEnumerable<CacheObject> objs);
        internal abstract IEnumerable<CacheObject> _CustomCollectionLoad(string code, string sessionUID = "", string hashUID = "", string JSONObjectCollections = null, IEnumerable<Guid> id = null);
        internal abstract IEnumerable<CacheObject> _JSONGetCollection(string JSONObjectCollections);
    }

    public class DataServiceConfiguration<TDataModel, TCacheModel> : DataServiceConfigurationBase
        where TDataModel : EntityObject, new()
        where TCacheModel : CacheObject
    {
        public override Type DataType => typeof(TDataModel);

        public override Type CacheType => typeof(TCacheModel);
        #region Object load
        public override CacheObject ObjectLoad(Guid id)
        {
            return _ObjectLoad(id);
        }
        private TCacheModel _ObjectLoad(Guid id)
        {
            var data = new TDataModel();
            var entity = data._ObjectLoadById(id);
            var entityModel = (TDataModel)entity;
            var cacheModel = EntityToCache(entityModel);
            return cacheModel;
        }
        #endregion
        #region Object load
        public override IEnumerable<CacheObject> CollectionObjectLoad()
        {
            return _CollectionObjectLoad();
        }
        private IEnumerable<CacheObject> _CollectionObjectLoad()
        {
            var data = new TDataModel();
            var entitys = data._CollectionObjectLoad();
            var entityModels = (TDataModel[])entitys;
            var cacheModels = EntityToCache(entityModels);
            return cacheModels;
        }
        #endregion
        #region SaveChanges
        public override IEnumerable<CacheObject> ObjectSave(IEnumerable<CacheObject> objs)
        {
            return _ObjectSave(objs);
        }
        private IEnumerable<TCacheModel> _ObjectSave(IEnumerable<CacheObject> objs)
        {
            var collections = Enumerable.Empty<TCacheModel>();
            DCT.DCT.Execute(c =>
            {
                var dataModels = CacheToEntity(objs);
                var data = new TDataModel();
                var entitys = data._CacheSave(dataModels.ToArray());
                collections = dataModels.Select(q => q._ConvertToServiceModel<TCacheModel>());
            });
            return collections;
        }
        #endregion
        #region Custom query
        internal override IEnumerable<CacheObject> _CustomCollectionLoad(string code, string sessionUID = "", string hashUID = "", string JSONObjectCollections = null, IEnumerable<Guid> id = null)
        {
            var result = Enumerable.Empty<TCacheModel>();
            DCT.DCT.Execute(c =>
            {
                var collection = _JSONGetCollection(JSONObjectCollections);
                var data = new TDataModel();
                var dataCollection = CacheToEntity(collection);
                var entityCollection = data.CustomCollectionLoad(code, sessionUID, hashUID, dataCollection, id);
                result =  entityCollection.Select(q => q._ConvertToServiceModel<TCacheModel>());
            });
            return result;
        }
        #endregion
        #region Tools
        private IEnumerable<TDataModel> CacheToEntity(IEnumerable<CacheObject> objs)
        {
            var result = Enumerable.Empty<TDataModel>();
            if (objs.Any())
            {
                   var dataModels = new List<TDataModel>();
                var data = new TDataModel();
                foreach (var obj in objs)
                    dataModels.Add((TDataModel)data._ConvertToDataModel((TCacheModel)obj));
                result = dataModels.ToArray();
            }
            return result;
        }
        private TCacheModel EntityToCache(TDataModel obj)
        {
            return obj._ConvertToServiceModel<TCacheModel>();
        }
        private IEnumerable<TCacheModel> EntityToCache(IEnumerable<TDataModel> objs)
        {
            return objs.Select(q => q._ConvertToServiceModel<TCacheModel>()).ToArray();
        }

        internal override IEnumerable<CacheObject> _JSONGetCollection(string JSONObjectCollections)
        {
            var collection = Enumerable.Empty<TCacheModel>();
            if (!string.IsNullOrWhiteSpace(JSONObjectCollections))
            {
                var objs = JsonConvert.DeserializeObject(JSONObjectCollections, typeof(TCacheModel[]));
                collection = (IEnumerable<TCacheModel>)objs;
            }
            return collection;
        }
        #endregion
    }
}
