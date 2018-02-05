using FessooFramework.Objects.Data;
using FessooFramework.Tools.IOC;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FessooFramework.Objects.SourceData
{
    internal abstract class DataSourceBase : _IOCElement
    {
        #region Property
        public abstract DbContext GetContext();
        protected abstract bool IsCreated { get; }
        public abstract Type CurrentType { get; }
        /// <summary>   Gets or sets the types.
        ///             Список DbSet'ов в контексте </summary>
        ///
        /// <value> The types. </value>

        public IEnumerable<Type> Types { get; set; }
        #endregion
        #region Constructor
        public DataSourceBase()
        {
            UID = CurrentType.ToString();
        }
        #endregion
        #region Methods
        /// <summary>   Gets the database set types in this collection. </summary>
        ///
        /// <remarks>   AM Kozhevnikov, 05.02.2018. </remarks>
        ///
        /// <returns>
        ///     An enumerator that allows foreach to be used to process the database set types in this
        ///     collection.
        /// </returns>
        private IEnumerable<Type> GetDbSetTypes()
        {
            var result = new List<Type>();
            var dbSets = GetContext().GetType().GetProperties().Where(q => q.PropertyType.Name == "DbSet`1");
            if (dbSets.Any())
            {
                foreach (var item in dbSets)
                    result.Add(item.PropertyType.GenericTypeArguments[0]);
            }
            return result;
        }
        /// <summary>   Determines if we can check type. </summary>
        ///
        /// <remarks>   AM Kozhevnikov, 05.02.2018. </remarks>
        ///
        /// <returns>   True if it succeeds, false if it fails. </returns>
        public bool CheckType<T>()
        {
            var result = false;
            if (Types == null || !Types.Any())
                Types = GetDbSetTypes();
            result = Types.Any(q => q == typeof(T));
            return result;
        }
        /// <summary>   Database set. </summary>
        ///
        /// <remarks>   AM Kozhevnikov, 05.02.2018. </remarks>
        ///
        /// <typeparam name="T">    Generic type parameter. </typeparam>
        ///
        /// <returns>   A DbSet&lt;T&gt; </returns>

        public DbSet<T> DbSet<T>() where T : EntityObject
        {
            return GetContext().Set<T>();
        }
        public void SaveChanges()
        {
            if (IsCreated)
                GetContext().SaveChanges();
        }
        public override void Dispose()
        {
            base.Dispose();
            if (IsCreated)
                GetContext().Dispose();
        }
        #endregion

    }
}
