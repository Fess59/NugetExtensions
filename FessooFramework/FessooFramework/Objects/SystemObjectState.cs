using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FessooFramework.Objects
{
    /// <summary>   Values that represent system object states. </summary>
    ///
    /// <remarks>   AM Kozhevnikov, 29.01.2018. </remarks>

    public enum SystemState
    {
        Created = 0,
        Initialized = 1,
        Configured = 2,
        Loaded = 3,
        Testing = 4,
        Launched = 5,
        Unload = 6,
    }
}
