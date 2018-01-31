using FessooFramework.Core;
using FessooFramework.Objects.SourceData;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FessooFramework.Tools.DCT
{
    /// <summary>   A dct context default.
    ///             Базовая реализация контекста данных для DCTDefault
    ///             Создана для внутренних целей фреймворка или проектов без собственной реализации </summary>
    ///
    /// <remarks>   Fess59, 26.01.2018. </remarks>
    public class DCTContextDefault : _DCTContext
    {
        public DataContextStore DataContextStore { get; set; }
        public DCTContextDefault()
        {
        }
    }
}
