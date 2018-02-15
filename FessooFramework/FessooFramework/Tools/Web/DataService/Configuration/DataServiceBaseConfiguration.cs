using FessooFramework.Objects;
using FessooFramework.Objects.Data;
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
        #region Tools
        private TCacheModel EntityToCache(TDataModel obj)
        {
            return obj._ConvertToServiceModel<TCacheModel>();
        }
        private IEnumerable<TCacheModel> EntityToCache(IEnumerable<TDataModel> objs)
        {
            return objs.Select(q => q._ConvertToServiceModel<TCacheModel>()).ToArray();
        }

       
        #endregion
    }
}
