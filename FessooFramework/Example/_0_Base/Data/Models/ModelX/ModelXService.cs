using FessooFramework.Objects.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Example._0_Base.Data.DataComponent.ModelX
{
    public class ModelXService : CacheObject
    {
        public string Description { get; set; }

        public override TimeSpan SetTTL()
        {
            return new TimeSpan();
        }

        public override Version SetVersion()
        {
            return new System.Version(1,0,0,0);
        }
    }
}
