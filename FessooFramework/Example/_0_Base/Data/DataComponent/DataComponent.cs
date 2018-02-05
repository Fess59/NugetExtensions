using FessooFramework.Objects.Data;
using FessooFramework.Tools.DCT;
using FessooFramework.Tools.Helpers;
using FessooFramework.Tools.IOC;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Example._0_Base.Data.DataComponent
{
    /// <summary>   A data component.
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
        public void Save(EntityObject obj)
        {
            DCTDefault.Context.GetContext
            //1. Проверяю существование контекста - был использован DCT
            //2.  Прогоняю объект по жиненному циклу
            //3. Сохраняю объект в указанный источник данных
            ConsoleHelper.Send("UpdateObject", $"Type={obj.GetType()}");

        }
        #endregion
    }
}


