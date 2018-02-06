using FessooFramework.Objects.Data;
using FessooFramework.Tools.Helpers;
using FessooFramework.Tools.IOC;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FessooFramework.Tools.Repozitory
{
    /// <summary>   A data component.
    ///             Компонент определяет базовые методы для работы с со списками
    ///              </summary>
    ///
    /// <remarks>   Fess59, 02.02.2018. </remarks>
    ///
    /// <typeparam name="TModel">   Type of the model. </typeparam>
    public class DataComponent : _IOCElement
    {
        #region Property
        /// <summary>   Gets or sets the type of the current.
        ///             Тип текущего компоненнта данных </summary>
        ///
        /// <value> The type of the current. </value>
        public Type CurrentType { get; private set; }

        #endregion
        #region Constructor
        /// <summary>   New data component from type. </summary>
        ///
        /// <remarks>   Fess59, 02.02.2018. </remarks>
        ///
        /// <param name="type"> The type. </param>
        ///
        /// <returns>   A DataComponent. </returns>
        internal static DataComponent New(Type type)
        {
            return new DataComponent()
            {
                CurrentType = type,
                UID = type.ToString()
            };
        }
        #endregion
        #region Methods
        /// <summary>   Converts the given object. </summary>
        ///
        /// <remarks>   AM Kozhevnikov, 05.02.2018. </remarks>
        ///
        /// <typeparam name="TResult">  Type of the result. </typeparam>
        /// <param name="obj">  The object. </param>
        ///
        /// <returns>   A TResult. </returns>
        public TResult Convert<TResult>(EntityObject obj) where TResult : class
        {
            ConsoleHelper.Send("Convert", $"Type={obj.GetType()}");
            return default(TResult);
        }
        #endregion
    }
}
