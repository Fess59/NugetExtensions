using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace FessooFramework.Tools.Helpers
{
    /// <summary>   A context helper.
    ///             Управление объектами в области данных потока </summary>
    ///
    /// <remarks>   Fess59, 26.01.2018. </remarks>

    public static class ContextHelper
    {
        /// <summary>   Gets the context.
        ///             Получаем объект из потока по имени и типу
        ///              </summary>
        ///
        /// <remarks>   Fess59, 26.01.2018. </remarks>
        ///
        /// <returns>   The context. </returns>
        public static T GetContext<T>(string name) where T : class
        {
            return Thread.GetData(Thread.GetNamedDataSlot(name)) as T;
        }

        /// <summary>   Sets a context.
        ///              Кладем объект в область данных потока по наименованию и типу
        ///              </summary>
        ///
        /// <remarks>   Fess59, 26.01.2018. </remarks>
        ///
        /// <typeparam name="T">    Generic type parameter. </typeparam>
        /// <param name="value">    The value. </param>
        ///
        /// <returns>   A T. </returns>

        public static T SetContext<T>(T value, string name) where T : class
        {
            Thread.SetData(Thread.GetNamedDataSlot(name), value);
            return value;
        }

        /// <summary>   Check or create context.
        ///             Проверяет наличие данных по имени и типу, при необходимости создаёт копию по умолчанию </summary>
        ///
        /// <remarks>   Fess59, 26.01.2018. </remarks>
        ///
        /// <typeparam name="T">    Generic type parameter. </typeparam>
        /// <param name="contextName">  Name of the context. </param>
        ///
        /// <returns>   A T. </returns>

        public static T CheckOrCreateContext<T>(string contextName) where T : class, new()
        {            
            var context = ContextHelper.GetContext<T>(contextName);
            var data = context == null ? ContextHelper.SetContext(new T(), contextName) : context;
            return data;
        }
    }
}
