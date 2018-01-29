using FessooFramework.Tools.DCT;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FessooFramework.Objects
{
    /// <summary>   A module object.
    ///             Модуль системы - настройка блока логики и залог жизненного цикла блока.
    ///             Расширяемый за счёт частей реализующих интерфейс базового модуля, с возможностью отключения базовых частей и полной замены.
    ///             В случае реализации нескольких частей возможен множественный вызов и управление приоритетом выполнения
    ///             
    ///             Основная задача модуля - выработка систематичного подхода к реализации компонентов системы с жизненным циклом
    ///             
    ///             Жизненный цикл модуля:
    ///             - None - не был создан
    ///             - Created - успешно выполнил конструктор
    ///             - Initialized - успешно прошёл инициализацию, в инициализацию входит базовые настройки всего модуля, значение по умолчанию и настройки по умолчанию
    ///             - Configured - базовая настройка блока - вызов Default метода
    ///             - Loaded - загрузка данных для блока, к примеру с сервера
    ///             - Launched - запуск процессов не обходимы для работу блока  
    ///             - Complete - модуль завершил работу, принудительно или планово
    ///             
    ///             Второстепенная цель которую преследует модульность - простая, понятная и расширяемая структура кода:
    ///             - Глобальные настройки модулей в одном месте  
    ///             - Реализации модулей в одном месте  
    ///             - Компоненты модулей в одном месте </summary>
    ///
    /// <remarks>   Fess59, 26.01.2018. </remarks>
    public abstract class ModuleObject : SystemObject, IModule
    {
        #region Property
        /// <summary>   Gets or sets the state.
        ///             Состояние жизненного цикла модуля </summary>
        ///
        /// <value> The state. </value>
        public ModuleState _State { get; private set; }
        /// <summary>   Gets the name.
        ///             Уникальное имя модуля</summary>
        ///
        /// <value> The name. </value>
        public abstract string _Name { get; }
        /// <summary>   Gets or sets a value indicating whether this object is enable. </summary>
        ///
        /// <value> True if this object is enable, false if not. </value>
        public bool _IsEnable { get; set; }
        /// <summary>   Gets or sets the priority.
        ///             Приоритет загрузки модуля </summary>
        ///
        /// <value> The priority. </value>
        public int _Priority { get; set; }
        #endregion
        #region Constructor
        /// <summary>   Default constructor.
        ///             Первый шаг жизненного цикла модуля - Create </summary>
        ///
        /// <remarks>   Fess59, 26.01.2018. </remarks>
        public ModuleObject()
        {
            StateSet(ModuleState.Created);
        }
        #endregion
        #region Methods
        #region Lifecycle
        internal void StateSet(ModuleState state)
        {
            if (StateCheck(_State, state))
                throw new Exception("Переход из состояния  в это состояние не поддерживается");

            //Переопределяемый блок
            switch (state)
            {
                case ModuleState.None:
                    throw new Exception("Переход в это состояние не поддерживается");
                    break;
                case ModuleState.Created:
                    _State = ModuleState.Created;
                    break;
                case ModuleState.Initialized:
                    Initialization();
                    break;
                case ModuleState.Configured:
                    Configuration();
                    break;
                case ModuleState.Loaded:
                    Load();
                    break;
                case ModuleState.Launched:
                    Launch();
                    break;
                case ModuleState.Complete:
                    Completed();
                    break;
                default:
                    break;
            }
            //Переопределяемый блок
        }
        IEnumerable<StateConfiguratuion> StateConfiguration()
        {
            var list = new List<StateConfiguratuion>();
            foreach (var item in Enum.GetValues(typeof(ModuleState)))
            {
                var state = (ModuleState)item;
                IEnumerable<ModuleState> nextStates = Enumerable.Empty<ModuleState>();
                //Переопределяемый блок
                switch (state)
                {
                    case ModuleState.None:
                        break;
                    case ModuleState.Created:
                        break;
                    case ModuleState.Initialized:
                        break;
                    case ModuleState.Configured:
                        break;
                    case ModuleState.Loaded:
                        break;
                    case ModuleState.Launched:
                        break;
                    case ModuleState.Complete:
                        break;
                    default:
                        break;
                }
                //Переопределяемый блок

                if (nextStates.Any())
                    list.Add(new StateConfiguratuion(state, nextStates));
            }
            return list.ToArray();
        }
        bool StateCheck(ModuleState oldState, ModuleState newState)
        {
            var result = false;
            var configurations = StateConfiguration();
            if (configurations.Any())
            {
                var stateConfiguration = configurations.FirstOrDefault(q=> q.State == oldState);
                if (stateConfiguration != null)
                {
                    result = stateConfiguration.NextState.Any(q => q == newState);
                }
                else
                {
                    throw new Exception("Не реализована настройка состояний");
                }
            }
            else
            {
                throw new Exception("Не реализована настройка состояний");
            }
            return result;
        }
        class StateConfiguratuion
        {
            public StateConfiguratuion(ModuleState state, IEnumerable<ModuleState> nextState)
            {
                State = state;
                NextState = nextState;
            }
            public ModuleState State { get; }
            public IEnumerable<ModuleState> NextState { get; }
        }
        #endregion
        /// <summary>   Второй шаг настройки модуля - в инициализацию входит базовые настройки модуля, установка значений и настроек по умолчанию</summary>
        ///
        /// <remarks>   Fess59, 26.01.2018. </remarks>
        private void Initialization()
        {
            Default();
            if (string.IsNullOrWhiteSpace(_Name))
                throw new Exception("Имя модуля не указано");
            _State = ModuleState.Initialized;
        }
        /// <summary>   Configurations this object.
        ///             Настройка текущего модуля - наложение кастомных настроек и подписки глобальных настроек </summary>
        ///
        /// <remarks>   Fess59, 26.01.2018. </remarks>
        private void Configuration()
        {
            _Configuring();
            _State = ModuleState.Configured;
        }
        /// <summary>   Loads this object.
        ///             Загрузка данных для объекта и обработка данных, к примеру парметры глобальные из настрое или кэша или сервера </summary>
        ///
        /// <remarks>   Fess59, 26.01.2018. </remarks>
        private void Load()
        {
            _Loading();
            _State = ModuleState.Loaded;
        }
        /// <summary>   Launches this object.
        ///             Запуск процессов необходимых для работы модуля. Модуль в работе, все настройки завершены</summary>
        ///
        /// <remarks>   Fess59, 26.01.2018. </remarks>
        private void Launch()
        {
            _Launching();
            _State = ModuleState.Launched;
        }
        /// <summary>   Completed this object.
        ///             Работа с модулем была завершена </summary>
        ///
        /// <remarks>   Fess59, 26.01.2018. </remarks>
        private void Completed()
        {
            _Compliting();
            _State = ModuleState.Complete;
            _IsEnable = false;
            Dispose();
        }

        /// <summary>   Configurings this object.
        ///             Настройка текущего модуля - наложение кастомных настроек и подписки глобальных настроек </summary>
        ///
        /// <remarks>   Fess59, 26.01.2018. </remarks>

        public abstract void _Configuring();

        /// <summary>   Loadings this object.
        ///              Загрузка данных для объекта и обработка данных </summary>
        ///
        /// <remarks>   Fess59, 26.01.2018. </remarks>

        public abstract void _Loading();

        /// <summary>   Launchings this object. </summary>
        ///
        /// <remarks>   Fess59, 26.01.2018. </remarks>

        public abstract void _Launching();

        /// <summary>   Complitings this object. 
        ///             Работа с модулем была завершена</summary>
        ///
        /// <remarks>   Fess59, 26.01.2018. </remarks>

        public abstract void _Compliting();
        #endregion
    }
    public enum ModuleState
    {
        None = 0,
        Created = 1,
        Initialized = 2,
        Configured = 3,
        Loaded = 4,
        Launched = 5,
        Complete = 6
    }
    /// <summary>   Interface for module.
    ///             Интерфейс модуля для расширения </summary>
    ///
    /// <remarks>   Fess59, 26.01.2018. </remarks>

    public interface IModule
    {
        string _Name { get; }
        bool _IsEnable { get; set; }
    }

    public interface ILoggerModule : IModule
    {
        void Send();
        void SendException();
        void SendInformation();
        void SendWarnings();
    }


}
