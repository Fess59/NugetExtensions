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
    /// <summary>   A dispatcher helper. Компонент для управления сихронизации потоков с основным UI потоком </summary>
    ///
    /// <remarks>   AM Kozhevnikov, 07.02.2018. </remarks>

    public class DispatcherHelper : SystemComponent
    {
        #region Property
        public static bool HasSynchronizationContext => CurrentSynchronizationContext != null;
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
            if (CurrentSynchronizationContext == null)
                DCT.SendWarning($"Не удалось настроить SynchronizationContext - требуеться WPF приложение или вызов Bootstrapper.Run был произведён не из основного потока", "DispatcherHelper");
            Dispacher = dispatcher;
            Dispacher.UnhandledException += Dispacher_UnhandledException;
        }
        internal static void Execute(Action action, bool isAsync = true, string name = "")
        {
            DCT.Execute(data =>
            {
                if (action == null)
                    throw new NullReferenceException("CurrentDispatcher.Execute - Action не может быть пустым");
                //Если контекст синхронизации не доступен в данном приложении
                if (CurrentSynchronizationContext == null)
                {
                    DCT.SendInformations($"Тип вызова TaskAsync", "DispatcherHelper");
                    DCT.ExecuteAsync(c => action());
                    return;
                }
                DCT.SendInformations($"Тип вызова SynchronizationContext IsAsync={isAsync}", "DispatcherHelper");
                if (isAsync)
                    CurrentSynchronizationContext.Post((a) => execute(a), action);
                else
                    CurrentSynchronizationContext.Send((a) => execute(a), action);
            }, name: "DispatcherHelper");
        }

        /// <summary>   Event handler. Called by Dispacher for unhandled exception events. </summary>
        ///
        /// <remarks>   AM Kozhevnikov, 07.02.2018. </remarks>
        ///
        /// <param name="sender">   Source of the event. </param>
        /// <param name="e">        Dispatcher unhandled exception event information. </param>

        internal static void Dispacher_UnhandledException(object sender, DispatcherUnhandledExceptionEventArgs e)
        {
            DCT.Execute(data => { e.Handled = true; });
        }

        /// <summary>   Executes the given arguments. </summary>
        ///
        /// <remarks>   AM Kozhevnikov, 07.02.2018. </remarks>
        ///
        /// <throwses cref="NullReferenceException">    Thrown when a value was unexpectedly null. </throwses>
        ///
        /// <param name="args"> The arguments. </param>

        static void execute(object args)
        {
            DCT.Execute(data =>
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

        /// <summary>
        ///     Configurings this object. Настройка текущего модуля - наложение кастомных настроек и
        ///     подписки глобальных настроек.
        /// </summary>
        ///
        /// <remarks>   AM Kozhevnikov, 07.02.2018. </remarks>

        public override void _2_Configuring()
        {
            SetDispatherAsDefault(Dispatcher.CurrentDispatcher);
        }

        /// <summary>   Loadings this object. Загрузка данных для объекта и обработка данных. </summary>
        ///
        /// <remarks>   AM Kozhevnikov, 07.02.2018. </remarks>

        protected override void _3_Loaded()
        {
        }

        /// <summary>
        ///     Testing this object. Проверка компонента на определённые условия - в будующем в этом
        ///     месте требуется подключение модуля динамической отладки с выводом предпреждений.
        /// </summary>
        ///
        /// <remarks>   AM Kozhevnikov, 07.02.2018. </remarks>
        ///
        /// <returns>
        ///     An enumerator that allows foreach to be used to process testing in this collection.
        /// </returns>

        protected override IEnumerable<TestingCase> _4_Testing()
        {
            return Enumerable.Empty<TestingCase>();
        }

        /// <summary>   Launchings this object. </summary>
        ///
        /// <remarks>   AM Kozhevnikov, 07.02.2018. </remarks>

        public override void _5_Launching()
        {
        }

        /// <summary>   Complitings this object. Работа с модулем была завершена. </summary>
        ///
        /// <remarks>   AM Kozhevnikov, 07.02.2018. </remarks>

        protected override void _6_Unload()
        {
        }
        #endregion

    }
}
