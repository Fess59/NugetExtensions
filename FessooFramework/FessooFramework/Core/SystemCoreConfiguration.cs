using FessooFramework.Objects;
using FessooFramework.Objects.SourceData;
using FessooFramework.Tools.Controllers;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace FessooFramework.Core
{
    /// <summary>   A core configuration.
    ///             Базовые настройки системы </summary>
    ///
    /// <remarks>   AM Kozhevnikov, 30.01.2018. </remarks>

    public class SystemCoreConfiguration : SystemObject
    {
        /// <summary>   Gets or sets the log enable.
        ///             Логи в системе </summary>
        ///
        /// <value> The log enable. </value>

        public ObjectController<bool> LogEnable = new ObjectController<bool>(false);
        /// <summary>   Gets the pathname of the root directory. </summary>
        ///
        /// <value> The pathname of the root directory. </value>

        public static string RootDirectory { get { return System.IO.Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location); } }

        /// <summary>
        ///     Gets or sets a value indicating whether this object is test module enable.
        ///     Управление тестирование компонентов при инициализации системы
        /// </summary>
        ///
        /// <value> True if test module enable, false if not. </value>

        public bool ComponentTestEnable { get; set; }
        /// <summary>   The store. 
        ///             Данные контекста</summary>
        public DataContextStore Store = new DataContextStore();

        public override void Default()
        {
            base.Default();
#if DEBUG
            LogEnable.Value = true;
            ComponentTestEnable = true;
#endif
        }
    }
}
