using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FessooFramework.Objects.ALM;
using FessooFramework.Tools.Repozitory;

namespace Example._0_Base.Data.DataComponent.ModelX
{
    public static class ModelXHelper
    {
        internal static ModelX Edited(ModelX oldObj, ModelX newObj)
        {
            oldObj.Description = "Edited";
            return oldObj;
        }
        internal static ModelX Edited2(ModelX oldObj, ModelX newObj)
        {
            oldObj.Description = "Edited2";
            return oldObj;
        }
        internal static ModelX Edited3(ModelX oldObj, ModelX newObj)
        {
            oldObj.Description = "Edited3";
            return oldObj;
        }
        internal static ModelX Error(ModelX oldObj, ModelX newObj)
        {
            oldObj.Description = "Error";
            return oldObj;
        }
    }
}
