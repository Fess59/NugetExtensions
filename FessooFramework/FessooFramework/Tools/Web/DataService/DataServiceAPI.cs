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
        protected sealed override IEnumerable<ServiceRequestConfigBase> Configurations =>
            new ServiceRequestConfigBase[] {
                ServiceRequestConfig<RequestGet, ResponseGet>.New(ExecuteQuery),
            }.Concat(CustomConfigurations);

        protected abstract IEnumerable<ServiceRequestConfigBase> CustomConfigurations { get; }
       
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
        #region Methods
        public ResponseGet ExecuteQuery(RequestGet request)
        {
            var result = default(ResponseGet);
            DCT.DCT.Execute(c =>
            {
                result = new ResponseGet();
                result.QueryType = request.QueryType;
                result.HasCollection = request.HasCollection;
                var data = Enumerable.Empty<CacheObject>();
                if (result.HasCollection)
                    data = ExecuteObjectCollection(request);
                else
                    data = new CacheObject[] { ExecuteObject(request) };
                result.SetObjectCollections(data);
            });
            return result;
        }
        private CacheObject ExecuteObject(RequestGet request)
        {
            var result = default(CacheObject);
            DCT.DCT.Execute(c =>
            {
                var type = Type.GetType(request.CurrentType);
                var convertor = GetConvertor(type);
                switch (request.QueryType)
                {
                    case "_ObjectLoad":
                        result = convertor.ObjectLoad(request.Ids.FirstOrDefault());
                        break;
                    case "_SaveChanges":
                        {
                            var obj = convertor._JSONGetCollection(request.JSONObjectCollections);
                            result = convertor.ObjectSave(obj).FirstOrDefault();
                            break;
                        }
                    default:
                        {
                            var code = request.QueryType;
                            var sessionUID = request.SessionUID;
                            var hashUID = request.HashUID;
                            Guid id = request.Ids == null ? Guid.Empty : request.Ids.FirstOrDefault();
                            result = convertor._CustomCollectionLoad(code, sessionUID, hashUID, request.JSONObjectCollections, new[] { id }).FirstOrDefault();
                            break;
                        }
                }
            });
            return result;
        }
        private IEnumerable<CacheObject> ExecuteObjectCollection(RequestGet request)
        {
            var result = Enumerable.Empty<CacheObject>();
            DCT.DCT.Execute(c =>
            {
                var type = Type.GetType(request.CurrentType);
                var convertor = GetConvertor(type);
                switch (request.QueryType)
                {
                    case "_CollectionLoad":
                        result = convertor.CollectionObjectLoad();
                        break;
                    case "_SaveChanges":
                        var obj = convertor._JSONGetCollection(request.JSONObjectCollections);
                        result = convertor.ObjectSave(obj);
                        break;
                    default:
                        var code = request.QueryType;
                        var sessionUID = request.SessionUID;
                        var hashUID = request.HashUID;
                        var ids = request.Ids;
                        result = convertor._CustomCollectionLoad(code, sessionUID, hashUID, request.JSONObjectCollections, ids);
                        break;
                }
            });
            return result;
        }
        #endregion
    }
}
