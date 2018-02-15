using FessooFramework.Tools.Web;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FessooFramework.Objects.Message
{
    public abstract class RequestMessageBase : MessageObject
    {
        public long CurrentTimestamp { get; set; }
        public abstract object _Execute(ServiceBaseAPI service, object obj);
    }
}
