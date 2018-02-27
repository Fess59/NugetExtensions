using ExampleServiceModels;
using FessooFramework.Tools.Web;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExampleServiceClient
{
    public class ServiceClient : BaseServiceClient
    {
        public override string Address => "http://localhost:56792/ServiceBase.svc";

        public override TimeSpan PostTimeout => TimeSpan.FromSeconds(100);

        public override string HashUID => "ExampleService";

        public override string SessionUID => "ExampleService";

        protected override IEnumerable<ServiceResponseConfigBase> Configurations => new ServiceResponseConfigBase[]{ ServiceResponseConfig<ResponseExampleModel>.New(a => Console.WriteLine(a.ResponseDescription + "!!!")) };
    }
}
