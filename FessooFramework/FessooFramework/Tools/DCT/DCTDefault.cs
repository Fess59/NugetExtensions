using FessooFramework.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FessooFramework.Tools.DCT
{
    public static class DCTDefault
    {
        #region Property
        public static _DCTContext Context { get { return _DCT.Context<_DCTContext>(); } }
        #endregion
        #region Execute methods
        /// <summary>   Executes void. </summary>
        ///
        /// <remarks>   Fess59, 26.01.2018. </remarks>
        ///
        /// <param name="action">                   The action. </param>
        /// <param name="continueExceptionMethod">  (Optional) The continue exception method. Выполнится
        ///                                         при ошибке в method. </param>
        /// <param name="continueMethod">           (Optional) The continue method. Выполнится после
        ///                                         method и continueExceptionMethod. </param>
        public static void Execute(Action<_DCTContext> action, Action<_DCTContext, Exception> continueExceptionMethod = null, Action<_DCTContext> continueMethod = null, string name = "")
        {
            _DCT.Execute<_DCTContext>(action, continueExceptionMethod: continueExceptionMethod, continueMethod: continueMethod, name: name);
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
        public static TResult Execute<TResult>(Func<_DCTContext, TResult> action, Action<_DCTContext, Exception> continueExceptionMethod = null, Action<_DCTContext> continueMethod = null)
        {
            return _DCT.Execute<_DCTContext, TResult>(action, continueExceptionMethod: continueExceptionMethod, continueMethod: continueMethod);
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
        public static void ExecuteAsync<TResult>(Func<_DCTContext, TResult> action, Action<_DCTContext, TResult> complete, Action<_DCTContext, Exception> continueExceptionMethod = null, Action<_DCTContext> continueMethod = null)
        {
            _DCT.ExecuteAsync<_DCTContext, TResult>(action, complete, continueExceptionMethod: continueExceptionMethod, continueMethod: continueMethod);
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
        public static void ExecuteAsync(Action<_DCTContext> action, Action<_DCTContext, Exception> continueExceptionMethod = null, Action<_DCTContext> continueMethod = null)
        {
            _DCT.ExecuteAsync<_DCTContext>(action, continueExceptionMethod: continueExceptionMethod, continueMethod: continueMethod);
        }
        /// <summary>   Executes the main thread operation. </summary>
        ///
        /// <remarks>   Fess59, 26.01.2018. </remarks>
        ///
        /// <param name="action">                   The action. </param>
        /// <param name="continueExceptionMethod">  (Optional) The continue exception method. Выполнится
        ///                                         при ошибке в method. </param>
        /// <param name="continueMethod">           (Optional) The continue method. Выполнится после
        ///                                         method и continueExceptionMethod. </param>
        public static void ExecuteMainThread(Action<_DCTContext> action, Action<_DCTContext, Exception> continueExceptionMethod = null, Action<_DCTContext> continueMethod = null)
        {
            _DCT.ExecuteMainThread<_DCTContext>(action, continueExceptionMethod: continueExceptionMethod, continueMethod: continueMethod);
        }
        #endregion
        #region Send message methods
        public static void SendExceptions(string text, string category)
        {
            _DCT.SendExceptions(text, category);
        }
        public static void SendExceptions(Exception ex, string category)
        {
            _DCT.SendExceptions(ex, category);
        }
        public static void SendInformations(string text, string category)
        {
            _DCT.SendInformations(text, category);
        }
        public static void SendWarning(string text, string category)
        {
            _DCT.SendWarning(text, category);
        }
        public static void Send(string text)
        {
            _DCT.Send(text);
        }
        #endregion
    }
}
