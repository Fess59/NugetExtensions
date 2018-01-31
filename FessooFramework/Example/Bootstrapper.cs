using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Example._0_Base.Data.Contexts;
using Example.Tests.CoreExample.Components;
using FessooFramework.Core;
using FessooFramework.Objects;

namespace Example.Tests.CoreExample
{
    public class Bootstrapper : FessooFramework.Core.Bootstrapper
    {
        public override string ApplicationName => "Application Example";

        public override void SetComponents(ref List<SystemComponent> components)
        {
           components.Add(new SingltonApplicationHelper());
        }

        public override void SetConfiguration(ref SystemCoreConfiguration settings)
        {
        }
    }
}
