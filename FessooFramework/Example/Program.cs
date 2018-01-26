using FessooFramework.Tools.DCT;
using FessooFramework.Tools.Helpers;
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
            DCTDefault.Execute(data =>
            {
                ConsoleHelper.SendMessage($"Context 1 ID {data.Id}");
                DCTDefault.Execute(data2 =>
                {
                    ConsoleHelper.SendMessage($"Context 2 ID {data2.Id}");
                });
                DCTDefault.Execute(data3 =>
                {
                    ConsoleHelper.SendMessage($"Context 3 ID {data3.Id}");
                    DCTDefault.Execute(data4 =>
                    {
                        ConsoleHelper.SendMessage($"Context 4 ID {data4.Id}");
                    });
                });
                InternalMethod();
            });
            DCTDefault.Execute(data =>
            {
                ConsoleHelper.SendMessage($"Context 5 ID {data.Id}");
                InternalMethod();
            });
            DCTDefault.ExecuteAsync(data =>
            {
                ConsoleHelper.SendMessage($"Context 6 ID {data.Id}");
                InternalMethod();
            });
            Console.Read();
        }

        private static void InternalMethod()
        {
            var context = DCTDefault.Context;
            ConsoleHelper.SendMessage($"Internal ID {context.Id}");
        }
    }
}
