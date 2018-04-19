using FessooFramework.Tools.Web.MainService;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExampleDataServiceClient
{
    public class MainClient : MainServiceClient
    {
        public override string Address => "http://176.111.73.51/MainService/MainService.svc";

        public override TimeSpan PostTimeout => TimeSpan.FromSeconds(100);

        public override string HashUID => "DataExample";

        public override string SessionUID => "DataExample";
    }
}
