using FessooFramework.Components.LoggerComponent.Models;
using FessooFramework.Tools.IOC;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FessooFramework.Components.LoggerComponent.Parts
{
    /// <summary>   A logger element.
    ///             Базовая реализация логгера</summary>
    ///
    /// <remarks>   AM Kozhevnikov, 30.01.2018. </remarks>

    public abstract class LoggerElement : _IOCElementEnum<LoggerElementType>
    {
        /// <summary>   Constructor. </summary>
        ///
        /// <remarks>   AM Kozhevnikov, 01.02.2018. </remarks>
        ///
        /// <param name="type"> The type. </param>

        public LoggerElement(LoggerElementType type) : base(type)
        {
        }
        /// <summary>   Sends a log message. </summary>
        ///
        /// <remarks>   AM Kozhevnikov, 01.02.2018. </remarks>
        ///
        /// <param name="message">  The message. </param>
        ///
        /// <returns>   True if it succeeds, false if it fails. </returns>

        public abstract bool SendMessage(LoggerMessage message);

        /// <summary>   Sends custom a log message. </summary>
        ///
        /// <remarks>   AM Kozhevnikov, 01.02.2018. </remarks>
        ///
        /// <param name="loggerType">   Type of the logger. </param>
        /// <param name="message">      The message. </param>
        /// <param name="category">     The category. </param>
        ///
        /// <returns>   True if it succeeds, false if it fails. </returns>

        public bool SendMessage(LoggerMessageType loggerType, string message, string category)
        {
            return SendMessage(LoggerMessage.New(loggerType, message, category));
        }

        /// <summary>   Sends an log information. </summary>
        ///
        /// <remarks>   AM Kozhevnikov, 01.02.2018. </remarks>
        ///
        /// <param name="text">     The text. </param>
        /// <param name="category"> The category. </param>
        ///
        /// <returns>   True if it succeeds, false if it fails. </returns>

        public bool SendInformation(string text, string category)
        {
            return SendMessage(LoggerMessage.New(LoggerMessageType.Information, text, category));
        }

        /// <summary>   Sends an log exception. </summary>
        ///
        /// <remarks>   AM Kozhevnikov, 01.02.2018. </remarks>
        ///
        /// <param name="ex">       The ex. </param>
        /// <param name="category"> The category. </param>
        ///
        /// <returns>   True if it succeeds, false if it fails. </returns>

        public bool SendException(Exception ex, string category)
        {
            return SendMessage(LoggerMessage.New(LoggerMessageType.Exception, ex.ToString(), category));
        }

        /// <summary>   Sends  a log warning. </summary>
        ///
        /// <remarks>   AM Kozhevnikov, 01.02.2018. </remarks>
        ///
        /// <param name="text">     The text. </param>
        /// <param name="category"> The category. </param>
        ///
        /// <returns>   True if it succeeds, false if it fails. </returns>

        public bool SendWarning(string text, string category)
        {
            return SendMessage(LoggerMessage.New(LoggerMessageType.Warning, text, category));
        }
    }
}
