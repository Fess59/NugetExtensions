using FessooFramework.Components;
using FessooFramework.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FessooFramework.Tools.DCT
{
    /// <summary>   A dct default. Базовая реализация DCT - используем в проектах, где не требуется кастомная реализация </summary>
    ///
    /// <remarks>   AM Kozhevnikov, 07.02.2018. </remarks>

    public static class DCTDefault 
    {
        #region Property
        /// <summary>   Gets the context.  </summary>
        ///
        /// <value> . </value>
        public static DCTContext Context { get { return _DCT.Context<DCTContext>(); } }
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
        public static void Execute(Action<DCTContext> action, Action<DCTContext, Exception> continueExceptionMethod = null, Action<DCTContext> continueMethod = null, string name = "")
        {
            _DCT.Execute<DCTContext>(action, continueExceptionMethod: continueExceptionMethod, continueMethod: continueMethod, name: name);
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
        public static TResult Execute<TResult>(Func<DCTContext, TResult> action, Action<DCTContext, Exception> continueExceptionMethod = null, Action<DCTContext> continueMethod = null)
        {
            return _DCT.Execute<DCTContext, TResult>(action, continueExceptionMethod: continueExceptionMethod, continueMethod: continueMethod);
        }
        /// <summary>   Executes the asynchronous operation. With result </summary>
        ///
        /// <remarks>   Fess59, 26.01.2018. </remarks>
        ///
        /// <typeparam name="TResult">  Type of the result. </typeparam>
        /// <param name="action">                   The action. </param>
        /// <param name="complete">                 The complete. Для отправки результата в другой блок
        ///                                         кода, используется в ExecuteAsync </param>
        /// <param name="continueExceptionMethod">  (Optional) The continue exception method. Выполнится
        ///                                         при ошибке в method. </param>
        /// <param name="continueMethod">           (Optional) The continue method. Выполнится после
        ///                                         method и continueExceptionMethod. </param>
        public static void ExecuteAsync<TResult>(Func<DCTContext, TResult> action, Action<DCTContext, TResult> complete, Action<DCTContext, Exception> continueExceptionMethod = null, Action<DCTContext> continueMethod = null)
        {
            _DCT.ExecuteAsync<DCTContext, TResult>(action, complete, continueExceptionMethod: continueExceptionMethod, continueMethod: continueMethod);
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
        public static void ExecuteAsync(Action<DCTContext> action, Action<DCTContext, Exception> continueExceptionMethod = null, Action<DCTContext> continueMethod = null)
        {
            _DCT.ExecuteAsync<DCTContext>(action, continueExceptionMethod: continueExceptionMethod, continueMethod: continueMethod);
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
        public static void ExecuteMainThread(Action<DCTContext> action, Action<DCTContext, Exception> continueExceptionMethod = null, Action<DCTContext> continueMethod = null)
        {
            _DCT.ExecuteMainThread<DCTContext>(action, continueExceptionMethod: continueExceptionMethod, continueMethod: continueMethod);
        }
        #endregion
        #region Send message methods

        /// <summary>   Sends the exceptions. </summary>
        ///
        /// <remarks>   AM Kozhevnikov, 07.02.2018. </remarks>
        ///
        /// <param name="text">     The text. </param>
        /// <param name="category"> The category. </param>

        public static void SendExceptions(string text, string category)
        {
            _DCT.SendExceptions(text, category);
        }

        /// <summary>   Sends the exceptions. </summary>
        ///
        /// <remarks>   AM Kozhevnikov, 07.02.2018. </remarks>
        ///
        /// <param name="ex">       The ex. </param>
        /// <param name="category"> The category. </param>

        public static void SendExceptions(Exception ex, string category)
        {
            _DCT.SendExceptions(ex, category);
        }

        /// <summary>   Sends the informations. </summary>
        ///
        /// <remarks>   AM Kozhevnikov, 07.02.2018. </remarks>
        ///
        /// <param name="text">     The text. </param>
        /// <param name="category"> The category. </param>

        public static void SendInformations(string text, string category)
        {
            _DCT.SendInformations(text, category);
        }

        /// <summary>   Sends a warning. </summary>
        ///
        /// <remarks>   AM Kozhevnikov, 07.02.2018. </remarks>
        ///
        /// <param name="text">     The text. </param>
        /// <param name="category"> The category. </param>

        public static void SendWarning(string text, string category)
        {
            _DCT.SendWarning(text, category);
        }

        /// <summary>   Send this message. </summary>
        ///
        /// <remarks>   AM Kozhevnikov, 07.02.2018. </remarks>
        ///
        /// <param name="text"> The text. </param>

        public static void Send(string text)
        {
            _DCT.Send(text);
        }
        #endregion
    }
}
