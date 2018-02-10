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
    internal sealed class ConfigurationDefaultDB2 : DbMigrationsConfiguration<DefaultDB2>
    {
        public ConfigurationDefaultDB2() { AutomaticMigrationsEnabled = true; }
        protected override void Seed(DefaultDB2 context) { }
    }
    #endregion
    #region Context
    public class DefaultDB2 : DbContext
    {
        public DbSet<SecondModel> SecondModels { get; set; }
        public DbSet<ThirdModel> ThirdModels { get; set; }

        public DefaultDB2()
        {
            base.Configuration.ProxyCreationEnabled = false;
            base.Configuration.LazyLoadingEnabled = true;
            //base.Database.Connection.ConnectionString = EntityHelper.CreateRemoteSQL("DefaultDB2", "192.168.26.116", @"ExtUser", "123QWEasd");
            base.Database.Connection.ConnectionString = EntityHelper.CreateLocalSQL("DefaultDB2");

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
