﻿using FessooFramework.Core;
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
        public static DCTContextDefault Context { get { return _DCT<DCTContextDefault>.Context; } }
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
        public static void Execute(Action<DCTContextDefault> action, Action<DCTContextDefault, Exception> continueExceptionMethod = null, Action<DCTContextDefault> continueMethod = null, string name = "")
        {
            _DCT<DCTContextDefault>.Execute(action, continueExceptionMethod: continueExceptionMethod, continueMethod: continueMethod, name: name);
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
        public static TResult Execute<TResult>(Func<DCTContextDefault, TResult> action, Action<DCTContextDefault, Exception> continueExceptionMethod = null, Action<DCTContextDefault> continueMethod = null)
        {
            return _DCT<DCTContextDefault>.Execute(action, continueExceptionMethod: continueExceptionMethod, continueMethod: continueMethod);
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
        public static void ExecuteAsync<TResult>(Func<DCTContextDefault, TResult> action, Action<DCTContextDefault, TResult> complete, Action<DCTContextDefault, Exception> continueExceptionMethod = null, Action<DCTContextDefault> continueMethod = null)
        {
            _DCT<DCTContextDefault>.ExecuteAsync(action, complete, continueExceptionMethod: continueExceptionMethod, continueMethod: continueMethod);
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
        public static void ExecuteAsync(Action<DCTContextDefault> action, Action<DCTContextDefault, Exception> continueExceptionMethod = null, Action<DCTContextDefault> continueMethod = null)
        {
            _DCT<DCTContextDefault>.ExecuteAsync(action, continueExceptionMethod: continueExceptionMethod, continueMethod: continueMethod);
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
        public static void ExecuteMainThread(Action<DCTContextDefault> action, Action<DCTContextDefault, Exception> continueExceptionMethod = null, Action<DCTContextDefault> continueMethod = null)
        {
            _DCT<DCTContextDefault>.ExecuteMainThread(action, continueExceptionMethod: continueExceptionMethod, continueMethod: continueMethod);
        }
        #endregion
        #region Send message methods
        public static void SendExceptions(string text, string category)
        {
            _DCT<DCTContextDefault>.SendExceptions(text, category);
        }
        public static void SendExceptions(Exception ex, string category)
        {
            _DCT<DCTContextDefault>.SendExceptions(ex, category);
        }
        public static void SendInformations(string text, string category)
        {
            _DCT<DCTContextDefault>.SendInformations(text, category);
        }
        public static void SendWarning(string text, string category)
        {
            _DCT<DCTContextDefault>.SendWarning(text, category);
        }
        public static void Send(string text)
        {
            _DCT<DCTContextDefault>.Send(text);
        }
        #endregion
    }
}
