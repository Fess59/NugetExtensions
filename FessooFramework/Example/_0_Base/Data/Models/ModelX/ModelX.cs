﻿using FessooFramework.Objects.Data;
using FessooFramework.Tools.Helpers;
using FessooFramework.Tools.Repozitory;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Example._0_Base.Data.DataComponent.ModelX
{
    public class ModelX : EntityObjectALM<ModelX, ModelXState>
    {
        #region Property
        public string Description { get; set; }
        #endregion
        #region Abstracts
        protected override int GetStateValue(ModelXState state) { return (int)state; }
        protected override IEnumerable<EntityObjectALMConfiguration<ModelX, ModelXState>> Configurations => new EntityObjectALMConfiguration<ModelX, ModelXState>[] {
            EntityObjectALMConfiguration<ModelX, ModelXState>.New(ModelXState.Create, ModelXState.Edited, ModelXHelper.Edited),
              EntityObjectALMConfiguration<ModelX, ModelXState>.New(ModelXState.Edited, ModelXState.Edited2, ModelXHelper.Edited2),
               EntityObjectALMConfiguration<ModelX, ModelXState>.New(ModelXState.Edited, ModelXState.Edited3, ModelXHelper.Edited3),
              EntityObjectALMConfiguration<ModelX, ModelXState>.New(ModelXState.Error, ModelXState.Error, ModelXHelper.Error),
        };

        protected override ModelX SetValueDefault(ModelX oldObj, ModelX newObj)
        {
            oldObj.Description = "Default set value";
            return oldObj;
        }

        protected override IEnumerable<EntityObjectALMCreator<ModelX>> CreatorsService => new EntityObjectALMCreator<ModelX>[]
        {
              EntityObjectALMCreator<ModelX>.New<ModelXService>(ModelXToModelXService, ModelXServiceToModelX, new Version(1,0,0,0))
        };

        private ModelXService ModelXToModelXService(ModelX obj)
        {
            return new ModelXService()
            {
                Description = obj.Description
            };
        }
        private ModelX ModelXServiceToModelX(ModelXService obj, ModelX entity)
        {
            entity.Description = obj.Description;
            entity.StateEnum = ModelXState.Edited;
            return entity;
        }

       
        #endregion
    }
}
