﻿using FessooFramework.Components.LoggerComponent.Models;
using FessooFramework.Tools.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace FessooFramework.Components.LoggerComponent.Tests
{
    internal static class LoggerCases
    {
        private static string Category => MethodBase.GetCurrentMethod().DeclaringType.ToString();

        #region Case1
        internal static string Case1Description = "Case1 - Отключение логгера. При отправке сообщения должно вернуться false тк обработка не была выполнена";
        internal static bool Case1()
        {
            var result = true;
            if (!LoggerHelper.HasLoggerEnable.Value)
            {
                result = LoggerHelper.SendMessage(LoggerMessage.New(LoggerMessageType.Testing, "Logger.Case1 - Test", Category));
            }
            return result;
        }
        #endregion
        #region Case2
        internal static string Case2Description = "Case2 - проверка элементов логгера. Все элементы должны вернуть True";
        internal static bool Case2()
        {
            var result = true;
            foreach (var item in LoggerHelper.GetAll())
            {
                if (!item.SendInformation("Logger.Case2 - Test", Category))
                {
                    result = false;
                    ConsoleHelper.SendMessage($"[{LoggerMessageType.Testing.ToString()}] LoggerHelper.{ item.UID} - Реализация должна вернуть True");
                }
            }
            return result;
        }
        #endregion
    }
}
