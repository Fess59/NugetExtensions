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

namespace Example._0_Base.Data.DataComponent
{
    public class DataContainer : IOContainer<DataComponent>
    { 
        #region Constructor
        public DataContainer(string text)
        {
            if (text != "Fdsf4ew5gsf")
                throw new Exception("Please use the singlton realization to initialize DataContainer. Re-initialization is not allowed");
        }
        #endregion
        #region   Methods

        /// <summary>   Updates the object described by obj.
        ///             Обновляем </summary>
        ///
        /// <remarks>   Fess59, 03.02.2018. </remarks>
        ///
        /// <param name="obj">  The object. </param>

        internal static void Save(EntityObject obj)
        {
            var dataComponent = Current.GetByName(obj.GetType().ToString());
            if (dataComponent != null)
                dataComponent.Save(obj);
        }
        internal static DbSet<T> DbSet<T>(T obj) where T : class
        {
            return null;
        }
        internal static TResult Convert<TResult>(EntityObject obj)
        {
            ConsoleHelper.Send("Convert", $"Type={obj.GetType()}");
            return default(TResult);
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
