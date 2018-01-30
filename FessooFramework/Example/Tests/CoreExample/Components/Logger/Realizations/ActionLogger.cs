using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Example.Tests.CoreExample.Components.Logger.Elements
{
    public class ActionLogger : LoggerElement
    {
        public ActionLogger() : base(LoggerElementType.Action)
        {
        }

        public override bool SendMessage(LoggerMessage message)
        {
            return false;
        }
    }
}
