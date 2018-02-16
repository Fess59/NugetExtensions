using FessooFramework.Tools.DataContexts.Models;
using FessooFramework.Tools.Helpers;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Migrations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FessooFramework.Tools.DataContexts
{
    #region Configuration
    //Add-migration MainDB_1 -ConfigurationTypeName FessooFramework.Tools.DataContexts.ConfigurationMainDB
    // Update-database
    internal sealed class ConfigurationMainDB : DbMigrationsConfiguration<MainDB>
    {
        public ConfigurationMainDB() { AutomaticMigrationsEnabled = true; }
        protected override void Seed(MainDB context) { }
    }
    #endregion
    #region Context
    public class MainDB : DbContext
    {
        public DbSet<ApplicationAccess> Applications { get; set; }
        public DbSet<UserProfile> UserProfiles { get; set; }
        public DbSet<UserRole> UserRoles{ get; set; }
        public DbSet<UserSession> UserSessions{ get; set; }
        public DbSet<UserAccess> UserAccesses{ get; set; }
        public MainDB()
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
