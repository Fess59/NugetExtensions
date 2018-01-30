using FessooFramework.Objects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FessooFramework.Tools.IOC
{
    /// <summary>   An IOC element. </summary>
    ///
    /// <remarks>   AM Kozhevnikov, 11.01.2018. </remarks>

    public class _IOCElement : SystemObject
    {
        /// <summary>    Gets or sets the UID. </summary>
        ///
        /// <value>  The UID of IOC element. </value>
        public string UID { get; set; }
        /// <summary>   Gets or sets a value indicating whether this object is enable. 
        ///             Отключает работу компонента, используется для управления множеством реализаций или временным отключением блока кода
        ///             </summary>
        ///
        /// <value> True if this object is enable, false if not. </value>
        public bool HasEnable { get; set; }

        /// <summary>   Default constructor. </summary>
        ///
        /// <remarks>   AM Kozhevnikov, 30.01.2018. </remarks>

        public _IOCElement()
        {
            HasEnable = true;
        }
    }
}
