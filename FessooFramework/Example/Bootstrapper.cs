using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Example._0_Base.Data.Contexts;
using Example.Tests.CoreExample.Components;
using FessooFramework.Core;
using FessooFramework.Objects;
using FessooFramework.Objects.SourceData;

namespace Example
{
    public class Bootstrapper : FessooFramework.Core.Bootstrapper<Bootstrapper>
    {
        public override string ApplicationName => "Application Example";

        public override void SetComponents(ref List<SystemComponent> components)
        {
           components.Add(new SingltonApplicationHelper());
        }

        public override void SetConfiguration(ref SystemCoreConfiguration settings)
        {
        }

        public override void SetDbContext(ref DataContextStore _Store)
        {
            _Store.Add<DefaultDB>();
            _Store.Add<DefaultDB2>();
            _Store.Add<DefaultDB3>();
        }
    }
}
