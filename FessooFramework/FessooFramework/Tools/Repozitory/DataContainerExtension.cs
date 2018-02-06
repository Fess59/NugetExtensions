using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FessooFramework.Tools.Repozitory
{
    /// <summary>   A data container extension. </summary>
    ///
    /// <remarks>   Fess59, 02.02.2018. </remarks>

    public static class DataContainerExtension
    {
        /// <summary>   A Type extension method that converts a type to a data container. </summary>
        ///
        /// <remarks>   AM Kozhevnikov, 06.02.2018. </remarks>
        ///
        /// <param name="type"> The type to act on. </param>

        internal static void ToDataContainer(this Type type)
        {
            DataContainer.Current.Add(DataComponent.New(type));
        }

        /// <summary>   A DbContext extension method that gets a set. </summary>
        ///
        /// <remarks>   AM Kozhevnikov, 06.02.2018. </remarks>
        ///
        /// <typeparam name="TEntity">  Type of the entity. </typeparam>
        /// <param name="context">  The context to act on. </param>
        ///
        /// <returns>   The set. </returns>

        public static DbSet<TEntity> GetSet<TEntity>(this DbContext context)
          where TEntity : class
        {
            return context.Set<TEntity>();
        }
    }
}
