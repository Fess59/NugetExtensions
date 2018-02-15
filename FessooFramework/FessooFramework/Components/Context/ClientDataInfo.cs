using FessooFramework.Objects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FessooFramework.Components.Context
{
    public class ClientSessionInfo : SystemObject
    {
        public string HashUID { get; set; }
        public string SessionUID { get; set; }
    }
}
