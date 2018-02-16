using FessooFramework.Objects.Message;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExampleServiceModels
{
    public class RequestExampleModel : RequestMessage<RequestExampleModel, ResponseExampleModel>
    {
        public string Description { get; set; }
    }
    public class ResponseExampleModel : ResponseMessage<RequestExampleModel, ResponseExampleModel>
    {
        public string ResponseDescription { get; set; }

    }
}
