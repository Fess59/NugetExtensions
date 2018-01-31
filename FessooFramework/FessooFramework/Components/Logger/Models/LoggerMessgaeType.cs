using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FessooFramework.Components.LoggerComponent.Models
{
    /// <summary>   Values that represent logger message types. </summary>
    ///
    /// <remarks>   AM Kozhevnikov, 30.01.2018. </remarks>

    public enum LoggerMessageType
    {
        Information = 0,
        Warning = 1,
        Exception = 2,
        Testing = 3,
    }
}
