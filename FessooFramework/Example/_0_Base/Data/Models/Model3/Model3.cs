using FessooFramework.Objects.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Example._0_Base.Data.Models.Model3
{
    public class Model3 : EntityObjectALM<Model3, Model3State>
    {
        public string Description { get; set; }
        protected override IEnumerable<EntityObjectALMConfiguration<Model3, Model3State>> Configurations =>  new EntityObjectALMConfiguration<Model3, Model3State>[] 
        {
            new EntityObjectALMConfiguration<Model3, Model3State>(Model3State.Create, Model3State.Edit, (a,b)=>{ a.Description = "Edit"; return a; }),
            new EntityObjectALMConfiguration<Model3, Model3State>(Model3State.Edit, Model3State.Complete, (a,b)=>{ a.Description = "Complete"; return a; }),
            new EntityObjectALMConfiguration<Model3, Model3State>(Model3State.Complete, Model3State.Edit, (a,b)=>{ a.Description = "Re-edit"; return a; })
        };
        protected override IEnumerable<Model3State> DefaultState => new Model3State[] { Model3State.Error };

        protected override IEnumerable<EntityObjectALMCreator<Model3>> CreatorsService => throw new NotImplementedException();

        protected override int GetStateValue(Model3State state)
        {
            return (int)state;
        }
    }
    public enum Model3State
    {
        Create = 0,
        Edit = 1,
        Complete = 2,
        Error = -1
    }
}
