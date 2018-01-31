//using Example._0_Base.Models;
//using FessooFramework.Tools.Helpers;
//using System;
//using System.Collections.Generic;
//using System.Data.Entity;
//using System.Data.Entity.Migrations;
//using System.Data.SQLite;
//using System.Data.SQLite.EF6.Migrations;
//using System.Linq;
//using System.Text;
//using System.Threading.Tasks;

//namespace Example._0_Base.Data.Contexts
//{
//    #region Configuration
//    //Add-migration DefaultSQLiteDB_1 -ConfigurationTypeName Example._0_Base.Data.Contexts.ConfigurationDefaultSQLiteDB
//    // Update-database
//    internal sealed class ConfigurationDefaultSQLiteDB : DbMigrationsConfiguration<DefaultSQLiteDB>
//    {
//        public ConfigurationDefaultSQLiteDB() {
//            AutomaticMigrationsEnabled = true;
//            //SetSqlGenerator("System.Data.SQLite", new SQLiteMigrationSqlGenerator());
//        }
//        protected override void Seed(DefaultSQLiteDB context) { }
//    }
//    #endregion
//    #region Context
//    public class DefaultSQLiteDB : DbContext
//    {
//        public DbSet<FirstModel> FirstModels { get; set; }

//        public DefaultSQLiteDB() : base (new SQLiteConnection()
//        {
//            ConnectionString =
//            new SQLiteConnectionStringBuilder()
//            { DataSource = $@"|DataDirectory|\DefaultSQLiteDB.db", ForeignKeys = true, Version = 3 }
//            .ConnectionString
//        }, true)
//        {
//            base.Configuration.ProxyCreationEnabled = false;
//            base.Configuration.LazyLoadingEnabled = true;
//            //base.Database.Connection.ConnectionString = EntityHelper.CreateDefaultSQLite("DefaultSQLiteDB");
//            //Database.SetInitializer(new MigrateDatabaseToLatestVersion<DefaultSQLiteDB, ConfigurationDefaultSQLiteDB>(true));
//            Database.SetInitializer<DefaultSQLiteDB>(null);
//        }

//        protected override void OnModelCreating(DbModelBuilder modelBuilder)
//        {

//        }

//        protected override void Dispose(bool disposing)
//        {
//            Configuration.LazyLoadingEnabled = false;
//            base.Dispose(disposing);
//        }
//    }
//    #endregion
//}
