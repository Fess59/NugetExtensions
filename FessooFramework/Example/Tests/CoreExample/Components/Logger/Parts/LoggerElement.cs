using FessooFramework.Tools.IOC;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Example.Tests.CoreExample.Components.Logger
{
    public abstract class LoggerElement : _IOCElementEnum<LoggerElementType>
    {
        public LoggerElement(LoggerElementType type) : base(type)
        {
        }
        public abstract bool SendMessage(LoggerMessage message);

        public bool SendMessage(LoggerMessageType loggerType, string message)
        {
            return SendMessage(LoggerMessage.New(loggerType, message));
        }
        public bool SendInformation(string text)
        {
            return SendMessage(LoggerMessage.New(LoggerMessageType.Information, text));
        }
        public bool SendException(Exception ex)
        {
            return SendMessage(LoggerMessage.New(LoggerMessageType.Exception, ex.ToString()));
        }
        public bool SendWarning(string text)
        {
            return SendMessage(LoggerMessage.New(LoggerMessageType.Warning, text));
        }
    }
}
