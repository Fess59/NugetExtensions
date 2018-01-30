using FessooFramework.Tools.DCT;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Threading;

namespace FessooFramework.Tools.Helpers
{
    public static class DispatcherHelper
    {
        #region Property
        /// <summary>
        ///     Диспетчер основного потока
        /// </summary>
        internal static Dispatcher Dispacher { get; set; }

        /// <summary>
        ///     Диспетчер основного потока
        /// </summary>
        internal static SynchronizationContext CurrentSynchronizationContext { get; set; }
        #endregion
        #region Public methods
        /// <summary>
        ///     Устанавливает текущий диспетчер как основной диспетчер для выполнения операций
        /// </summary>
        /// <param name="dispatcher"></param>
        internal static void SetDispatherAsDefault(this Dispatcher dispatcher)
        {
            CurrentSynchronizationContext = SynchronizationContext.Current;
            Dispacher = dispatcher;
            Dispacher.UnhandledException += Dispacher_UnhandledException;
        }
        internal static void Execute(Action action, bool isAsync = true, string name = "")
        {
            DCTDefault.Execute(data =>
            {
                if (action == null)
                    throw new NullReferenceException("CurrentDispatcher.Execute - Action не может быть пустым");
                if (isAsync)
                    CurrentSynchronizationContext.Post((a) => execute(a), action);
                else
                    CurrentSynchronizationContext.Send((a) => execute(a), action);
            }, name: "DispatcherHelper");
        }

        #endregion
        #region Private
        internal static void Dispacher_UnhandledException(object sender, DispatcherUnhandledExceptionEventArgs e)
        {
            DCTDefault.Execute(data => { e.Handled = true; });
        }
        internal static void execute(object args)
        {
            DCTDefault.Execute(data =>
            {
                if (args == null)
                    throw new NullReferenceException("CurrentDispatcher.Execute - Action не может быть пустым");
                var action = (Action)args;
                var sw = new Stopwatch();
                sw.Start();
                action?.Invoke();
                sw.Stop();
                //if (sw.ElapsedMilliseconds > 500)
                //    DCT.SendInfo("DispatcherHelper очень долго выполнялась операция, продолжительность " + sw.ElapsedMilliseconds + Environment.NewLine + "Описание операции: " + action.ToString());
            });
        }
        #endregion
    }
}
