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
        public LoggerElement(LoggerElementType type) : base(type)
        {
        }
        public abstract bool SendMessage(LoggerMessage message);

        public bool SendMessage(LoggerMessageType loggerType, string message, string category)
        {
            return SendMessage(LoggerMessage.New(loggerType, message, category));
        }
        public bool SendInformation(string text, string category)
        {
            return SendMessage(LoggerMessage.New(LoggerMessageType.Information, text, category));
        }
        public bool SendException(Exception ex, string category)
        {
            return SendMessage(LoggerMessage.New(LoggerMessageType.Exception, ex.ToString(), category));
        }
        public bool SendWarning(string text, string category)
        {
            return SendMessage(LoggerMessage.New(LoggerMessageType.Warning, text, category));
        }
    }
}
