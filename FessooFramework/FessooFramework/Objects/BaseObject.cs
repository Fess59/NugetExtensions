using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
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
        [JsonProperty("Id")]
        public Guid Id { get; protected set; }

        /// <summary>   Gets or sets the create date. </summary>
        ///
        /// <value> The create date. Default value <see cref="DateTime.Now()"/> </value>
        [JsonProperty("CreateDate")]

        public DateTime CreateDate { get; protected set; }

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
