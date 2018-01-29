using FessooFramework.Objects;
using FessooFramework.Tools.Controllers;
using FessooFramework.Tools.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FessooFramework.Modules
{
    /// <summary>   A logger module.
    ///             Модуль логирования, управляет логированием данных в различные источники
    ///             Консоль, Файлы, Служба или Подписка </summary>
    ///
    /// <remarks>   Fess59, 26.01.2018. </remarks>

    public class LoggerModule : SystemObject
    {
        #region Module
        public bool IsEnable { get; set; }

        #endregion


        #region Property

        public ActionController Information { get; set; }
        public ActionController Exception { get; set; }
        public ActionController Warning { get; set; }
        #endregion
        #region Constructor

        #endregion
        #region Methods
        public void Send(string message)
        {
            try
            {
                
            }
            catch (Exception ex)
            {
                ConsoleHelper.SendException(ex);
            }
        }
        #endregion
        #region Singlton Lazy Threadsafe
        public static LoggerModule Current { get { return getInstance(); } }
        private static LoggerModule getInstance()
        {
            return Nested.current;
        }
        private class Nested
        {
            internal static readonly LoggerModule current = new LoggerModule();
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
