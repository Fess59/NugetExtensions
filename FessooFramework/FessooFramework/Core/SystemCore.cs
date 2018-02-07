using FessooFramework.Components;
using FessooFramework.Components.LoggerComponent;
using FessooFramework.Objects;
using FessooFramework.Objects.SourceData;
using FessooFramework.Tools.Helpers;
using FessooFramework.Tools.IOC;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FessooFramework.Core
{
    /// <summary>   A system core.
    ///             Ядро системы, является менеджером жизненного цикла системы и компонентов, так же выполняет роль и хранилища глобальных настроек.
    ///             Основные сущности системы Components и Settings может переопределить при реализации кастомной копии ядра
    ///             </summary>
    ///
    /// <remarks>   AM Kozhevnikov, 29.01.2018. </remarks>

    public class SystemCore : SystemObject
    {
        #region Property
        /// <summary>   Gets the name.
        ///             Наименование приложение  </summary>
        ///
        /// <value> The name. </value>
        public string Name { get => Bootstrapper.ApplicationName; }

        /// <summary>   Gets or sets the bootstrapper.
        ///             Загрузчик системы </summary>
        ///
        /// <value> The bootstrapper. </value>

        private _Bootstrapper Bootstrapper { get; set; }

        /// <summary>   Gets or sets the components container. </summary>
        ///
        /// <value> The components container. </value>

        public IOContainer<SystemComponent> ComponentsContainer = new IOContainer<SystemComponent>();

        /// <summary>   Gets or sets the core configuration.
        ///             Настройки системы при инициализации - определяются в загрузчике Bootstrapper. </summary>
        ///
        /// <value> The core configuration. </value>

        public SystemCoreConfiguration CoreConfiguration { get; private set; }
        #endregion
        #region Constructor
        public SystemCore(string text)
        {
            if (text != "Fdsf4ew5gsf")
                throw new Exception("Please use the Bootstrapper.Run to initialize system. Re-initialization is not allowed");
            CoreConfiguration = new SystemCoreConfiguration();
        }
        #endregion
        #region ALM realization

        /// <summary>   Gets the store.
        ///             Получить модель данных для контекста </summary>
        ///
        /// <remarks>   AM Kozhevnikov, 07.02.2018. </remarks>
        ///
        /// <returns>   The store. </returns>

        internal DataContextStore GetStore()
        {
            DataContextStore Store = new DataContextStore();
            Bootstrapper.SetDbContext(ref Store);
            return Store;
        }
        /// <summary>   The store. 
        ///             Данные контекста</summary>

        protected override void _StateChanged(SystemState newState, SystemState oldState)
        {
            switch (newState)
            {
                case SystemState.Created:
                    ConsoleHelper.SendMessage($"0. SystemCore: Core instance created");
                    break;
                case SystemState.Initialized:
                    ConsoleHelper.SendMessage($"1. SystemCore '{Name}': Load or replace core components");

                    //0. Получаю основные настройки системы
                    var coreConfiguration = CoreConfiguration;
                    Bootstrapper.SetConfiguration(ref coreConfiguration);
                    CoreConfiguration = coreConfiguration;

                    //1. Добавляю базовые компоненты системы
                    var components = new List<SystemComponent>();
                    components.Add(new LoggerHelper());
                    components.Add(new DispatcherHelper());
                    //Получаю кастомные компоненты
                    Bootstrapper.SetComponents(ref components);
                    ComponentsContainer.AddRange(components);
                    ConsoleHelper.SendMessage($"2. SystemCore '{Name}': Initialization - setup all components default settings");
                    foreach (var component in ComponentsContainer.GetAll())
                        component._SetState(State);

                    //2. Для всех модулей провожу инициализацию - установка значений по умолчанию
                    _SetState(SystemState.Configured);
                    break;
                case SystemState.Configured:
                    ConsoleHelper.SendMessage($"3. SystemCore '{Name}': Configuration - setup all components custom settings");

                    //3. Для всех модулей провожу конфигурацию - установка кастомных значений по верх стандартных
                    foreach (var component in ComponentsContainer.GetAll())
                        component._SetState(State);
                    _SetState(SystemState.Loaded);
                    break;
                case SystemState.Loaded:
                    ConsoleHelper.SendMessage($"4. SystemCore '{Name}': Load data from different sources, local or remote");
                    //4. Загружаю и обработываю данные
                    foreach (var component in ComponentsContainer.GetAll())
                        component._SetState(State);
#if DEBUG
                    if (CoreConfiguration.ComponentTestEnable)
                        _SetState(SystemState.Testing);
                    else
                        _SetState(SystemState.Launched);
#else
                    _SetState(SystemState.Launched);
#endif
                    break;
                case SystemState.Testing:
                    //4.1 Тестовый метод для проверки работы модуля - может использовать как динамическое тестирование под дебагом
                    foreach (var component in ComponentsContainer.GetAll())
                        component._SetState(State);

                    _SetState(SystemState.Launched);
                    break;
                case SystemState.Launched:
                    ConsoleHelper.SendMessage($"5. SystemCore '{Name}': Run background and basic processes. General and basic can be assigned to Bootsrapper.Run");
                    //5. Ядро и модули запускает фоновые процессы необходимые для работы
                    foreach (var component in ComponentsContainer.GetAll())
                        component._SetState(State);

                    foreach (var component in ComponentsContainer.GetAll())
                        ConsoleHelper.SendMessage(component.ToInfo());

                    //6. Ядро готово к работе
                    ConsoleHelper.SendMessage($"Core launch completed! Elapsed  { TimeSpan.FromTicks(DateTime.Now.Ticks - CreateDate.Ticks)} мс");
                    break;
                case SystemState.Unload:
                    break;
                default:
                    break;
            }
        }
        /// <summary>   Sets a bootstrapper.
        ///             Установка загрузчика и создание копии ядра при инициализации загрузчика </summary>
        ///
        /// <remarks>   AM Kozhevnikov, 29.01.2018. </remarks>
        ///
        /// <param name="b">    A Bootstrapper to process. </param>

        internal void SetBootstrapper(_Bootstrapper b)
        {
            Bootstrapper = b;
        }
        #endregion
        #region Singlton Lazy Threadsafe
        public static SystemCore Current { get { return getInstance(); } }
        private static SystemCore getInstance()
        {
            return Nested.current;
        }
        private class Nested
        {
            internal static readonly SystemCore current = new SystemCore("Fdsf4ew5gsf");
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
}
