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
        None = 0,
        Created = 1,
        Initialized = 2,
        Configured = 3,
        Loaded = 4,
        Launched = 5,
        Pause = 6,
        Complete = 7,
        Warnings = 8
    }
}
