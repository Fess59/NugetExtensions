using FessooFramework.Tools.DCT;
using FessooFramework.Tools.IOC;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FessooFramework.Objects
{
    /// <summary>   A component object.
    ///             Компонент системы - настройка компонента логики и залог жизненного цикла блока. Главное отличие об SystemObject - реагирует на изменение состояния изменения системы и имеет такое же состояние
    ///             
    ///             
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
    ///             - Testing - кейсы компонента системы, выполняются под DEBUG
    ///             - Launched - запуск процессов не обходимы для работу блока  
    ///             - Complete - модуль завершил работу, принудительно или планово
    ///             
    ///             Второстепенная цель которую преследует модульность - простая, понятная и расширяемая структура кода:
    ///             - Глобальные настройки модулей в одном месте  
    ///             - Реализации модулей в одном месте  
    ///             - Компоненты модулей в одном месте </summary>
    ///
    /// <remarks>   Fess59, 26.01.2018. </remarks>
    public abstract class SystemComponent : _IOCElement
    {
        #region Property
        /// <summary>   Gets the name.
        ///             Уникальное имя модуля</summary>
        ///
        /// <value> The name. </value>
        public string _Name => this.GetType().Name;
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
        public SystemComponent()
        {
            UID = _Name;
            //StateSet(ModuleState.Created);
        }
        #endregion
        #region Methods

        /// <summary>   Converts this object to an information. </summary>
        ///
        /// <remarks>   AM Kozhevnikov, 29.01.2018. </remarks>

        internal string ToInfo()
        {
            return $"Component {_Name} state {State}";
        }
        #endregion
        #region Abstractiona
        /// <summary>   Второй шаг настройки модуля(Create первый) - в инициализацию входит базовые настройки модуля, установка значений и настроек по умолчанию</summary>
        ///
        /// <remarks>   Fess59, 26.01.2018. </remarks>
        public virtual void _1_Initialization()
        {
            Default();
        }
        /// <summary>   Configurings this object.
        ///             Настройка текущего модуля - наложение кастомных настроек и подписки глобальных настроек </summary>
        ///
        /// <remarks>   Fess59, 26.01.2018. </remarks>

        public abstract void _2_Configuring();

        /// <summary>   Loadings this object.
        ///              Загрузка данных для объекта и обработка данных </summary>
        ///
        /// <remarks>   Fess59, 26.01.2018. </remarks>

        protected abstract override void _3_Loaded();

        /// <summary>   Testing this object.
        ///             Проверка компонента на определённые условия - в будующем в этом месте требуется подключение модуля динамической отладки с выводом предпреждений </summary>
        ///
        /// <remarks>   AM Kozhevnikov, 29.01.2018. </remarks>

        protected abstract override IEnumerable<TestingCase> _4_Testing();

        /// <summary>   Launchings this object. </summary>
        ///
        /// <remarks>   Fess59, 26.01.2018. </remarks>

        public abstract void _5_Launching();

        /// <summary>   Complitings this object. 
        ///             Работа с модулем была завершена</summary>
        ///
        /// <remarks>   Fess59, 26.01.2018. </remarks>

        protected abstract override void _6_Unload();
        #endregion
        #region ALM realization
        protected sealed override void _StateChanged(SystemState newState, SystemState oldState)
        {
            switch (newState)
            {
                case SystemState.Created:
                    break;
                case SystemState.Initialized:
                    _1_Initialization();
                    break;
                case SystemState.Configured:
                    _2_Configuring();
                    break;
                case SystemState.Loaded:
                    _3_Loaded();
                    break;
                case SystemState.Launched:
                    _5_Launching();
                    break;
                case SystemState.Testing:
                    Testing();
                    break;
                case SystemState.Unload:
                    _6_Unload();
                    _IsEnable = false;
                    Dispose();
                    break;
                default:
                    break;
            }
        }
        #endregion
    }
}
