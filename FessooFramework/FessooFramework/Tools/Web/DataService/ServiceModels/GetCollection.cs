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
    
    public class RequestGetCollection: DataRequestMessage<RequestGetCollection, ResponseGetCollection>
    {
        
    }
    
    public class ResponseGetCollection : DataResponseMessage<RequestGetCollection, ResponseGetCollection>

    {

    }
}
