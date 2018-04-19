using ExampleDataServiceClient.Core;
using ExampleDataServiceModels;
using FessooFramework.Tools.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ExampleDataServiceClient
{
    class Program
    {
        static string clientHash = "";
        static string sessionHash = "";
        static string email = "penzin2";
        static string phone = "799988888";
        static string password = "megaprogger";
        static string firstname = "Артур";
        static string secondname = "Пензин";
        static string middlename = "";
        static void Main(string[] args)
        {
            DCT.Execute(c =>
            {
                //Ping();
                Registaration(email, phone, password, firstname, secondname, middlename);
                while (true)
                {
                    Thread.Sleep(3000);
                    GetDataCollection();
                }
            });
            Thread.Sleep(10000);
            Console.ReadLine();
        }
        
        private static void Ping()
        {
            DCT.Execute(c =>
            {
                var ping = c.ServiceClient.Ping();
                Console.WriteLine($"Ping data - {ping}");
                using (var main = new MainClient())
                {
                    var ping2 = main.Ping();
                    Console.WriteLine($"Ping main - {ping2}");

                }
            });
        }

        static void Registaration(string email, string phone, string password, string firstname, string secondname, string middlename, DateTime? birthday = null)
        {
            using (var main = new MainClient())
                main.Registration(RegistarationCallback, email, phone, password, firstname, secondname, middlename);
        }
        static void RegistarationCallback(bool result)
        {
            if (result)
                Console.WriteLine($"Registration succesfull");
            else
                Console.WriteLine($"Registration not sucessfull");
            SignIn(email, password);
        }

        static void SignIn(string email, string password)
        {
            DCT.Execute(c =>
            {
                using (var main = new MainClient())
                    main.SignIn(SignInCallback, email, password);
            });
        }
        static void SignInCallback(bool result)
        {
            DCT.Execute(c =>
            {
                if (result)
                {
                    Console.WriteLine($"Signin succesfull");
                    clientHash = c._SessionInfo.HashUID;
                    sessionHash = c._SessionInfo.SessionUID;
                    DCT.ExecuteAsync(cc=>GetDataCollection());
                }
                else
                    Console.WriteLine($"Signin not sucessfull");

                Console.WriteLine($"SessionUID Request = {c._SessionInfo.SessionUID}");
                Console.WriteLine($"HashUID Request = {c._SessionInfo.HashUID}");
            });
        }
        static void GetDataCollection()
        {
            DCT.Execute(c =>
            {
                //var ping = c.ServiceClient.Ping();
                //var collection = c.ServiceClient.CollectionLoad<ExampleDataModel>();
                c._Store.ServiceContext<DataClient>().CollectionLoad<ExampleDataCache>(GetDataCollectionCallback);
            });
        }

        private static void GetDataCollectionCallback(IEnumerable<ExampleDataCache> collection)
        {
            DCT.Execute(c =>
            {
                var list = new List<ExampleDataCache>();
                list.Add(new ExampleDataCache());
                list.Add(new ExampleDataCache());
                list.Add(new ExampleDataCache());
                list.Add(new ExampleDataCache());
                list.Add(new ExampleDataCache());
                c.ServiceClient.Save<ExampleDataCache>((a) => { }, list.ToArray());
                //foreach (var item in collection)
                //{
                //    Console.WriteLine($"Description = {item.Description}");
                //    item.Description += "_C_";
                //    list.Add(item);
                //    c.ServiceClient.Save<ExampleDataCache>((a) => { }, item);

                //}
            });
        }
    }
}
