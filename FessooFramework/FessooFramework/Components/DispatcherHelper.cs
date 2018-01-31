using FessooFramework.Objects;
using FessooFramework.Objects.ALM;
using FessooFramework.Tools.DCT;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Threading;

namespace FessooFramework.Components
{
    public class DispatcherHelper : SystemComponent
    {
        #region Property
        /// <summary>
        ///     Диспетчер основного потока
        /// </summary>
        static Dispatcher Dispacher { get; set; }

        /// <summary>
        ///     Диспетчер основного потока
        /// </summary>
        static SynchronizationContext CurrentSynchronizationContext { get; set; }
        #endregion
        #region Methods
        /// <summary>
        ///     Устанавливает текущий диспетчер как основной диспетчер для выполнения операций
        /// </summary>
        /// <param name="dispatcher"></param>
        internal static void SetDispatherAsDefault(Dispatcher dispatcher)
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
                //Если контекст синхронизации не доступен в данном приложении
                if (CurrentSynchronizationContext == null)
                {
                    DCTDefault.ExecuteAsync(c => action());
                    return;
                }
                if (isAsync)
                    CurrentSynchronizationContext.Post((a) => execute(a), action);
                else
                    CurrentSynchronizationContext.Send((a) => execute(a), action);
            }, name: "DispatcherHelper");
        }
        internal static void Dispacher_UnhandledException(object sender, DispatcherUnhandledExceptionEventArgs e)
        {
            DCTDefault.Execute(data => { e.Handled = true; });
        }
        static void execute(object args)
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
        #region Component realization

        #endregion
        public override void _2_Configuring()
        {
            SetDispatherAsDefault(Dispatcher.CurrentDispatcher);
        }
        public override void _3_Loaded()
        {
        }
        public override IEnumerable<TestingCase> _4_Testing()
        {
            return Enumerable.Empty<TestingCase>();
        }
        public override void _5_Launching()
        {
        }
        public override void _6_Unload()
        {
        }
    }
}
