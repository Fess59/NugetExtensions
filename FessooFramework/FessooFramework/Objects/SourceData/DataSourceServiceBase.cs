using FessooFramework.Objects.Data;
using FessooFramework.Tools.IOC;
using FessooFramework.Tools.Web.DataService;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FessooFramework.Objects.SourceData
{
    internal abstract class DataSourceServiceBase : _IOCElement
    {
        #region Property
        public abstract DataServiceClient GetContext();
        protected abstract bool IsCreated { get; }
        public abstract Type CurrentType { get; }
        #endregion
        #region Constructor
        public DataSourceServiceBase()
        {
            UID = CurrentType.ToString();
        }
        #endregion
        #region Methods
        public void ObjectLoad<TCacheObject>(Guid id, Action<TCacheObject> callback) where TCacheObject : CacheObject
        {
            GetContext().ObjectLoad<TCacheObject>(callback, id);
        }
        public void ObjectCollection<TCacheObject>(Action<IEnumerable<TCacheObject>> callback) where TCacheObject : CacheObject
        {
            GetContext().CollectionLoad<TCacheObject>(callback);
        }
        public void SaveChanges()
        {
            //if (IsCreated)
            //    GetContext().SaveChanges();
        }
        public override void Dispose()
        {
            base.Dispose();
            if (IsCreated)
                GetContext().Dispose();
        }
        #endregion

    }
}
