﻿using FessooFramework.Core;
using FessooFramework.Objects;
using FessooFramework.Objects.SourceData;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExampleServiceClient
{
    public class Bootstrapper : FessooFramework.Core.Bootstrapper<Bootstrapper>
    {
        public override string ApplicationName => "Application Example";

        public override void SetComponents(ref List<SystemComponent> components)
        {
        }

        public override void SetConfiguration(ref SystemCoreConfiguration settings)
        {
        }

        public override void SetDbContext(ref DataContextStore _Store)
        {
            
        }
    }
}
