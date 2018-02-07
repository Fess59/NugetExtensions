using Example._0_Base.Data;
using Example._0_Base.Data.Contexts;
using Example._0_Base.Data.DataComponent.ModelX;
using Example._0_Base.Models;
using FessooFramework.Components;
using FessooFramework.Core;
using FessooFramework.Objects.SourceData;
using FessooFramework.Tools.Repozitory;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FessooFramework.Tools.DCT
{
    /// <summary>   A dct context default.
    ///             Базовая реализация контекста данных для DCTDefault
    ///             Создана для внутренних целей фреймворка или проектов без собственной реализации </summary>
    ///
    /// <remarks>   Fess59, 26.01.2018. </remarks>
    public class DCTContextExample : DCTContext
    {
        #region Context examples
        public DefaultDB ExampleDB => _Store.Context<DefaultDB>();
        #endregion
        #region DbSet Examples
        public DbSet<FirstModel> FirstModels => _Store.Context<DefaultDB>().GetSet<FirstModel>();
        public DbSet<SecondModel> SecondModels => _Store.Context<DefaultDB2>().GetSet<SecondModel>();
        public DbSet<ThirdModel> ThirdModels => _Store.Context<DefaultDB2>().ThirdModels;
        #endregion
        public DCTContextExample()
        {
          
        }
    }
}
