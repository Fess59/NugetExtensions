using FessooFramework.Tools.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Example.Tests.CoreExample.Components.Logger.Elements
{
    /// <summary>   A console logger.
    ///             Реализация логгера для логирования в консоль </summary>
    ///
    /// <remarks>   AM Kozhevnikov, 30.01.2018. </remarks>

    public class ConsoleLogger : LoggerElement
    {
        /// <summary>   Default constructor. </summary>
        ///
        /// <remarks>   AM Kozhevnikov, 30.01.2018. </remarks>

        public ConsoleLogger() : base(LoggerElementType.Console)
        {
        }
        /// <summary>   Sends a message.
        ///             Отправка сообщения в консоль </summary>
        ///
        /// <remarks>   AM Kozhevnikov, 30.01.2018. </remarks>
        ///
        /// <param name="message">  The message. </param>
        ///
        /// <returns>   True if it succeeds, false if it fails. </returns>
        public override bool SendMessage(LoggerMessage message)
        {
            var result = false;
            try
            {
                var textMessage = $"[{message.MessageType.ToString()}] {message.Text}";
                ConsoleHelper.SendMessage(textMessage);
                result = true;
            }
            catch (Exception ex)
            {
                ConsoleHelper.SendException(MethodBase.GetCurrentMethod(), "Ошибка при отправке логов через консоль", ex);
            }
            return result;
        }
    }
}
