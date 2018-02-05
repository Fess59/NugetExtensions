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
    public static class _DCT
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
        private static TResult execute<TContext, TResult>(
            string name,
            Func<TContext, TResult> method,
            Action<TContext, Exception> continueExceptionMethod = null,
            Action<TContext> continueMethod = null,
             Action<TContext, TResult> complete = null)
             where TContext : _DCTContext, new()
        {
            TResult result = default(TResult);
            bool isOwner = false;
            try
            {
                if (method == null) throw new NullReferenceException("Parameter 'method' cannot be null");
                //Статус владельца контекста
                isOwner = CheckContext<TContext>();
                result = method(Context<TContext>());

            }
            catch (Exception ex)
            {
                if (continueExceptionMethod != null)
                    execute<TContext>(name, dataEx => continueExceptionMethod(dataEx, ex));
                SendExceptions(ex, name);
            }
            finally
            {
                if (continueMethod != null)
                    execute<TContext>(name, dataCon => continueMethod(dataCon));
                if (complete != null)
                    execute<TContext>(name, dataCom => complete(dataCom, result));
                //TODO Tracker and Analitics
                //SendInformations($@"Complete", name);
            }
            DisposeContext(Context<TContext>(), isOwner);
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
        private static void execute<TContext>(string name, Action<TContext> method,
           Action<TContext, Exception> continueExceptionMethod = null,
           Action<TContext> continueMethod = null)
             where TContext : _DCTContext, new()
        {
            execute<TContext, object>(name, data => { method(data); return null; }, continueExceptionMethod, continueMethod);
        }
        #endregion
        #region Context base
        static string contextName = "DCTContext";
        public static TContext Context<TContext>()
             where TContext : _DCTContext, new()
        {
            return GetContext<TContext>();
        }
        /// <summary>    Gets a context.
        ///              Получаю контекст из области данных потока </summary>
        ///
        /// <remarks>    Fess59, 26.01.2018. </remarks>
        ///
        /// <param name="value"> (Optional) The value. </param>
        ///
        /// <returns>    The context. </returns>
        private static TContext GetContext<TContext>(TContext value = null)
              where TContext : _DCTContext, new()
        {
                var obj = ContextHelper.GetContext(contextName);
                if (obj == null)
                {
                    obj = new TContext();
                    ContextHelper.SetContext(obj, contextName);
                }
                var context = obj as TContext;
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
        private static bool CheckContext<TContext>()
              where TContext : _DCTContext, new()
        {
            var result = false;
            var context = ContextHelper.GetContext(contextName);
            if (context == null)
                result = true;
            return result;
        }
        private static void DisposeContext<TContext>(TContext data, bool isOwner)
              where TContext : _DCTContext, new()
        {
            try
            {
                if (isOwner)
                {
                    data.Dispose();
                    ContextHelper.SetContext(null, contextName);
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
        public static void Execute<TContext>(Action<TContext> action, Action<TContext, Exception> continueExceptionMethod = null, Action<TContext> continueMethod = null, string name = "")
             where TContext : _DCTContext, new()
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
        public static TResult Execute<TContext,TResult>(Func<TContext, TResult> action, Action<TContext, Exception> continueExceptionMethod = null, Action<TContext> continueMethod = null)
             where TContext : _DCTContext, new()
        {
            return execute<TContext,TResult>(GetCategoryName(), action, continueExceptionMethod: continueExceptionMethod, continueMethod: continueMethod);
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
        public static void ExecuteAsync<TContext,TResult>(Func<TContext, TResult> action, Action<TContext, TResult> complete, Action<TContext, Exception> continueExceptionMethod = null, Action<TContext> continueMethod = null, string name = "")
             where TContext : _DCTContext, new()
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
        public static void ExecuteAsync<TContext>(Action<TContext> action, Action<TContext, Exception> continueExceptionMethod = null, Action<TContext> continueMethod = null)
             where TContext : _DCTContext, new()
        {
            var name = GetCategoryName();
            ExecuteAsync<TContext,object>(data => { action(data); return null; }, null, continueExceptionMethod: continueExceptionMethod, continueMethod: continueMethod, name: name);
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
        public static void ExecuteMainThread<TContext,TResult>(Func<TContext, TResult> action, Action<TContext, TResult> complete, Action<TContext, Exception> continueExceptionMethod = null, Action<TContext> continueMethod = null)
             where TContext : _DCTContext, new()
        {
            var name = GetCategoryName();
            DispatcherHelper.Execute(() => execute(name, action, continueExceptionMethod: continueExceptionMethod, continueMethod: continueMethod, complete: (data, result) => ExecuteAsync<TContext>(dataAsync => complete.Invoke(dataAsync, result))));
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
        public static void ExecuteMainThread<TContext>(Action<TContext> action, Action<TContext, Exception> continueExceptionMethod = null, Action<TContext> continueMethod = null)
             where TContext : _DCTContext, new()
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
            Send(LoggerMessageType.Exception, LoggerHelper.ExToString(ex), category);
        }
        public static void Send(string text)
        {
            Send(LoggerMessage.New(LoggerMessageType.Information, text, "Message"));
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
                if (sf != null)
                {
                    var method = sf.GetMethod();
                    result = method.DeclaringType.Name;
                }
                else if (frame > 1)
                {
                    result = GetCategoryName(frame - 1);
                }
                else
                {
                    result = "UNKNOW";
                }
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
