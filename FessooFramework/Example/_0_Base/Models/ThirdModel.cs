using System;
using System.Collections.Generic;
using System.Data.Entity.Core.Objects.DataClasses;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FessooFramework.Objects.Data;

namespace Example._0_Base.Models
{
    public class  ThirdModel : FessooFramework.Objects.Data.EntityObject
    {
        public string Decription { get; set; }

        public override IEnumerable<TDataModel> _CacheSave<TDataModel>(IEnumerable<TDataModel> objs)
        {
            throw new NotImplementedException();
        }

        public override IEnumerable<FessooFramework.Objects.Data.EntityObject> _CollectionObjectLoad()
        {
            throw new NotImplementedException();
        }

        public override FessooFramework.Objects.Data.EntityObject _ConvertToDataModel<TResult>(TResult obj)
        {
            throw new NotImplementedException();
        }

        public override TResult _ConvertToServiceModel<TResult>()
        {
            throw new NotImplementedException();
        }

        public override FessooFramework.Objects.Data.EntityObject _ObjectLoadById(Guid Id)
        {
            throw new NotImplementedException();
        }
    }
}
