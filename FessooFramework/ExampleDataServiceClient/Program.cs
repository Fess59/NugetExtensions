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
            var clientHash = "";
            var sessionHash = "";
            DCT.Execute(c => {
                using (var main = new MainClient())
                {
                    var email = "ttt3@ttt.ru";
                    var phone = "799988888";
                    var password = "ttt3";
                    var firstname = "name";
                    var secondname = "sec";
                    var middlename = "sec";
                    var registration = main.Registration(email, phone, password, firstname, secondname, middlename);
                    if (registration)
                        Console.WriteLine($"Registration succesfull");
                    else
                        Console.WriteLine($"Registration not sucessfull");

                    var signin = main.SignIn(email, password);
                    if (signin)
                        Console.WriteLine($"Signin succesfull");
                    else
                        Console.WriteLine($"Signin not sucessfull");

                    clientHash = c._SessionInfo.HashUID;
                    sessionHash = c._SessionInfo.SessionUID;
                }   
               
                Console.WriteLine($"SessionUID Request = {c._SessionInfo.SessionUID}");
                Console.WriteLine($"HashUID Request = {c._SessionInfo.HashUID}");

                c.ServiceClient.Ping();

                //var collection = c.ServiceClient.CollectionLoad<ExampleDataModel>();
                var collection = c._Store.ServiceContext<DataClient>().CollectionLoad<ExampleDataCache>();
                foreach (var item in collection)
                {
                    Console.WriteLine($"Description = {item.Description}");
                }
            });
            Console.ReadLine();
        }

    }
}
