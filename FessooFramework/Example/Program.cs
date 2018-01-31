using Example.Tests;
using FessooFramework.Core;
using FessooFramework.Tools.DCT;
using FessooFramework.Tools.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Example
{
    public class Program
    {
        static void Main(string[] args)
        {
            CoreTest();
            var r = SynchronizationContext.Current;
            DCTNameTest();
            Console.Read();
        }
        #region DCT test
        private static void DCTNameTest()
        {
            DCTDefault.Execute(c =>{});
            DCTDefault.Execute<string>(c => { return ""; });
            DCTDefault.ExecuteAsync(c => {  });
            DCTDefault.ExecuteAsync<string>(c => { return "";  }, (c, result)=> { });
            DCTDefault.ExecuteMainThread(c => {  });
        }

        private static void DCTTest()
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
        }
        private static void InternalMethod()
        {
            var context = DCTDefault.Context;
            ConsoleHelper.SendMessage($"Internal ID {context.Id}");
        }
        #endregion
        #region ALM test
        static void ALMtest()
        {
            var almo = new ALMModel();
            DCTDefault.Execute(c =>
            {
                almo._SetState(ALMModelState.First);
            });
            DCTDefault.Execute(c =>
            {
                almo._SetState(ALMModelState.Create);
                almo._SetState(ALMModelState.First);
                almo._SetState(ALMModelState.Second);
                almo._SetState(ALMModelState.Third);
            });

        }
        static void ObjectAndComponent()
        {
            var soe = new SystemObjectExample();
            var sce = new SystemComponentExample();
            DCTDefault.Execute(c =>
            {
                sce._SetState(FessooFramework.Objects.SystemState.Initialized);
                sce._SetState(FessooFramework.Objects.SystemState.Configured);
                sce._SetState(FessooFramework.Objects.SystemState.Loaded);
                sce._SetState(FessooFramework.Objects.SystemState.Launched);
            });

        }

        #endregion
        #region CoreTest
        public static void CoreTest()
        {
            new Example.Tests.CoreExample.Bootstrapper().Run();
        }
        #endregion
    }
}
