using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExampleDataServiceClient
{
    public class ServiceClient : FessooFramework.Tools.Web.DataService.DataServiceClient
    {
        public override string Address => "http://localhost:56535/DataService.svc";

        public override TimeSpan PostTimeout => TimeSpan.FromSeconds(100);
    }
}
