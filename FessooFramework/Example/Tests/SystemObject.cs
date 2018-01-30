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
        public override void _Compliting()
        {
            ConsoleHelper.SendMessage($"{this.GetType().Name} => _Compliting Complete");
        }

        public override void _Configuring()
        {
            ConsoleHelper.SendMessage($"{this.GetType().Name} => _Configuring Complete");
        }

        public override void _Launching()
        {
            ConsoleHelper.SendMessage($"{this.GetType().Name} => _Launching Complete");
        }

        public override void _Loading()
        {
            ConsoleHelper.SendMessage($"{this.GetType().Name} => _Loading Complete");
        }

        public override void _Warnings()
        {
            throw new NotImplementedException();
        }
    }
}
