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
        public static object GetContext(string name)
        {
            return Thread.GetData(Thread.GetNamedDataSlot(name));
        }

        /// <summary>   Sets a context.
        ///              Кладем объект в область данных потока по наименованию и типу
        ///              </summary>
        ///
        /// <remarks>   Fess59, 26.01.2018. </remarks>
        ///
        /// <param name="value">    The value. </param>
        /// <param name="name">    The value. </param>
        ///
        /// <returns>   A T. </returns>

        public static object SetContext(object value, string name) 
        {
            Thread.SetData(Thread.GetNamedDataSlot(name), value);
            return value;
        }
    }
}
