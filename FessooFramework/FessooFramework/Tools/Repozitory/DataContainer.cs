using FessooFramework.Objects.Data;
using FessooFramework.Tools.DCT;
using FessooFramework.Tools.IOC;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FessooFramework.Tools.Repozitory
{
    /// <summary>   A data container. </summary>
    ///
    /// <remarks>   AM Kozhevnikov, 06.02.2018. </remarks>
    public class DataContainer : IOContainer<DataComponent>
    {
        #region Property

        /// <summary>   The creators. 
        ///             Контейнер с преобразователями в модели</summary>
        IOContainer<CreatorElement> creators = new IOContainer<CreatorElement>();
        #endregion
        #region Constructor

        /// <summary>   Constructor. </summary>
        ///
        /// <remarks>   AM Kozhevnikov, 07.02.2018. </remarks>
        ///
        /// <throwses cref="Exception"> Thrown when an exception error condition occurs. </throwses>
        ///
        /// <param name="text"> The text. </param>

        public DataContainer(string text) : base(true)
        {
            if (text != "Fdsf4ew5gsf")
                throw new Exception("Please use the singlton realization to initialize DataContainer. Re-initialization is not allowed");
        }
        #endregion
        #region   Methods

        /// <summary>   Converts the given object. Конвертация в модель отображения или кеширования </summary>
        ///
        /// <remarks>   AM Kozhevnikov, 07.02.2018. </remarks>
        ///
        /// <throwses cref="NullReferenceException">    Thrown when a value was unexpectedly null. </throwses>
        ///
        /// <typeparam name="TResult">  Type of the result. </typeparam>
        /// <param name="obj">  The object. </param>
        ///
        /// <returns>   A TResult. </returns>

        public static TResult Convert<TResult>(EntityObject obj) where TResult : class
        {
            var result = default(TResult);
            DCT.DCT.Execute(c =>
            {
                //var dataComponent = Current.GetByName(obj.GetType().ToString());
                //result = dataComponent.Convert<TResult>(obj);
                if (result == null)
                    throw new NullReferenceException($"DataComponent from {obj.GetType().ToString()}, not implimented Creator from {typeof(TResult).ToString()}");
            });
            return result;
        }
        #endregion
        #region Singlton

        /// <summary>   Gets the current. Текущий контейнер данных для системы </summary>
        ///
        /// <value> The current. </value>

        public static DataContainer Current { get { return getInstance(); } }

        /// <summary>   Gets the instance. </summary>
        ///
        /// <remarks>   AM Kozhevnikov, 07.02.2018. </remarks>
        ///
        /// <returns>   The instance. </returns>

        private static DataContainer getInstance()
        {
            return Nested.current;
        }

        /// <summary>   A nested. </summary>
        ///
        /// <remarks>   AM Kozhevnikov, 07.02.2018. </remarks>

        private class Nested
        {
            internal static readonly DataContainer current = new DataContainer("Fdsf4ew5gsf");
        }
        #endregion
    }


    public class CreatorElement : _IOCElement
    { }
}
