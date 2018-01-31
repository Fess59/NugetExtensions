using FessooFramework.Tools.IOC;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FessooFramework.Objects.SourceData
{
    public abstract class DataSourceBase : _IOCElement
    {
        public abstract DbContext GetContext();
        protected abstract bool IsCreated { get; }
        public abstract Type CurrentType { get; }
        public DataSourceBase()
        {
            UID = CurrentType.ToString();
        }
        public void SaveChanges()
        {
            if (IsCreated)
                GetContext().SaveChanges();
        }
        public override void Dispose()
        {
            base.Dispose();
            if (IsCreated)
                GetContext().Dispose();
        }
    }
}
