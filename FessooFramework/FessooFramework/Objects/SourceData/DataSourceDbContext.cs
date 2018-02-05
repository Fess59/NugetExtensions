using FessooFramework.Tools.IOC;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Core.Objects.DataClasses;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FessooFramework.Objects.SourceData
{
    internal class DataSourceDbContext<TContext> : DataSourceBase
         where TContext : DbContext, new()
    {
        #region Property
        private TContext Source
        {
            get
            {
                //TODO SystemObject ADD dispose state and commont stateconfiguration
                //if (State == SystemState.Unload)
                //    throw new Exception("Не возможно использовать объект, тк контекст данных был освобождём");
                if (source == null)
                    source = new TContext();
                return source;
            }
        }
        private TContext source { get; set; }
        #endregion
        #region Constructor
        public DataSourceDbContext()
        {
           
        }
        #endregion
        #region Methods

       
       


        #endregion
        #region Abstraction
        public override DbContext GetContext()
        {
            return Source;
        }
        protected override bool IsCreated => source != null;
        public override Type CurrentType => typeof(TContext);
        #endregion
    }
}
