using FessooFramework.Objects.Message;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FessooFramework.Tools.Services.ServiceModels
{
    public class Request_SignIn : RequestMessage<Request_SignIn, Response_SignIn>
    {
        public string Login { get; set; }
        public string Password { get; set; }

    }
    public class Response_SignIn : ResponseMessage<Request_SignIn, Response_SignIn>
    {

      
    }
}
