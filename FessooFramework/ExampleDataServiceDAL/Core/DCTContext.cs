using ExampleDataServiceDAL.Contexts;
using FessooFramework.Components;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExampleDataServiceDAL.Core
{
    public class DALContext : DCTContext
    {
        public DALContext()
        {
            _Store.Add<DALDB>("DALDB", "192.168.26.116", @"ExtUser", "123QWEasd");
        }
    }
}
