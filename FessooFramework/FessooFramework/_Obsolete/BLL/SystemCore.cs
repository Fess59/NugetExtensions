using FessooFramework.Objects;
using FessooFramework.Tools;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FessooFramework.BLL
{
    /// <summary>   A system core.
    ///             Ядро системы - хранит в себе все настроенные модули 
    ///             </summary>
    ///
    /// <remarks>   AM Kozhevnikov, 22.01.2018. </remarks>

    public class SystemCore : BaseObject
    {
        #region Constructor
        public SystemCore()
        {
            throw new Exception("Please use the Bootstrapper to initialize system. Re-initialization is not allowed");
        }
        public SystemCore(string str)
        {
            if (str != "Fv8eGl4oifie4")
                throw new Exception("Please use the Bootstrapper to initialize system. Re-initialization is not allowed");
            else
            {
                if (!CheckInitialization()) throw new Exception("Re-initialization is not allowed");
            }
        }
        #endregion
        #region Methods

        /// <summary>   Default initialization.
        ///             Базовая инициализация системы - параметры и модули по-умолчанию </summary>
        ///
        /// <remarks>   AM Kozhevnikov, 22.01.2018. </remarks>
        internal void DefaultInitialization()
        {
            try
            {
                ConsoleHelper.SendMessage("Default configuration system has been start");
                foreach (var item in Modules)
                    item.Default();
            }
            catch (Exception ex)
            {
                ConsoleHelper.SendException(ex);
            }
            finally
            {
                ConsoleHelper.SendMessage("Default configuration system has been complete");
            }
        }
        internal void ModulesAdd(Module[] module)
        {
            try
            {
                ConsoleHelper.SendMessage("Custom module adds has been start");
                Modules.AddRange(module);
            }
            catch (Exception ex)
            {
                ConsoleHelper.SendException(ex);
            }
            finally
            {
                ConsoleHelper.SendMessage("Custom module adds has been complete");
            }
        }
        #endregion
        #region Methods - private
        /// <summary>   Determines if we can check initalization. </summary>
        ///
        /// <remarks>   AM Kozhevnikov, 22.01.2018. </remarks>
        ///
        /// <returns>   True if it succeeds, false if it fails. </returns>
        bool CheckInitialization()
        {
            var result = false;
            try
            {
                lock (ObjectInitialized)
                {
                    if (!HasInitialized)
                    {
                        HasInitialized = true;
                        result = true;
                    }
                }
            }
            catch (Exception ex)
            {
                ConsoleHelper.SendException(ex);
            }
            finally
            {
                ConsoleHelper.SendMessage($"Check system initialization complete - {result}");
            }
            return result;
        }
        #endregion
        #region Property
        public static bool HasInitialized;
        static object ObjectInitialized = new object();
        static List<Module> Modules = new List<Module>();
        #endregion
        #region Singlton Lazy Threadsafe
        public static SystemCore Current { get { return getInstance(); } }
        private static SystemCore getInstance()
        {
            return Nested.current;
        }
        private class Nested
        {
            internal static readonly SystemCore current = new SystemCore("Fv8eGl4oifie4");
        }
        #endregion
    }
}
