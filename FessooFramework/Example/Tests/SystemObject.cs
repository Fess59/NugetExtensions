using FessooFramework.Objects;
using FessooFramework.Tools.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Example.Tests
{
    public class SystemObjectExample : SystemObject
    {

    }
    public class SystemComponentExample : SystemComponent
    {
        public override void _6_Unload()
        {
            ConsoleHelper.SendMessage($"{this.GetType().Name} => _Compliting Complete");
        }

        public override void _2_Configuring()
        {
            ConsoleHelper.SendMessage($"{this.GetType().Name} => _Configuring Complete");
        }

        public override void _5_Launching()
        {
            ConsoleHelper.SendMessage($"{this.GetType().Name} => _Launching Complete");
        }

        public override void _3_Loaded()
        {
            ConsoleHelper.SendMessage($"{this.GetType().Name} => _Loading Complete");
        }

        public override IEnumerable<TestingCase> _4_Testing()
        {
            return Enumerable.Empty<TestingCase>();
        }
    }
}
