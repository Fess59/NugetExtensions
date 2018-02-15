using FessooFramework.Components;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExampleDataServiceClient.Core
{
    public class ClientContext : DCTContext
    {
        public ServiceClient ServiceClient => _Store.ServiceContext<ServiceClient>();
        public ClientContext()
        {
            _Store.Add<ServiceClient>();
        }
    }
}
