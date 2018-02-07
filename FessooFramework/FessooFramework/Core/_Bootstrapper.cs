using FessooFramework.Objects;
using FessooFramework.Objects.SourceData;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FessooFramework.Core
{
    public abstract class Bootstrapper<T> : _Bootstrapper
   where T : _Bootstrapper, new()
    {
        #region Singlton Lazy Threadsafe

        /// <summary>   Gets the current. </summary>
        ///
        /// <value> The current. </value>

        public static T Current { get { return getInstance(); } }
        private static T getInstance()
        {
            return Nested.current;
        }
        private class Nested
        {
            internal static readonly T current = new T();
        }
        private enum BootstrapperState
        {
            Create = 0,
            Load = 1,
            Run = 2,
            Complete = 3,
            Abort = 4
        }
        #endregion
    }


    /// <summary>   A bootstrapper.
    ///             Загрузчик системы, создан для контроля и облегчения запуска системы или приложения
    ///              </summary>
    ///
    /// <remarks>   AM Kozhevnikov, 29.01.2018. </remarks>

    public abstract class _Bootstrapper : SystemObject
    {
        #region Property
        /// <summary>   Gets the name of the application.
        ///             Наименование приложения, обязательное к заполнению поле </summary>
        ///
        /// <value> The name of the application. </value>

        public abstract string ApplicationName { get; }
        #endregion
        #region Constructor

        /// <summary>   Constructor.
        ///             Данные класс Singlton. Пожалуйста используйте статичное свойство Current для работы </summary>
        ///
        /// <remarks>   AM Kozhevnikov, 07.02.2018. </remarks>
        ///
        /// <throwses cref="Exception"> Thrown when an exception error condition occurs. </throwses>
        ///
        /// <param name="text"> The text. </param>

        public _Bootstrapper()
        {
            SystemCore.Current.SetBootstrapper(this);
        }
        #endregion
        #region Method
        /// <summary>   Runs this object.
        ///             Последний метод в запуске ядра системы </summary>д
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

        /// <summary>   Sets database context. </summary>
        ///
        /// <remarks>   AM Kozhevnikov, 07.02.2018. </remarks>
        ///
        /// <param name="store">    [in,out] The store. </param>

        public abstract void SetDbContext(ref DataContextStore store);
        #endregion
    }
}
