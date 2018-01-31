﻿using Example._0_Base.Models;
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
    internal sealed class ConfigurationDefaultDB : DbMigrationsConfiguration<DefaultDB>
    {
        public ConfigurationDefaultDB() { AutomaticMigrationsEnabled = true; }
        protected override void Seed(DefaultDB context) { }
    }
    #endregion
    #region Context
    public class DefaultDB : DbContext
    {
        public DbSet<FirstModel> FirstModels{ get; set; }
      
        public DefaultDB()
        {
            base.Configuration.ProxyCreationEnabled = false;
            base.Configuration.LazyLoadingEnabled = true;
            base.Database.Connection.ConnectionString = EntityHelper.CreateRemoteSQL("DefaultDB", "192.168.26.116", @"ExtUser", "123QWEasd");
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
