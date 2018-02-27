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
        public override string Address => "http://localhost:53681/MainService.svc";

        public override TimeSpan PostTimeout => TimeSpan.FromSeconds(100);

        public override string HashUID => "DataExample";

        public override string SessionUID => "DataExample";
    }
}
