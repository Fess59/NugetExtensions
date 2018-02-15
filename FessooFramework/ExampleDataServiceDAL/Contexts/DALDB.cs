using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Migrations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExampleDataServiceDAL.Contexts
{
    #region Configuration
    //Add-migration DefaultDB_1 -ConfigurationTypeName Example._0_Base.Data.Contexts.ConfigurationDefaultDB
    // Update-database
    internal sealed class ConfigurationDALDB : DbMigrationsConfiguration<DALDB>
    {
        public ConfigurationDALDB() { AutomaticMigrationsEnabled = true; }
        protected override void Seed(DALDB context) { }
    }
    #endregion
    #region Context
    public class DALDB : DbContext
    {
        public DbSet<DataModels.ExampleData> ExampleDatas { get; set; }
        public DALDB()
        {
            base.Configuration.ProxyCreationEnabled = false;
            base.Configuration.LazyLoadingEnabled = true;
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
