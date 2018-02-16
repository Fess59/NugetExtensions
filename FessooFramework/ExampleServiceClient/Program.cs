using ExampleServiceModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExampleServiceClient
{
    class Program
    {
        static void Main(string[] args)
        {
            using (var client = new ServiceClient())
            {
                var result = client.Ping();
                Console.WriteLine($"Ping = {result}");
                var response = client.Execute<RequestExampleModel, ResponseExampleModel>(new RequestExampleModel() { Description = $"Request - {DateTime.Now.ToShortTimeString()}" });
            }
            Console.Read();
        }
    }
}
