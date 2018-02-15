using ExampleDataServiceClient.Core;
using ExampleDataServiceModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExampleDataServiceClient
{
    class Program
    {
        static void Main(string[] args)
        {
            DCT.Execute(c => {

                c.ServiceClient.Ping();

                //var collection = c.ServiceClient.CollectionLoad<ExampleDataModel>();
                var collection = c._Store.ServiceContext<ServiceClient>().CollectionLoad<ExampleDataModel>();
                foreach (var item in collection)
                {
                    Console.WriteLine($"Description = {item.Description}");
                }
            });
            Console.ReadLine();
        }

    }
}
