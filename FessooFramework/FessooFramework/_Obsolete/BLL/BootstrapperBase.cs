using FessooFramework.Tools;
using FessooFramework.Tools.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FessooFramework.BLL
{
    /// <summary>   Модуль управления загрузкой системы. Для загрузки рекомендуется использовать реализацию Singlton </summary
    /// <remarks>   AM Kozhevnikov, 19.01.2018. </remarks>

    public class BootstrapperBase
    {
        #region Constructor
        public BootstrapperBase()
        {
            ConsoleHelper.SendWarning("Рекомендуется использовать Singlton, с реализованым процессом настройки и повторного использования - Bootstrapper.Current");
        }
        public BootstrapperBase(string s)
        {
            StateSet(BootstrapperState.Create);
            ConsoleHelper.SendMessage($"The Fessooo system welcomes you! {Environment.NewLine}The creation of a new copy is initiated.{Environment.NewLine}To start work call Bootsrapper.Run");
            Initialization();
        }
        #endregion
        #region Methods
        /// <summary>   Initializations this object. 
        ///             Инициализцаия системы и всех её компонентов
        ///             Единоразовая операция при создании окружения для текущей инстации</summary>
        ///
        /// <remarks>   AM Kozhevnikov, 22.01.2018. </remarks>
        void Initialization()
        {
            try
            {
                StateSet(BootstrapperState.Load);
                ConsoleHelper.SendMessage("System initialization has been start");
                if (SystemCore.HasInitialized)
                    throw new Exception("Systym re-initialization is not allowed");
                //Инициализирую копию системы 
                var system = SystemCore.Current;
                SystemCore.Current.DefaultInitialization();

                //Подключаю кастомые модули к системе
                var list = new List<Module>();
                list = ModulesConnection(list);
                if (list.Any())
                    SystemCore.Current.ModulesAdd(list.ToArray());



                ConsoleHelper.SendMessage("Custom system configuration has been start");
                // Подключаем дополнительные модули
                // Настраиваем систему
                // - Базовая настройка
                // - Кастомная настройка

                //configuration = new SystemConfiguration();
                //// Создание и настройка базовых модулей
                //// Кастомизация конфигурирования системы
                //Configuring(configuration);
                ConsoleHelper.SendMessage("Custom system configuration has been complete");

                ConsoleHelper.SendMessage("Run system has been start");
                // Подготовка к запуску и запуск системы - обычно открытие визуального представления
                ConsoleHelper.SendMessage("Run system has been complete");
            }
            catch (Exception ex)
            {
                ConsoleHelper.SendException(ex);
            }
            finally
            {
                ConsoleHelper.SendMessage("System initialization has been complete");
            }
        }

        /// <summary>   Modules connection.
        ///             Подключаем кастомные модули </summary>
        ///
        /// <remarks>   AM Kozhevnikov, 22.01.2018. </remarks>

        public virtual List<Module> ModulesConnection(List<Module> list)
        {
            return list;
        }
        public void Run()
        {
            StateSet(BootstrapperState.Run);
        }
        /// <summary>   Configurings the given configuration. </summary>
        ///
        /// <remarks>   AM Kozhevnikov, 22.01.2018. </remarks>
        ///
        /// <param name="configuration">    The configuration. </param>
        public virtual void Configuring(SystemConfiguration configuration)
        {

        }
        private void ConfiguratingDefault()
        {

        }
        #endregion
        #region Object state pattern
        private BootstrapperState State { get; set; }
        private string Exception { get; set; }

        /// <summary>   State set.
        ///             Проверка жизненного цикла объекта </summary>
        ///
        /// <remarks>   AM Kozhevnikov, 22.01.2018. </remarks>
        ///
        /// <param name="newState"> State of the new. </param>
        ///
        /// <returns>   True if it succeeds, false if it fails. </returns>

        private bool StateSet(BootstrapperState newState, Exception Ex = null)
        {
            var result = false;
            //В состояние прерывания выходим 
            if (State == BootstrapperState.Abort)
                return result;
            //При переходе в состояние прерывания выходим 
            if (State == BootstrapperState.Abort)
            {
                if (Ex != null)
                    Exception = Ex.ToString();
                State = newState;
            }
            //Проверяем состояние
            bool hasStateException = false;
            switch (State)
            {
                case BootstrapperState.Create:
                    if (newState == BootstrapperState.Create || newState == BootstrapperState.Load)
                    {
                        State = newState;
                        result = true;
                    }
                    else
                        hasStateException = true;
                    break;
                case BootstrapperState.Load:
                    if (newState == BootstrapperState.Run)
                    { 
                        State = newState;
                        result = true;
                    }
                    else
                        hasStateException = true;
                    break;
                case BootstrapperState.Run:
                    if (newState == BootstrapperState.Complete)
                    { 
                        State = newState;
                        result = true;
                    }
                    else
                        hasStateException = true;
                    break;
                case BootstrapperState.Complete:
                    if (newState == BootstrapperState.Complete)
                    {
                        State = newState;
                        result = true;
                    }
                    else
                        hasStateException = true;
                    break;
                default:
                    throw new Exception("BootstrapperState value not found");
            }
            if (hasStateException)
                throw new Exception("BootstrapperState: Cannot go to this state");
            return result;
        }
        #endregion
        #region Property
        #endregion
        #region Singlton Lazy Threadsafe
        public static BootstrapperBase Current { get { return getInstance(); } }
        private static BootstrapperBase getInstance()
        {
            return Nested.current;
        }
        private class Nested
        {
            internal static readonly BootstrapperBase current = new BootstrapperBase("s");
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
