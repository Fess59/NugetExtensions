using FessooFramework.Components.LoggerComponent.Models;
using FessooFramework.Components.LoggerComponent.Parts;
using FessooFramework.Components.LoggerComponent.Realizations;
using FessooFramework.Components.LoggerComponent.Tests;
using FessooFramework.Core;
using FessooFramework.Objects;
using FessooFramework.Tools.Controllers;
using FessooFramework.Tools.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace FessooFramework.Components.LoggerComponent
{
    /// <summary>   A logger helper.
    ///             Блок отвечающий за всё логирование в приложении </summary>
    ///
    /// <remarks>   AM Kozhevnikov, 29.01.2018. </remarks>

    public class LoggerHelper : SystemComponentMulti<LoggerElement, LoggerElementType>
    {
        #region Property
        /// <summary>   The has logger enable.
        ///             Глобальная настройка включения логирования и перехвата сообщений </summary>
        public static ObjectController<bool> HasLoggerEnable = new ObjectController<bool>(false);
        /// <summary>   The has console.
        ///             Настройка перехвата логирования с использванием консоли </summary>
        public static ObjectController<bool> HasConsole = new ObjectController<bool>(false);
        /// <summary>   The has file. 
        ///             Сохранение логов в файл</summary>
        public static ObjectController<bool> HasFile = new ObjectController<bool>(false);
        /// <summary>   The has service. 
        ///             Отрпавка логов на сервер</summary>
        public static ObjectController<bool> HasService = new ObjectController<bool>(false);
        /// <summary>   The has action. </summary>
        public static ObjectController<bool> HasAction = new ObjectController<bool>(false);

        /// <summary>   Pathname of the file log folder. </summary>
        public static ObjectController<string> FileFolder = new ObjectController<string>("Logs");
        /// <summary>   Full pathname of the file. </summary>
        public static ObjectController<string> FilePath = new ObjectController<string>(SystemCore.Current.CoreConfiguration.RootDirectory);
        #endregion
        #region Method
        /// <summary>   Sends a message.
        ///             Основной метод логирования </summary>
        ///
        /// <remarks>   AM Kozhevnikov, 30.01.2018. </remarks>
        ///
        /// <param name="message">  The message. </param>
        ///
        /// <returns>   True if it succeeds, false if it fails. </returns>
        public static bool SendMessage(LoggerMessage message)
        {
            var result = false;
            if (HasLoggerEnable.Value)
                result = send(message);
            return result;
        }
        public static bool SendMessage(LoggerMessageType loggerType, string message, string category = "None")
        {
            return SendMessage(LoggerMessage.New(loggerType, message, category));
        }
        public static bool SendInformation(string text, string category = "None")
        {
            return SendMessage(LoggerMessage.New(LoggerMessageType.Information, text, category));
        }
        public static bool SendException(Exception ex, string category = "None")
        {
            return SendMessage(LoggerMessage.New(LoggerMessageType.Exception, ex.ToString(), category));
        }
        public static bool SendWarning(string text, string category = "None")
        {
            return SendMessage(LoggerMessage.New(LoggerMessageType.Warning, text, category));
        }
        #endregion
        #region Private

        /// <summary>   Send this message.
        ///             Логика логирования </summary>
        ///
        /// <remarks>   AM Kozhevnikov, 30.01.2018. </remarks>
        ///
        /// <param name="message">  The message. </param>
        ///
        /// <returns>   True if it succeeds, false if it fails. </returns>

        private static bool send(LoggerMessage message)
        {
            var result = false;
            try
            {
                foreach (var element in GetAll().Where(q => q.HasEnable))
                    element.SendMessage(message);
                result = true;
            }
            catch (Exception ex)
            {
                ConsoleHelper.SendException(MethodBase.GetCurrentMethod(), "Ошибка при попытке логирования", ex);
            }
            return result;
        }
        #endregion
        #region Component realization
        public override void _1_ElementAdd(ref List<LoggerElement> elements)
        {
            elements.Add(new ConsoleLogger());
            elements.Add(new FileLogger());
            elements.Add(new ServiceLogger());
            elements.Add(new ActionLogger());
        }

        public override void _2_ElementConfigurated(ref LoggerElement element)
        {
            switch (element.UIDEnum)
            {
                case LoggerElementType.Console:
                    element.HasEnable = HasConsole.Value;
                    HasConsole.Change += () => { GetElement(LoggerElementType.Console).HasEnable = HasConsole.Value; };
                    break;
                case LoggerElementType.File:
                    element.HasEnable = HasFile.Value;
                    HasFile.Change += () => { GetElement(LoggerElementType.File).HasEnable = HasFile.Value; };
                    break;
                case LoggerElementType.Service:
                    element.HasEnable = HasService.Value;
                    HasService.Change += () => { GetElement(LoggerElementType.Service).HasEnable = HasService.Value; };
                    break;
                case LoggerElementType.Action:
                    element.HasEnable = HasAction.Value;
                    HasAction.Change += () => { GetElement(LoggerElementType.Action).HasEnable = HasAction.Value; };
                    break;
                default:
                    break;
            }
        }
        public override void _3_Loaded()
        {
            HasLoggerEnable.Value = SystemCore.Current.CoreConfiguration.LogEnable.Value;
#if DEBUG
            HasConsole.Value = true;
#endif
        }
        public override IEnumerable<TestingCase> _4_Testing()
        {
            return new TestingCase[]
            {
                TestingCase.New(LoggerCases.Case1Description, LoggerCases.Case1),
                TestingCase.New(LoggerCases.Case2Description, LoggerCases.Case2)
            };
        }
        public override void _5_Run()
        {
        }
        public override void _99_Unload()
        {
        }
        #endregion
    }
}
