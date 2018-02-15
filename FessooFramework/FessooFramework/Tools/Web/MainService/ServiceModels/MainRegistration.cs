using FessooFramework.Objects.Message;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FessooFramework.Tools.Web.MainService.ServiceModels
{
    public class Request_Registration : RequestMessage<Request_Registration, Response_Registration>
    {
        public string FirstName { get; set; }
        public string SecondName { get; set; }
        public string MiddleName { get; set; }
        public DateTime Birthday { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public string Login { get; set; }
        public string Password { get; set; }
        public string LoginHash { get; set; }
        public string EmailHash { get; set; }
        public string CompanyName { get; set; }
    }

    public class Response_Registration : ResponseMessage<Request_Registration, Response_Registration>
    {

    }
}
