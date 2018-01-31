using FessooFramework.Components.LoggerComponent.Models;
using FessooFramework.Components.LoggerComponent.Parts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FessooFramework.Components.LoggerComponent.Realizations
{
    public class ServiceLogger : LoggerElement
    {
        public ServiceLogger() : base(LoggerElementType.Service)
        {
        }
        public override bool SendMessage(LoggerMessage message)
        {
            return false;
        }
    }
}
