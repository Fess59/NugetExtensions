using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FessooFramework.Core
{

    public static class _DCT<TContext> where TContext : _DCTContext, new()
    {
        #region DCT base
        //1. Void
        //2. Result
        //3. Async
        //4. Async + ResultAction - обработка результа в отдельном методе


        private static TResult execute<TResult>(
            Func<TContext, TResult> method,
            TContext _data = null,
            Action<TContext, Exception> continueExceptionMethod = null,
            Action<TContext> continueMethod = null)
        {
            TResult result = default(TResult);
            try
            {
                if (method == null) throw new NullReferenceException("Parameter 'method' cannot be null");
                var data = _data == null ? new TContext() : _data;
                result = method(data);
                if (_data == null) data.Dispose();
            }
            catch (Exception e)
            {
                if (continueExceptionMethod != null)
                    execute((data) => continueExceptionMethod(data, e), _data);
            }
            finally
            {
                if (continueMethod != null)
                    execute((data) => continueMethod(data), _data);
            }
            return result;
        }
        private static void execute(Action<TContext> method,
           TContext _data = null,
           Action<TContext, Exception> continueExceptionMethod = null,
           Action<TContext> continueMethod = null)
        {
            execute<object>((data) => { method(data); return null; }, _data,continueExceptionMethod, continueMethod);
        }
        #endregion
        #region Logger module
        //public static bool IsLogEnable { get; set; }
        //public static int MethodNameLevel = 2;
        //public static string GetMethodNameWrapper(int frame = 1)
        //{
        //    return GetMethodName(frame);
        //}
        //internal static void LogDisable()
        //{
        //    if (IsLogEnable)
        //    {
        //        IsLogEnable = false;
        //    }
        //}
        //internal static void LogEnable()
        //{
        //    if (!IsLogEnable)
        //    {
        //        IsLogEnable = true;
        //    }
        //}
        //   /// <summary>
        /// Получаем имя текущего метода
        /// </summary>
        /// <returns></returns>
        //        internal static string GetMethodName(int frame = 1)
        //        {
        //            try ///Разрешённый try
        //            {
        //                StackTrace st = new StackTrace();
        //                StackFrame sf = st.GetFrame(frame);
        //                var method = sf.GetMethod();
        //                if (method.Name.Contains("<"))
        //                    return method.DeclaringType.FullName + "." + method.Name.Remove(0, 1).Remove(method.Name.IndexOf(">") - 1);
        //                return method.DeclaringType.FullName + "." + method.Name;
        //            }
        //            catch (Exception e)
        //            {
        //                Console.WriteLine(e);
        //            }
        //            return "";
        //        }
        //public static void SendInfo(System.Enum group, string comment, Guid? track = null)
        //{
        //    var message = new LogMessage(group);
        //    message.Comment = comment;
        //    message.Elapsed = 0;
        //    message.MessageType = LogMessageType.Message;
        //    //message.TrackNumber = track.Value;
        //    Send(message);
        //}
        //public static void SendWarning(System.Enum group, string comment, Guid? track = null)
        //{
        //    var message = new LogMessage(group);
        //    message.Comment = comment;
        //    message.Elapsed = 0;
        //    message.MessageType = LogMessageType.Message;
        //    //message.TrackNumber = track.Value;
        //    Send(message);
        //}
        //public static void SendExceptions(System.Enum group, string comment, Guid? track = null)
        //{
        //    var message = new LogMessage(group);
        //    message.Comment = comment;
        //    message.Elapsed = 0;
        //    message.MessageType = LogMessageType.Message;
        //    //message.TrackNumber = track.Value;
        //    Send(message);
        //}
        //public static void SendExceptions(System.Enum group, Exception ex, Guid? track = null)
        //{
        //    SendExceptions(group, ex.ToString(), track);
        //}
        //private static void Send(LogMessage message)
        //{
        //    if (IsLogEnable)
        //        Logger.Logger.Instance.Add(message);
        //}
        #endregion
        #region Pools
        //public static int TaskCount { get { return TaskPool.Current.CurrentTaskCount; } }
        //public static TaskPool TaskPool { get { return TaskPool.Current; } }
        //public static TaskSchedulePool SchdulePool { get { return TaskSchedulePool.Current; } }
        #endregion
        #region Methods
        ///// <summary>
        ///// Выполенение метода, для возврата значение используем стандартную обёртку с внешней переменной
        ///// </summary>
        ///// <param name="action"></param>
        //public static void Execute(Action<T> action, System.Enum group, object[] parameters = null, string comment = "", bool logInfo = true, Action<T, Exception> continueExceptionMethod = null, Action<T> continueMethod = null)
        //{
        //    var methodName = GetMethodName(MethodNameLevel);
        //    execute(action, group, parameters, comment, logInfo: logInfo, continueExceptionMethod: continueExceptionMethod, continueMethod: continueMethod, methodName: methodName);
        //}
        //public static TResult Execute<TResult>(Func<T, TResult> action, System.Enum group, object[] parameters = null, string comment = "", bool logInfo = true, Action<T, Exception> continueExceptionMethod = null, Action<T> continueMethod = null)
        //{
        //    var methodName = GetMethodName(MethodNameLevel);
        //    return execute<TResult>(action, group, parameters, comment, logInfo: logInfo, continueExceptionMethod: continueExceptionMethod, continueMethod: continueMethod, methodName: methodName);
        //}
        ///// <summary>
        ///// Асинхронное выполенение метода, без заморочек просто Task
        ///// </summary>
        ///// <param name="action"></param>
        //public static void ExecuteAsync(Action<T> action, System.Enum group, object[] parameters = null, string comment = "", bool logInfo = true, Action<T, Exception> continueExceptionMethod = null, Action<T> continueMethod = null)
        //{
        //    var methodName = GetMethodName(MethodNameLevel);
        //    var task = new Task(() => execute(action, group, parameters, comment, logInfo: logInfo, continueExceptionMethod: continueExceptionMethod, continueMethod: continueMethod, methodName: methodName));
        //    TaskStart(task);
        //    //TaskPool.Execute((a) => execute(action, group, parameters, comment, logInfo: logInfo, continueExceptionMethod: continueExceptionMethod, continueMethod: continueMethod, methodName: methodName));
        //}
        ///// <summary>
        ///// Асинхронное выполенение метода, без заморочек просто Task
        ///// </summary>
        ///// <param name="action"></param>
        //public static void ExecuteCurrentDispatcher(Action<T> action, System.Enum group, object[] parameters = null, string comment = "", bool logInfo = true, Action<T, Exception> continueExceptionMethod = null, Action<T> continueMethod = null)
        //{
        //    var methodName = GetMethodName(MethodNameLevel);
        //    DispatcherHelper.Execute(() => execute(action, group, parameters, comment, logInfo: logInfo, continueExceptionMethod: continueExceptionMethod, continueMethod: continueMethod, methodName: methodName));
        //}
        ///// <summary>
        ///// Асинхронное выполенение метода, без заморочек просто Task
        ///// </summary>
        ///// <param name="action"></param>
        //public static Task<TResult> ExecuteAsync<TResult>(Func<T, TResult> action, System.Enum group, object[] parameters = null, string comment = "", bool logInfo = true, Action<T, Exception> continueExceptionMethod = null, Action<T> continueMethod = null)
        //{
        //    var methodName = GetMethodName(MethodNameLevel);
        //    var task = new Task<TResult>(() => execute<TResult>(action, group, parameters, comment, logInfo: logInfo, continueExceptionMethod: continueExceptionMethod, continueMethod: continueMethod, methodName: methodName));
        //    return TaskStart(task);
        //}
        ///// <summary>
        ///// Асинхронное выполенение метода, без заморочек просто Task
        ///// </summary>
        ///// <param name="action"></param>
        //public static void ExecuteAsyncPool(Action<T> action, System.Enum group, object[] parameters = null, string comment = "", bool logInfo = true, Action<T, Exception> continueExceptionMethod = null, Action<T> continueMethod = null)
        //{
        //    var methodName = GetMethodName(MethodNameLevel);
        //    var task = new Task(() => execute(action, group, parameters, comment, logInfo: logInfo, continueExceptionMethod: continueExceptionMethod, continueMethod: continueMethod, methodName: methodName));
        //    TaskStart(task);
        //    //ThreadPoolHelper.Execute(() => execute(action, group, parameters, comment, logInfo: logInfo, continueExceptionMethod: continueExceptionMethod, continueMethod: continueMethod, methodName: methodName));
        //    //TaskPool.Execute((a) => execute(action, group, parameters, comment, logInfo: logInfo, continueExceptionMethod: continueExceptionMethod, continueMethod: continueMethod, methodName: methodName));
        //}
      
        #endregion
        #region Task dispatcher
        ///// <summary>
        ///// Заготовка
        ///// </summary>
        ///// <param name="task"></param>
        //public static Task TaskStart(Task task)
        //{
        //    task.Start();
        //    return task;
        //}
        ///// <summary>
        ///// Заготовка
        ///// </summary>
        ///// <param name="task"></param>
        //public static Task<TResult> TaskStart<TResult>(Task<TResult> task)
        //{

        //    task.Start();
        //    return task;
        //}
        #endregion
    }
}
