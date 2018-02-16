using ExampleServiceModels;
using FessooFramework.Objects;
using FessooFramework.Objects.Message;
using FessooFramework.Tools.Web;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Web;
using System.Text;

namespace ExampleService
{
    [ServiceBehavior(InstanceContextMode = InstanceContextMode.Single, ConcurrencyMode = ConcurrencyMode.Multiple, AutomaticSessionShutdown = true, UseSynchronizationContext = false)]
    [ServiceContract]
    public class ServiceBase : FessooFramework.Tools.Web.ServiceBaseAPI
    {
        public override string Name => "Тестовая базовая служба";

        protected override IEnumerable<ServiceRequestConfigBase> Configurations => new ServiceRequestConfigBase[] 
        {
            ServiceRequestConfig<RequestExampleModel, ResponseExampleModel>.New((a) => new ResponseExampleModel() { ResponseDescription = a.Description + " Well done" }),
        };

        [WebInvoke(
         Method = "POST",
         UriTemplate = "Ping",
         RequestFormat = WebMessageFormat.Xml,
         ResponseFormat = WebMessageFormat.Xml
         )]
        [OperationContract]
        public override bool Ping(string p)
        {
            return _Ping(p);
        }
        [WebInvoke(
          Method = "GET",
          UriTemplate = "Stat",
          RequestFormat = WebMessageFormat.Json,
          ResponseFormat = WebMessageFormat.Json
          )]
        [OperationContract]
        public override string Stat()
        {
            return _Stat(this);
        }
        [WebInvoke(
          Method = "POST",
          UriTemplate = "Execute",
          RequestFormat = WebMessageFormat.Xml,
          ResponseFormat = WebMessageFormat.Xml
          )]
        [OperationContract]
        public override ServiceMessage Execute(ServiceMessage message)
        {
            return _Execute(message);
        }
    }
}
