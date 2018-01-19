using FessooFramework.Tools;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FessooFramework.BLL
{
    /// <summary>   Модуль управления загрузкой системы. Для загрузки рекомендуется использовать реализацию Singlton </summary
    /// <remarks>   AM Kozhevnikov, 19.01.2018. </remarks>

    public class Bootstrapper
    {
        #region Singlton Lazy Threadsafe
        public static Bootstrapper Current {  get { return getInstance(); } }
        private static Bootstrapper getInstance()
        {
            return Nested.current;
        }
        private class Nested
        {
            internal static readonly Bootstrapper current = new Bootstrapper();
        }
        #endregion
        #region Constructor
        public Bootstrapper()
        {
            ConsoleHelper.SendWarning("Рекомендуется использовать Singlton в нём реализован процесс настройки и повторного использования. Bootstrapper.Current");
        }
        public Bootstrapper(string s)
        {
           
        }
        #endregion
    }
}
