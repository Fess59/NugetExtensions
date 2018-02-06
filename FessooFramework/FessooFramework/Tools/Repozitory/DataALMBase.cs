using FessooFramework.Objects.Data;
using FessooFramework.Tools.IOC;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FessooFramework.Tools.Repozitory
{
    /// <summary>   A data alm base. </summary>
    ///
    /// <remarks>   AM Kozhevnikov, 06.02.2018. </remarks>
    public abstract class DataALMBase : _IOCElement
    {
        /// <summary>   Gets the type of the object. </summary>
        ///
        /// <value> The type of the object. </value>

        public abstract Type ObjectType { get; }

        /// <summary>   Updates the given newObj. </summary>
        ///
        /// <remarks>   AM Kozhevnikov, 06.02.2018. </remarks>
        ///
        /// <param name="newObj">   The new object. </param>
        ///
        /// <returns>   True if it succeeds, false if it fails. </returns>
        public abstract bool Update(EntityObject newObj);
    }
}
