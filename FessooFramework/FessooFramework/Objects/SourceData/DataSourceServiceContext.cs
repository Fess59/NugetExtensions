using FessooFramework.Tools.Helpers;
using FessooFramework.Tools.IOC;
using FessooFramework.Tools.Web.DataService;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Core.Objects.DataClasses;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FessooFramework.Objects.SourceData
{
    internal class DataSourceServiceContext<TContext> : DataSourceServiceBase
         where TContext : DataServiceClient, new()
    {
        #region Property
        private TContext Source
        {
            get
            {
                if (source == null)
                    source = new TContext();
                return source;
            }
        }
        private TContext source { get; set; }
        #endregion
        #region Constructor
        public DataSourceServiceContext()
        {
         
        }
        #endregion
        #region Methods
        #endregion
        #region Abstraction
        public override DataServiceClient GetContext()
        {
            return Source;
        }
        protected override bool IsCreated => source != null;
        public override Type CurrentType => typeof(TContext);
        #endregion
    }
}
