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
        /// <summary>   Executes void. </summary>
        ///
        /// <remarks>   Fess59, 26.01.2018. </remarks>
        ///
        /// <param name="action">                   The action. </param>
        /// <param name="continueExceptionMethod">  (Optional) The continue exception method. Выполнится
        ///                                         при ошибке в method. </param>
        /// <param name="continueMethod">           (Optional) The continue method. Выполнится после
        ///                                         method и continueExceptionMethod. </param>
        public static void Execute(Action<DCTContextDefault> action, Action<DCTContextDefault, Exception> continueExceptionMethod = null, Action<DCTContextDefault> continueMethod = null)
        {
            _DCT<DCTContextDefault>.Execute(action, continueExceptionMethod: continueExceptionMethod, continueMethod: continueMethod);
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

        public static DCTContextDefault Context { get { return _DCT<DCTContextDefault>.Context; } }
    }
}
