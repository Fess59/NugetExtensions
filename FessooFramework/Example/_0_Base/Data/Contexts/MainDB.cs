using FessooFramework.Tools.DataContexts;
using FessooFramework.Tools.DCT;
using System;
using System.Collections.Generic;
using System.Data.Entity.Migrations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Example._0_Base.Data.Contexts
{
    /// <summary>
    /// Пример конфигурации миграций в главную базу
    /// Add-migration MainDB_1 -ConfigurationTypeName Example._0_Base.Data.Contexts.ConfigurationMainDB
    // Update-database
    /// </summary>
    internal sealed class ConfigurationMainDB : DbMigrationsConfiguration<MainDB>
    {
        public ConfigurationMainDB() { AutomaticMigrationsEnabled = true; }
        protected override void Seed(MainDB context) {
            DCT.Execute(c => {
                context = c._Store.Context<MainDB>();
                
            });
        }
    }
}
