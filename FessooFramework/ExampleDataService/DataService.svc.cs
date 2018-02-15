using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Web;
using System.Text;
using ExampleDataServiceDAL.Core;
using ExampleDataServiceDAL.DataModels;
using ExampleDataServiceModels;
using FessooFramework.Objects.Data;
using FessooFramework.Objects.Message;
using FessooFramework.Tools.Web.DataService.Configuration;

namespace ExampleDataService
{
    [ServiceBehavior(InstanceContextMode = InstanceContextMode.Single, ConcurrencyMode = ConcurrencyMode.Multiple, AutomaticSessionShutdown = true, UseSynchronizationContext = false)]
    [ServiceContract]
    public class DataService : FessooFramework.Tools.Web.DataService.DataServiceAPI
    {
        public override IEnumerable<DataServiceConfigurationBase> Convertors => new DataServiceConfigurationBase[]
        {
            new DataServiceConfiguration<ExampleData, ExampleDataCache>(),
        };

        [WebInvoke(
           Method = "POST",
           UriTemplate = "Ping",
           RequestFormat = WebMessageFormat.Xml,
           ResponseFormat = WebMessageFormat.Xml)]
        [OperationContract]
        public override bool Ping(string p)
        {
            DCT.Execute(c => 
            {
                for (int i = 0; i < 10; i++)
                {
                    var model = new ExampleDataServiceDAL.DataModels.ExampleData();
                    model.D = i.ToString();
                    model.StateEnum = ExampleDataServiceDAL.DataModels.ExampleDataState.Edit;
                }
                c.SaveChanges();
            });
            return _Ping(p);
        }
        [WebInvoke(
        Method = "GET",
        UriTemplate = "Stat",
        RequestFormat = WebMessageFormat.Json,
        ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        public override string Stat()
        {
            return _Stat(this);
        }
        [WebInvoke(
         Method = "POST",
         UriTemplate = "Execute",
         RequestFormat = WebMessageFormat.Xml,
         ResponseFormat = WebMessageFormat.Xml)]
        [OperationContract]
        public override ServiceMessage Execute(ServiceMessage data)
        {
            var result = default(ServiceMessage);
            DCT.Execute(c => {
                result = _Execute(data);
            });
            return result;
        }

    }
}
