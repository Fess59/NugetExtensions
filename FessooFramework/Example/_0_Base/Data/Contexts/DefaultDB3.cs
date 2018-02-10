using Example._0_Base.Data.DataComponent.ModelX;
using Example._0_Base.Data.Models.Model3;
using Example._0_Base.Models;
using FessooFramework.Tools.Helpers;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Migrations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Example._0_Base.Data.Contexts
{
    #region Configuration
    //Add-migration DefaultDB_1 -ConfigurationTypeName Example._0_Base.Data.Contexts.ConfigurationDefaultDB
    // Update-database
    internal sealed class ConfigurationDefaultDB3 : DbMigrationsConfiguration<DefaultDB3>
    {
        public ConfigurationDefaultDB3() { AutomaticMigrationsEnabled = true; }
        protected override void Seed(DefaultDB3 context) { }
    }
    #endregion
    #region Context
    public class DefaultDB3 : DbContext
    {
        public DbSet<Model3> Model3 { get; set; }

        public DefaultDB3()
        {
            base.Configuration.ProxyCreationEnabled = false;
            base.Configuration.LazyLoadingEnabled = true;
            //base.Database.Connection.ConnectionString = EntityHelper.CreateRemoteSQL("DefaultDB_3", "192.168.26.116", @"ExtUser", "123QWEasd");
            base.Database.Connection.ConnectionString = EntityHelper.CreateLocalSQL("DefaultDB_3");

        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {

        }

        protected override void Dispose(bool disposing)
        {
            Configuration.LazyLoadingEnabled = false;
            base.Dispose(disposing);
        }
    }
    #endregion
}
