using FessooFramework.Objects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FessooFramework.Core
{
    /// <summary>   A bootstrapper.
    ///             Загрузчик системы, создан для контроля и облегчения запуска системы или приложения
    ///              </summary>
    ///
    /// <remarks>   AM Kozhevnikov, 29.01.2018. </remarks>

    public abstract class Bootstrapper : SystemObject
    {
        #region Property
        /// <summary>   Gets the name of the application.
        ///             Наименование приложения, обязательное к заполнению поле </summary>
        ///
        /// <value> The name of the application. </value>

        public abstract string ApplicationName { get; }
        #endregion
        #region Constructor
        public Bootstrapper()
        {
            SystemCore.Current.SetBootstrapper(this);
        }
        #endregion
        #region Method
        /// <summary>   Runs this object.
        ///             Последний метод в запуске ядра системы </summary>
        ///
        /// <remarks>   AM Kozhevnikov, 29.01.2018. </remarks>

        public void Run()
        {
            SystemCore.Current._SetState(SystemState.Initialized);
        }

        /// <summary>   Sets the components.
        ///             Список модулей и их настройки, возможность добавить модули </summary>
        ///
        /// <remarks>   AM Kozhevnikov, 29.01.2018. </remarks>
        ///
        /// <param name="components">   [in,out] The components. </param>

        public abstract void SetComponents(ref List<SystemComponent> components);

        /// <summary>   Sets a configuration.
        ///             Основные настройки ядра при его инициализации</summary>
        ///
        /// <remarks>   AM Kozhevnikov, 30.01.2018. </remarks>
        ///
        /// <param name="settings"> [in,out] Options for controlling the operation. </param>

        public abstract void SetConfiguration(ref SystemCoreConfiguration configuration);
        #endregion
    }
}
