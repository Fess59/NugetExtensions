using FessooFramework.Objects.Data;
using FessooFramework.Objects.Message;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace FessooFramework.Tools.Web.DataService.ServiceModels
{
    
    public class RequestGet: DataRequestMessage<RequestGet, ResponseGet>
    {
        /// <summary>   Gets or sets the type of the message. </summary>
        ///
        /// <value> The type of the message. </value>

        public string QueryType { get; set; }
        public bool HasCollection { get; set; }
    }
    
    public class ResponseGet : DataResponseMessage<RequestGet, ResponseGet>
    {
        /// <summary>   Gets or sets the type of the message. </summary>
        ///
        /// <value> The type of the message. </value>
        public string QueryType { get; set; }
        public bool HasCollection { get; set; }
    }
}
