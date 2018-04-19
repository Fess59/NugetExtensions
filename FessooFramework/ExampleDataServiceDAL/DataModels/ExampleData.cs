using ExampleDataServiceDAL.Core;
using ExampleDataServiceModels;
using FessooFramework.Objects.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExampleDataServiceDAL.DataModels
{
    public class ExampleData : EntityObjectALM<ExampleData, ExampleDataState>
    {
        public string D { get; set; }

        protected override IEnumerable<EntityObjectALMConfiguration<ExampleData, ExampleDataState>> Configurations => new EntityObjectALMConfiguration<ExampleData, ExampleDataState>[] 
        {
            EntityObjectALMConfiguration<ExampleData, ExampleDataState>.New(ExampleDataState.Create, ExampleDataState.Edit, SetValueDefault),
             EntityObjectALMConfiguration<ExampleData, ExampleDataState>.New(ExampleDataState.Edit, ExampleDataState.Complete, SetValueDefault),
               EntityObjectALMConfiguration<ExampleData, ExampleDataState>.New(ExampleDataState.Edit, ExampleDataState.Edit, SetValueDefault),
                  EntityObjectALMConfiguration<ExampleData, ExampleDataState>.New(ExampleDataState.Error, ExampleDataState.Error, SetValueDefault),
        };
        protected override ExampleData SetValueDefault(ExampleData oldObj, ExampleData newObj)
        {
            oldObj.D = newObj.D;
            return oldObj;
        }
        protected override IEnumerable<EntityObjectALMCreator<ExampleData>> CreatorsService => new EntityObjectALMCreator<ExampleData>[] 
        {
            EntityObjectALMCreator<ExampleData>.New<ExampleDataCache>( ExampleDataToExampleDataModel, ExampleDataModelToExampleData ,new Version(1,0,0,0))
        };
        private ExampleData ExampleDataModelToExampleData(ExampleDataCache arg, ExampleData entity)
        {
           var c = DCT.Context;
            entity.Id = arg.Id;
            entity.D = arg.Description;
            entity.StateEnum = ExampleDataState.Edit;
            return entity;
        }
        public override IEnumerable<TDataModel> _CacheSave<TDataModel>(IEnumerable<TDataModel> objs)
        {
            return base._CacheSave(objs);
        }

        public override IEnumerable<EntityObject> _CollectionObjectLoad()
        {
            return base._CollectionObjectLoad();
        }

        private ExampleDataCache ExampleDataToExampleDataModel(ExampleData arg)
        {
            return new ExampleDataCache()
            {
                Description = arg.D
            };
        }
       
        protected override int GetStateValue(ExampleDataState state)
        {
            return (int)state;
        }

       
    }
    public enum ExampleDataState
    {
        Create = 0,
        Edit =1,
        Complete =2,
        Error = 3
    }
}
