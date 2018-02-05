using FessooFramework.Tools.IOC;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Example._0_Base.Data.DataComponent.ModelX;
using System.Data.Entity;
using FessooFramework.Objects.Data;
using FessooFramework.Tools.Helpers;
using FessooFramework.Tools.DCT;

namespace Example._0_Base.Data.DataComponent
{
    public class DataContainer : IOContainer<DataComponentBase>
    {
        #region Property
        private IOContainer<ALMComponentBase> ALM = new IOContainer<ALMComponentBase>();
        #endregion
        #region Constructor
        public DataContainer(string text) : base(true)
        {
            if (text != "Fdsf4ew5gsf")
                throw new Exception("Please use the singlton realization to initialize DataContainer. Re-initialization is not allowed");

            ALM.Add(new ModelXALM());
        }
        #endregion
        #region   Methods

        /// <summary>   Updates the object described by obj.
        ///             Обновляем модель данные - треубеться SaveChanges </summary>
        ///
        /// <remarks>   Fess59, 03.02.2018. </remarks>
        ///
        /// <param name="obj">  The object. </param>

        internal static void Save<T>(T obj) where T : EntityObject
        {
            DCTExample.Execute(c =>
            {
                var r = Current.ALM.GetByName(typeof(T).ToString());
                var dbSet = DbSet<T>();
                var oldObj = dbSet.FirstOrDefault(q=> q.Id == obj.Id);
                r.Update(oldObj, obj);
            });
        }

        /// <summary>   Database set.
        ///             TODO заменить DbSet обертку </summary>
        ///
        /// <remarks>   AM Kozhevnikov, 05.02.2018. </remarks>
        ///
        /// <throwses cref="NullReferenceException">    Thrown when a value was unexpectedly null. </throwses>
        ///
        /// <typeparam name="T">    Generic type parameter. </typeparam>
        ///
        /// <returns>   A DbSet&lt;T&gt; </returns>

        internal static DbSet<T> DbSet<T>() where T : EntityObject
        {
            var result = default(DbSet<T>);
            DCTExample.Execute(c =>
            {
                result = DCTExample.Context.DbSet<T>();
                if (result == null)
                    throw new NullReferenceException($"DataComponent from {typeof(T).ToString()}, not implimented DbSet");
            });
            return result;
        }
        internal static TResult Convert<TResult>(EntityObject obj) where TResult : class
        {
            var result = default(TResult);
            DCTExample.Execute(c =>
            {
                var dataComponent = Current.GetByName(obj.GetType().ToString());
                result = dataComponent.Convert<TResult>(obj);
                if (result == null)
                    throw new NullReferenceException($"DataComponent from {obj.GetType().ToString()}, not implimented Creator from {typeof(TResult).ToString()}");
            });
            return result;
        }
        #endregion
        #region Abstraction
        public override DataComponentBase Create(string uid)
        {
            return Add(DataComponentBase.New(Type.GetType(uid, false, false)));
        }
        #endregion
        #region Singlton
        public static DataContainer Current { get { return getInstance(); } }
        private static DataContainer getInstance()
        {
            return Nested.current;
        }
        private class Nested
        {
            internal static readonly DataContainer current = new DataContainer("Fdsf4ew5gsf");
        }
        #endregion
    }
}
