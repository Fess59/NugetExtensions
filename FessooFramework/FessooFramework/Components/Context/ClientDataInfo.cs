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
        public string HashUID { get => hashUID; set => hashUID = value; }
        public string SessionUID { get => sessionUID; set => sessionUID = value; }
        private static string hashUID { get; set; }
        public static string sessionUID { get; set; }
    }
}
