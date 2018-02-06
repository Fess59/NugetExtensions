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
        IOContainer<CreatorElement> creators = new IOContainer<CreatorElement>();
        #endregion
        #region Constructor
        public DataContainer(string text) : base(true)
        {
            if (text != "Fdsf4ew5gsf")
                throw new Exception("Please use the singlton realization to initialize DataContainer. Re-initialization is not allowed");
        }
        #endregion
        #region   Methods

        public static TResult Convert<TResult>(EntityObject obj) where TResult : class
        {
            var result = default(TResult);
            DCTDefault.Execute(c =>
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


    public class CreatorElement : _IOCElement
    { }
}
