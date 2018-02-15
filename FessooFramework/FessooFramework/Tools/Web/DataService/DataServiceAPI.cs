using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FessooFramework.Objects.Data;
using FessooFramework.Objects.Message;
using FessooFramework.Tools.Web.DataService.ServiceModels;

namespace FessooFramework.Tools.Web.DataService
{
    public abstract class DataServiceAPI : ServiceBaseAPI
    {
        public override string Name => "DataServiceAPI";
        public abstract IEnumerable<CacheObject> GetData(Type type);
        protected override IEnumerable<ServiceRequestConfigBase> Configurations => 
            new ServiceRequestConfigBase[] {
                ServiceRequestConfig<RequestGetCollection, ResponseGetCollection>.New(GetCollection),
            };

        public ResponseGetCollection GetCollection(RequestGetCollection request)
        {
            var result = default(ResponseGetCollection);
            DCT.DCT.Execute(c => {
                var type = Type.GetType(request.CurrentType);
                result = new ResponseGetCollection();
                result.SetObjectCollections(GetData(type));
            });
            return result;
        }
    }
}
