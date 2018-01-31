using FessooFramework.Objects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Example.Tests.CoreExample.Components
{
    public class SingltonApplicationHelper : SystemComponent
    {
        protected override void _6_Unload()
        {
        }

        public override void _2_Configuring()
        {
        }

        public override void _5_Launching()
        {
        }

        protected override void _3_Loaded()
        {
        }

        protected override IEnumerable<TestingCase> _4_Testing()
        {
            return Enumerable.Empty<TestingCase>();
        }
    }
}
