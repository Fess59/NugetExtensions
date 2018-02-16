using FessooFramework.Components;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FessooFramework.Core
{
    /// <summary>   A dct default. Базовая реализация DCT - используем в проектах, где не требуется кастомная реализация </summary>
    ///
    /// <remarks>   AM Kozhevnikov, 07.02.2018. </remarks>
    public class _DCT<TContext> : _DCTBase
        where TContext : DCTContext, new()
    {
        #region Property
        /// <summary>   Gets the context.  </summary>
        ///
        /// <value> . </value>
        public static TContext Context { get { return Context<TContext>(); } }
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
        public static void Execute(Action<TContext> action, Action<TContext, Exception> continueExceptionMethod = null, Action<TContext> continueMethod = null, string name = "")
        {
            _Execute<TContext>(action, continueExceptionMethod: continueExceptionMethod, continueMethod: continueMethod, name: name);
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
            return _Execute<TContext, TResult>(action, continueExceptionMethod: continueExceptionMethod, continueMethod: continueMethod);
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
        public static void ExecuteAsync<TResult>(Func<TContext, TResult> action, Action<TContext, TResult> complete, Action<TContext, Exception> continueExceptionMethod = null, Action<TContext> continueMethod = null)
        {
            _ExecuteAsync<TContext, TResult>(action, complete, continueExceptionMethod: continueExceptionMethod, continueMethod: continueMethod);
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
            _ExecuteAsync<TContext>(action, continueExceptionMethod: continueExceptionMethod, continueMethod: continueMethod);
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
        public static void ExecuteMainThread(Action<TContext> action, Action<TContext, Exception> continueExceptionMethod = null, Action<TContext> continueMethod = null, bool isAsync = true)
        {
            _ExecuteMainThread<TContext>(action, continueExceptionMethod: continueExceptionMethod, continueMethod: continueMethod, isAsync: isAsync);
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
            _SendExceptions(text, category);
        }

        /// <summary>   Sends the exceptions. </summary>
        ///
        /// <remarks>   AM Kozhevnikov, 07.02.2018. </remarks>
        ///
        /// <param name="ex">       The ex. </param>
        /// <param name="category"> The category. </param>

        public static void SendExceptions(Exception ex, string category)
        {
            _SendExceptions(ex, category);
        }

        /// <summary>   Sends the informations. </summary>
        ///
        /// <remarks>   AM Kozhevnikov, 07.02.2018. </remarks>
        ///
        /// <param name="text">     The text. </param>
        /// <param name="category"> The category. </param>

        public static void SendInformations(string text, string category)
        {
            _SendInformations(text, category);
        }

        /// <summary>   Sends a warning. </summary>
        ///
        /// <remarks>   AM Kozhevnikov, 07.02.2018. </remarks>
        ///
        /// <param name="text">     The text. </param>
        /// <param name="category"> The category. </param>

        public static void SendWarning(string text, string category)
        {
            _SendWarning(text, category);
        }

        /// <summary>   Send this message. </summary>
        ///
        /// <remarks>   AM Kozhevnikov, 07.02.2018. </remarks>
        ///
        /// <param name="text"> The text. </param>

        public static void Send(string text)
        {
            _Send(text);
        }
        #endregion
    }
}
