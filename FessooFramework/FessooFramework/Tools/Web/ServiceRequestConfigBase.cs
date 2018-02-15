using FessooFramework.Objects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FessooFramework.Tools.Web
{
    public abstract class ServiceRequestConfigBase : SystemObject
    {
        public Type CurrentType { get; private set; }
        public abstract object Execute(object obj);
        public ServiceRequestConfigBase(Type type)
        {
            CurrentType = type;
        }
    }
}
