using Example._0_Base.Data.DataComponent;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace System
{
    /// <summary>   A data container extension. </summary>
    ///
    /// <remarks>   Fess59, 02.02.2018. </remarks>

    public static class DataContainerExtension
    {
        internal static void ToDataContainer(this Type type)
        {
            DataContainer.Current.Add(DataComponent.New(type));
        }
        public static DbSet<TEntity> GetSet<TEntity>(this DbContext context)
          where TEntity : class
        {
            return context.Set<TEntity>();
        }
    }
}
