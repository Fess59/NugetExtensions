using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FessooFramework.Objects
{
    /// <summary>   A base object. A class that inherits all objects in the system  </summary>
    ///
    /// <remarks>   AM Kozhevnikov, 11.01.2018. </remarks>

    public class BaseObject
    {
        /// <summary>   Gets or sets the identifier. </summary>
        ///
        /// <value>  The identifier. Default value <see cref="Guid.NewGuid()"/></value>

        public Guid Id { get; private set; }

        /// <summary>   Gets or sets the create date. </summary>
        ///
        /// <value> The create date. Default value <see cref="DateTime.Now()"/> </value>

        public DateTime CreateDate { get; private set; }

        /// <summary>   Default constructor. Fills in the identifier and create date</summary>
        ///
        /// <remarks>   AM Kozhevnikov, 11.01.2018. </remarks>

        public BaseObject()
        {
            Id = Guid.NewGuid();
            CreateDate = DateTime.Now;
        }
    }
}
