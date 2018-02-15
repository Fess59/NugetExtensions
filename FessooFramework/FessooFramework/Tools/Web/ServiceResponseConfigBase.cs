using FessooFramework.Objects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FessooFramework.Tools.Web
{
    public abstract class ServiceResponseConfigBase : SystemObject
    {
        public Type CurrentType { get; private set; }
        public abstract void Execute(object obj);
        public ServiceResponseConfigBase(Type type)
        {
            CurrentType = type;
        }
    }
}
