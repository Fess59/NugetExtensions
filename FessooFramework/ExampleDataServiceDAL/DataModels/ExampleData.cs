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
            EntityObjectALMConfiguration<ExampleData, ExampleDataState>.New(ExampleDataState.Create, ExampleDataState.Edit, Edit),
             EntityObjectALMConfiguration<ExampleData, ExampleDataState>.New(ExampleDataState.Edit, ExampleDataState.Complete, Edit),
               EntityObjectALMConfiguration<ExampleData, ExampleDataState>.New(ExampleDataState.Edit, ExampleDataState.Edit, Edit),
                  EntityObjectALMConfiguration<ExampleData, ExampleDataState>.New(ExampleDataState.Error, ExampleDataState.Error, Edit),
        };

        private ExampleData Edit(ExampleData arg1, ExampleData arg2)
        {
            arg1.D = arg2.D;
            return arg1;
        }
        protected override IEnumerable<ExampleDataState> DefaultState => new[] { ExampleDataState.Error };

        protected override IEnumerable<EntityObjectALMCreator<ExampleData>> CreatorsService => new EntityObjectALMCreator<ExampleData>[] 
        {
            EntityObjectALMCreator<ExampleData>.New<ExampleDataModel>( ExampleDataToExampleDataModel, new Version(1,0,0,0))
        };

        private ExampleDataModel ExampleDataToExampleDataModel(ExampleData arg)
        {
            return new ExampleDataModel()
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
