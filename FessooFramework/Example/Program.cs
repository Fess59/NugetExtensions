using Example._0_Base.Data;
using Example._0_Base.Data.DataComponent.ModelX;
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
            DCTExample.Execute(c =>
            {
                CoreTest();
            });

            DataComponentTest();

            Console.Read();
        }
        #region DCT test
        private static void DCTNameTest()
        {
            DCTDefault.Execute(c => { });
            DCTDefault.Execute<string>(c => { return ""; });
            DCTDefault.ExecuteAsync(c => { });
            DCTDefault.ExecuteAsync<string>(c => { return ""; }, (c, result) => { });
            DCTDefault.ExecuteMainThread(c => { });
        }

        private static void DCTTest()
        {
            DCTExample.Execute(data =>
            {
                ConsoleHelper.SendMessage($"Context 1 ID {data.Id}");
                DCTExample.Execute(data2 =>
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
            DCTExample.Execute(data =>
            {
                ConsoleHelper.SendMessage($"Context 5 ID {data.Id}");
                InternalMethod();
            });
            DCTExample.ExecuteAsync(data =>
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
        #region Entity test
        public static void EntityTest()
        {
            DCTExample.Execute(c =>
            {
                c.FirstModels.Add(new _0_Base.Models.FirstModel() { Decription = "1" });
                EntityTest2();
                c.FirstModels.Add(new _0_Base.Models.FirstModel() { Decription = "3" });
                c.SaveChanges();
            });
        }
        public static void EntityTest2()
        {
            DCTExample.Context.FirstModels.Add(new _0_Base.Models.FirstModel() { Decription = "2" });
        }
        #endregion
        #region DataComponent test
        public static void DataComponentTest()
        {
            //ALM
            var model = new ModelX();
            DCTExample.Execute(c =>
            {
                #region 1 default
                //Create + Create to Edited
                model.StateEnum = ModelXState.Edited;
                model._Save();
                DCTExample.Context.SaveChanges();

                //Edited to Edited2
                model.StateEnum = ModelXState.Edited2;
                model._Save();
                DCTExample.Context.SaveChanges();

                //Edited2 to Error
                model.StateEnum = ModelXState.Error;
                model._Save();
                DCTExample.Context.SaveChanges();
                #endregion
                #region 2 Create to Edited2 - error
                var model2 = new ModelX();
                //Create + Create to Edited
                model2.StateEnum = ModelXState.Edited2;
                model2._Save();
                DCTExample.Context.SaveChanges();
                #endregion
                #region 3 Create to Error - error
                var model3 = new ModelX();
                //Create + Create to Edited
                model3.StateEnum = ModelXState.Error;
                model3._Save();
                DCTExample.Context.SaveChanges();
                #endregion
                #region 4 Create to Edited
                var model4 = new ModelX();
                //Create + Create to Edited
                model4.StateEnum = ModelXState.Edited;
                model4._Save();
                DCTExample.Context.SaveChanges();
                #endregion
                #region 5 Create to Edited2
                var model5 = new ModelX();
                //Create + Create to Edited
                model5.StateEnum = ModelXState.Edited;
                model5._Save();
                DCTExample.Context.SaveChanges();
                model5.StateEnum = ModelXState.Edited2;
                model5._Save();
                DCTExample.Context.SaveChanges();
                #endregion



                //Query
                var set = model._DbSet();
                set = ModelX.DbSet();
                set = DCTExample.Context.DbSet<ModelX>();
                set = c.DbSet<ModelX>();

                //Creators
                var visualModel = model._Convert<ModelXView>();
            });

        }
        #endregion
    }
}
