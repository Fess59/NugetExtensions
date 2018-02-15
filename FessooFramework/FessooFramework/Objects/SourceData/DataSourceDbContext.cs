using FessooFramework.Tools.Helpers;
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
                {
                    source = new TContext();
                    if (HasRemoteServer)
                        source.Database.Connection.ConnectionString = EntityHelper.CreateRemoteSQL(DbName, Address, Login, Password);
                    else
                        source.Database.Connection.ConnectionString = EntityHelper.CreateLocalSQL(DbName);
                }

                return source;
            }
        }
        private TContext source { get; set; }
        private bool HasRemoteServer { get; set; }
        private string DbName { get; set; }
        private string Address { get; set; }
        private string Login { get; set; }
        private string Password { get; set; }
        #endregion
        #region Constructor
        public DataSourceDbContext(string dbName, string address, string login, string password)
        {
            HasRemoteServer = true;
            DbName = dbName;
            Address = address;
            Login = login;
            Password = password;
        }
        public DataSourceDbContext(string name)
        {
            HasRemoteServer = false;
            DbName = name;
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
