using FessooFramework.Objects.Data;
using FessooFramework.Tools.Web.DataService.ServiceModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FessooFramework.Tools.Web.DataService
{
    public abstract class DataServiceClient : BaseServiceClient
    {
        protected override IEnumerable<ServiceResponseConfigBase> Configurations => new ServiceResponseConfigBase[] {  };

        public IEnumerable<TCacheObject> CollectionLoad<TCacheObject>()
            where TCacheObject : CacheObject
        {
            var result = Enumerable.Empty<TCacheObject>();
            DCT.DCT.Execute(c=> 
            {
                var request = new RequestGetCollection();
                request.CurrentType = typeof(TCacheObject).AssemblyQualifiedName;
                var response = Execute<RequestGetCollection, ResponseGetCollection>(request);
                result = response.GetObjectCollections<TCacheObject>().ToArray();
            });
            return result;
        }
        public TCacheObject ObjectLoad<TCacheObject>(Guid id)
           where TCacheObject : CacheObject
        {
            var result = default(TCacheObject);
            DCT.DCT.Execute(c =>
            {
                var request = new RequestGetCollection();
                request.CurrentType = typeof(TCacheObject).AssemblyQualifiedName;
                //var response = Execute<RequestGetCollection, ResponseGetCollection>(request);
                //result = response.GetObject<TCacheObject>();
            });
            return result;
        }
    }
}
