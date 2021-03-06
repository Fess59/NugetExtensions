﻿using Example._0_Base.Data;
using Example._0_Base.Data.DataComponent.ModelX;
using Example._0_Base.Data.Models.Model3;
using Example.Tests;
using FessooFramework.Core;
using FessooFramework.Tools.Controllers;
using FessooFramework.Tools.DataContexts;
using FessooFramework.Tools.DataContexts.Models;
using FessooFramework.Tools.DCT;
using FessooFramework.Tools.Helpers;
using FessooFramework.Tools.Web;
using System;
using System.Collections.Generic;
using System.Diagnostics;
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
            DCTExample.Execute(c =>
            {
                QueueTaskController2();
                //var r = new Model3();
                //r.StateEnum = Model3State.Edit;
                //r.StateEnum = Model3State.Edit;
                //c.SaveChanges();
                //r.StateEnum = Model3State.Complete;
                //c.SaveChanges();
                //r.StateEnum = Model3State.Edit;
                //c.SaveChanges();
                //var r2 = new Model3();
                //r2.StateEnum = Model3State.Complete;
                //c.SaveChanges();
                ////DataComponentTest();
                //var list = Model3.DbSet().ToArray();
                //foreach (var item in list)
                //{
                //    ConsoleHelper.Send("Info", $"Create={item.ToString()} Description={item.Description}");
                //}
                //var model = new ModelX();
                //var visualModel = model._ConvertToServiceModel<ModelXService>();
            });

            Console.Read();
        }
        #region DCT test
        private static void DCTNameTest()
        {
            DCT.Execute(c => { });
            DCT.Execute<string>(c => { return ""; });
            DCT.ExecuteAsync(c => { });
            DCT.ExecuteAsync<string>(c => { return ""; }, (c, result) => { });
            DCT.ExecuteMainThread(c => { });
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
                DCT.Execute(data3 =>
                {
                    ConsoleHelper.SendMessage($"Context 3 ID {data3.Id}");
                    DCT.Execute(data4 =>
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
            var context = DCT.Context;
            ConsoleHelper.SendMessage($"Internal ID {context.Id}");
        }
        #endregion
        #region ALM test
        static void ALMtest()
        {
            var almo = new ALMModel();
            DCT.Execute(c =>
            {
                almo._SetState(ALMModelState.First);
            });
            DCT.Execute(c =>
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
            DCT.Execute(c =>
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
            Example.Bootstrapper.Current.Run();
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
                var visualModel = model._ConvertToServiceModel<ModelXService>();
            });

        }
        #endregion
        #region MainDbAPI
        private static void UserCreate()
        {
            ////1. Регистрация пользователя
            //var r = new Request_Registration()
            //{
            //    Email = "ttt@ttt.ru",
            //    Birthday = new DateTime(2018, 01, 01),
            //    Phone = "899988888",
            //    Password = "123QWE",
            //    FirstName = "Alex",
            //    SecondName = "S",
            //    MiddleName = "M",
            //};
            //var service1 = BaseServiceClient._Send<Request_Registration, Response_Registration>(r);
            ////var service1 = MainServiceAPI.User_Registration(r);
            //DCTExample.SendInformations($"{service1.Comment} State = {service1.StateEnum}", "MainServiceAPI");
            ////2. Авторизация 
            //var r2 = new Request_SignIn()
            //{
            //    Login = "ttt@ttt.ru",
            //    Password = "123QWE",
            //};
            //var service2 = MainServiceAPI.User_SignIn(r2);
            //DCTExample.SendInformations($"{service2.Comment} State = {service2.StateEnum}", "MainServiceAPI");


           
        }
        #endregion
        #region QueueController
        private static void QueueTaskController2()
        {
            //QueueTaskController.Current.TaskCount = 10;
            ConsoleHelper.SendMessage("Start");
            var sw = new Stopwatch();
            sw.Start();
            //Start
            for (int i = 0; i < 100; i++)
            {
                var ii = i;
                var result = ii.ToString();
                QueueTaskController.Current.Execute(() =>
                {
                    Thread.Sleep(500);
                    ConsoleHelper.SendMessage(result);
                });
            }
            Thread.Sleep(10000);
            //Continue
            for (int i = 101; i < 130; i++)
            {
                var result = i.ToString();
                QueueTaskController.Current.Execute(() => ConsoleHelper.SendMessage(result));
            }
            //Break - контроллер очереди останавливается через 30 секунд не активности
            Thread.Sleep(35000);
            //New start
            for (int i = 131; i < 250; i++)
            {
                var result = i.ToString();
                QueueTaskController.Current.Execute(() => ConsoleHelper.SendMessage(result));
            }
            sw.Stop();
            ConsoleHelper.SendMessage($"Finish {sw.ElapsedMilliseconds}");
        }
      
        #endregion
    }
}
