using FessooFramework.Tools.Helpers;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using FessooFramework.Tools.DCT;
using FessooFramework.Components.LoggerComponent;
using FessooFramework.Components.LoggerComponent.Models;
using FessooFramework.Components;

namespace FessooFramework.Core
{
    /// <summary>   A dct.
    ///             Инструмент отказоустойчивости системы, интегрирован модулями:
    ///             - Логирования - сбор ошибок и урощенная аналитика ошибок  
    ///             - Аналитика - количество и время выполнения каждого метода и блока  
    ///             - Трекер - дерево выполнения всех методов  
    ///             - MainThread - Интеграция с модулем основного потока  
    ///             - DCTContext - расширяемый блок данных  
    ///             - Pools - различные пулы объектов, визуальные и другие  
    ///             - Warnings - модуль предупреждений, аналог юнит тестирования на лету </summary>
    ///
    /// <remarks>   Fess59, 26.01.2018. </remarks>
    ///
    /// <typeparam name="TContext"> Type of the context. </typeparam>
    public static class _DCT<TContext> where TContext : _DCTContext, new()
    {
        #region DCT base
        /// <summary>    Executes result.
        ///              Основной инструмент, в нём сосредоточена вся основная логика работы </summary>
        ///
        /// <remarks>    Fess59, 26.01.2018. </remarks>
        ///
        /// <exception cref="NullReferenceException">    Thrown when a value was unexpectedly null. </exception>
        ///
        /// <typeparam name="TResult">   Type of the result. </typeparam>
        /// <param name="method">                    The method. Выполняемый блок кода  </param>
        /// <param name="_data">                     (Optional) The data. Контекст данных </param>
        /// <param name="continueExceptionMethod">   (Optional) The continue exception method. Выполнится при ошибке в method </param>
        /// <param name="continueMethod">            (Optional) The continue method. Выполнится после method и continueExceptionMethod  </param>
        /// <param name="complete">                  (Optional) The complete. Для отправки результата в другой блок кода, используется в ExecuteAsync<TReusult>  </param>
        ///
        /// <returns>    A TResult. </returns>
        private static TResult execute<TResult>(
            string name,
            Func<TContext, TResult> method,
            Action<TContext, Exception> continueExceptionMethod = null,
            Action<TContext> continueMethod = null,
             Action<TContext, TResult> complete = null)
        {
            TResult result = default(TResult);
            bool isOwner = false;
            try
            {
                if (method == null) throw new NullReferenceException("Parameter 'method' cannot be null");
                //Статус владельца контекста
                isOwner = CheckContext();
                result = method(Context);

            }
            catch (Exception ex)
            {
                if (continueExceptionMethod != null)
                    execute(name, dataEx => continueExceptionMethod(dataEx, ex));
                SendExceptions(ex, name);
            }
            finally
            {
                if (continueMethod != null)
                    execute(name, dataCon => continueMethod(dataCon));
                if (complete != null)
                    execute(name, dataCom => complete(dataCom, result));
                //TODO Tracker and Analitics
                //SendInformations($@"Complete", name);
            }
            DisposeContext(Context, isOwner);
            return result;
        }
        /// <summary>
        ///     Executes void. Основной инструмент, в нём сосредоточена вся основная логика работы.
        /// </summary>
        ///
        /// <remarks>   Fess59, 26.01.2018. </remarks>
        ///
        /// <param name="method">                   The method. Выполняемый блок кода. </param>
        /// <param name="_data">                    (Optional) The data. Контекст данных. </param>
        /// <param name="continueExceptionMethod">  (Optional) The continue exception method. Выполнится
        ///                                         при ошибке в method. </param>
        /// <param name="continueMethod">           (Optional) The continue method. Выполнится после
        ///                                         method и continueExceptionMethod. </param>
        private static void execute(string name, Action<TContext> method,
           Action<TContext, Exception> continueExceptionMethod = null,
           Action<TContext> continueMethod = null)
        {
            execute<object>(name, data => { method(data); return null; }, continueExceptionMethod, continueMethod);
        }
        #endregion
        #region Context base
        static string contextName = "DCTContext";

        public static TContext Context { get { return GetContext(null); } }
        /// <summary>    Gets a context.
        ///              Получаю контекст из области данных потока </summary>
        ///
        /// <remarks>    Fess59, 26.01.2018. </remarks>
        ///
        /// <param name="value"> (Optional) The value. </param>
        ///
        /// <returns>    The context. </returns>
        private static TContext GetContext(TContext value = null)
        {
            var context = ContextHelper.CheckOrCreateContext<TContext>(contextName);
            if (value != null)
            {
                var parentTrackId = context.TrackId == value.TrackId ? context.ParentTrackId : value.ParentTrackId;
                context.ParentTrackId = parentTrackId;
            }
            return context;
        }
        /// <summary>   Determines if we can check context. </summary>
        ///
        /// <remarks>   Fess59, 26.01.2018. </remarks>
        ///
        /// <returns>   True if it succeeds, false if it fails. </returns>
        private static bool CheckContext()
        {
            var result = false;
            var context = ContextHelper.GetContext<TContext>(contextName);
            if (context == null)
                result = true;
            return result;
        }
        private static void DisposeContext(TContext data, bool isOwner)
        {
            try
            {
                if (isOwner)
                {
                    data.Dispose();
                    ContextHelper.SetContext<TContext>(null, contextName);
                }
            }
            catch (Exception ex)
            {
                ConsoleHelper.SendException(ex);
            }
        }
        #endregion
        #region Public DCT method
        /// <summary>   Executes void. </summary>
        ///
        /// <remarks>   Fess59, 26.01.2018. </remarks>
        ///
        /// <param name="action">                   The action. </param>
        /// <param name="continueExceptionMethod">  (Optional) The continue exception method. Выполнится
        ///                                         при ошибке в method. </param>
        /// <param name="continueMethod">           (Optional) The continue method. Выполнится после
        ///                                         method и continueExceptionMethod. </param>
        public static void Execute(Action<TContext> action, Action<TContext, Exception> continueExceptionMethod = null, Action<TContext> continueMethod = null, string name = "")
        {
            if (name == "")
                name = GetCategoryName();
            execute(name, action, continueExceptionMethod: continueExceptionMethod, continueMethod: continueMethod);
        }
        /// <summary>   Executes result. </summary>
        ///
        /// <remarks>   Fess59, 26.01.2018. </remarks>
        ///
        /// <typeparam name="TResult">  Type of the result. </typeparam>
        /// <param name="action">                   The action. </param>
        /// <param name="continueExceptionMethod">  (Optional) The continue exception method. Выполнится
        ///                                         при ошибке в method. </param>
        /// <param name="continueMethod">           (Optional) The continue method. Выполнится после
        ///                                         method и continueExceptionMethod. </param>
        ///
        /// <returns>   A TResult. </returns>
        public static TResult Execute<TResult>(Func<TContext, TResult> action, Action<TContext, Exception> continueExceptionMethod = null, Action<TContext> continueMethod = null)
        {
            return execute<TResult>(GetCategoryName(), action, continueExceptionMethod: continueExceptionMethod, continueMethod: continueMethod);
        }
        /// <summary>   Executes the asynchronous operation. With result </summary>
        ///
        /// <remarks>   Fess59, 26.01.2018. </remarks>
        ///
        /// <typeparam name="TResult">  Type of the result. </typeparam>
        /// <param name="action">                   The action. </param>
        /// <param name="complete">                 The complete. Для отправки результата в другой блок
        ///                                         кода, используется в ExecuteAsync<TReusult> </param>
        /// <param name="continueExceptionMethod">  (Optional) The continue exception method. Выполнится
        ///                                         при ошибке в method. </param>
        /// <param name="continueMethod">           (Optional) The continue method. Выполнится после
        ///                                         method и continueExceptionMethod. </param>
        public static void ExecuteAsync<TResult>(Func<TContext, TResult> action, Action<TContext, TResult> complete, Action<TContext, Exception> continueExceptionMethod = null, Action<TContext> continueMethod = null, string name = "")
        {
            if (name == "")
             name = GetCategoryName();
            Task.Factory.StartNew(() => execute(name, action, continueExceptionMethod: continueExceptionMethod, continueMethod: continueMethod, complete: complete));
        }
        /// <summary>   Executes the asynchronous operation. Void </summary>
        ///
        /// <remarks>   Fess59, 26.01.2018. </remarks>
        ///
        /// <param name="action">                   The action. </param>
        /// <param name="continueExceptionMethod">  (Optional) The continue exception method. Выполнится
        ///                                         при ошибке в method. </param>
        /// <param name="continueMethod">           (Optional) The continue method. Выполнится после
        ///                                         method и continueExceptionMethod. </param>
        public static void ExecuteAsync(Action<TContext> action, Action<TContext, Exception> continueExceptionMethod = null, Action<TContext> continueMethod = null)
        {
            var name = GetCategoryName();
            ExecuteAsync<object>(data => { action(data); return null; }, null, continueExceptionMethod: continueExceptionMethod, continueMethod: continueMethod, name: name);
        }
        /// <summary>   Executes the main thread operation. With result async </summary>
        ///
        /// <remarks>   Fess59, 26.01.2018. </remarks>
        ///
        /// <param name="action">                   The action. </param>
        /// <param name="continueExceptionMethod">  (Optional) The continue exception method. Выполнится
        ///                                         при ошибке в method. </param>
        /// <param name="continueMethod">           (Optional) The continue method. Выполнится после
        ///                                         method и continueExceptionMethod. </param>
        public static void ExecuteMainThread<TResult>(Func<TContext, TResult> action, Action<TContext, TResult> complete, Action<TContext, Exception> continueExceptionMethod = null, Action<TContext> continueMethod = null)
        {
            var name = GetCategoryName();
            DispatcherHelper.Execute(() => execute(name, action, continueExceptionMethod: continueExceptionMethod, continueMethod: continueMethod, complete: (data, result) => ExecuteAsync(dataAsync => complete.Invoke(dataAsync, result))));
        }
        /// <summary>   Executes the main thread operation. Void </summary>
        ///
        /// <remarks>   Fess59, 26.01.2018. </remarks>
        ///
        /// <param name="action">                   The action. </param>
        /// <param name="continueExceptionMethod">  (Optional) The continue exception method. Выполнится
        ///                                         при ошибке в method. </param>
        /// <param name="continueMethod">           (Optional) The continue method. Выполнится после
        ///                                         method и continueExceptionMethod. </param>
        public static void ExecuteMainThread(Action<TContext> action, Action<TContext, Exception> continueExceptionMethod = null, Action<TContext> continueMethod = null)
        {
            var name = GetCategoryName();
            DispatcherHelper.Execute(() => execute(name, action, continueExceptionMethod: continueExceptionMethod, continueMethod: continueMethod));
        }
        #endregion
        #region Logger module
        public static void SendInformations(string text, string category)
        {
            Send(LoggerMessageType.Information, text, category);
        }
        public static void SendWarning(string text, string category)
        {
            Send(LoggerMessageType.Warning, text, category);
        }
        public static void SendExceptions(string text, string category)
        {
            Send(LoggerMessageType.Exception, text, category);
        }
        public static void SendExceptions(Exception ex, string category)
        {
            Send(LoggerMessageType.Exception, ex.ToString(), category);
        }
        private static void Send(LoggerMessageType messageType, string text, string category)
        {
            Send(LoggerMessage.New(messageType, text, category));
        }
        private static void Send(LoggerMessage message)
        {
            LoggerHelper.SendMessage(message);
        }
        #endregion
        #region Tools
        public static int MethodNameLevel = 2;
        private static string GetMethodNameWrapper(int frame = 1)
        {
            return GetCategoryName(frame);
        }
        /// <summary>
        /// Получаем имя текущего метода
        /// </summary>
        /// <returns></returns>
        private static string GetCategoryName(int frame = 3)
        {
            var result = "";
            try 
            {
                if (!LoggerHelper.HasLoggerEnable.Value)
                    return result;
                StackTrace st = new StackTrace(1, false);
                StackFrame sf = st.GetFrame(frame);
                var method = sf.GetMethod();
                result = method.DeclaringType.Name;
            }
            catch (Exception e)
            {
                ConsoleHelper.SendException(e);
            }
            return result;
        }
        #endregion
        #region Pools
        //public static int TaskCount { get { return TaskPool.Current.CurrentTaskCount; } }
        //public static TaskPool TaskPool { get { return TaskPool.Current; } }
        //public static TaskSchedulePool SchdulePool { get { return TaskSchedulePool.Current; } }
        #endregion
    }
}
