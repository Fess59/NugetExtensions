using Example._0_Base.ClientModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Example
{
    public class Program
    {
        static void Main(string[] args)
        {
            new TestInheritance_3().Set();
            var model = new ClientCacheModel();
            model = model;
            //Bootstrapper.Current.Run();
            Console.Read();
        }
    }
}
