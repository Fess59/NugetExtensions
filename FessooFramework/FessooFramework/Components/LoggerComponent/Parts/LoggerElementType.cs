using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FessooFramework.Components.LoggerComponent.Parts
{
    /// <summary>   Values that represent logger element types. </summary>
    ///
    /// <remarks>   AM Kozhevnikov, 30.01.2018. </remarks>

    public enum LoggerElementType
    {
        Console = 0,
        File = 1,
        Service = 2,
        Action = 3
    }
}
