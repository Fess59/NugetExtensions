using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FessooFramework.Objects.ALM;

namespace Example._0_Base.Data.DataComponent.ModelX
{
    public class ModelXALM : ALMComponent<ModelX, ModelXState>
    {
        protected override IEnumerable<ModelXState> DefaultState => new ModelXState[] { ModelXState.Error };
      
        protected override IEnumerable<ALMConfiguration<ModelX, ModelXState>> Configurations => new ALMConfiguration<ModelX, ModelXState>[] {
            ALMConfiguration<ModelX, ModelXState>.New(ModelXState.Create, ModelXState.Edited, Edited),
              ALMConfiguration<ModelX, ModelXState>.New(ModelXState.Edited, ModelXState.Edited2, Edited2),
               ALMConfiguration<ModelX, ModelXState>.New(ModelXState.Edited, ModelXState.Edited3, Edited3),
              ALMConfiguration<ModelX, ModelXState>.New(ModelXState.Error, ModelXState.Error, Error),
        };
        protected override ModelXState GetType(ModelX obj)
        {
            return obj.StateEnum;
        }
        protected override void SetType(ModelX obj, ModelXState state)
        {
            obj.StateEnum = state;
        }


        private ModelX Edited(ModelX oldObj, ModelX newObj)
        {
            oldObj.Description = "Edited";
            return oldObj;
        }
        private ModelX Edited2(ModelX oldObj, ModelX newObj)
        {
            oldObj.Description = "Edited2";
            return oldObj;
        }
        private ModelX Edited3(ModelX oldObj, ModelX newObj)
        {
            oldObj.Description = "Edited3";
            return oldObj;
        }
        private ModelX Error(ModelX oldObj, ModelX newObj)
        {
            oldObj.Description = "Error";
            return oldObj;
        }


    }
}
