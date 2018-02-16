using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FessooFramework.Objects.Data;
using FessooFramework.Objects.Message;
using FessooFramework.Tools.Web.DataService.Configuration;
using FessooFramework.Tools.Web.DataService.ServiceModels;

namespace FessooFramework.Tools.Web.DataService
{
    public abstract class DataServiceAPI : ServiceBaseAPI
    {
        #region Property
        public override string Name => "DataServiceAPI";
        protected override IEnumerable<ServiceRequestConfigBase> Configurations =>
            new ServiceRequestConfigBase[] {
                ServiceRequestConfig<RequestGetCollection, ResponseGetCollection>.New(GetCollection),
            };
        #endregion
        #region Constructor
        public DataServiceAPI()
        {
            if (_Convertors == null)
                _Convertors = Convertors;
        }
        #endregion
        #region Convertor
        public abstract IEnumerable<DataServiceConfigurationBase> Convertors { get; }
        private static IEnumerable<DataServiceConfigurationBase> _Convertors { get; set; }
        private DataServiceConfigurationBase GetConvertor(Type dataModel)
        {
            if (!_Convertors.Any())
                throw new Exception($"Не определён ни один ковертор на службе данных");
            var configuration = _Convertors.SingleOrDefault(q => q.CacheType == dataModel);
            if (configuration == null)
                throw new Exception($"Не определён конвертор для '{dataModel.ToString()}'");
           return configuration;
        }
        #endregion


        public ResponseGetCollection GetCollection(RequestGetCollection request)
        {
            var result = default(ResponseGetCollection);
            DCT.DCT.Execute(c => {
                var type = Type.GetType(request.CurrentType);
                var convertor = GetConvertor(type);
                result = new ResponseGetCollection();
                var data = convertor.CollectionObjectLoad();
                result.SetObjectCollections(data);
            });
            return result;
        }
    }
}
