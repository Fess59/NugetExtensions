using FessooFramework.Objects.Data;
using FessooFramework.Tools.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Example.Tests.CoreExample.Components.Logger
{
    /// <summary>   A logger message.
    ///             Базовое сообщение логирования </summary>
    ///
    /// <remarks>   AM Kozhevnikov, 30.01.2018. </remarks>

    public class LoggerMessage : DataObject
    {
        /// <summary>   Gets or sets the type of the message. </summary>
        ///
        /// <value> The type of the message. </value>

        public int messageType { get; set; }

        /// <summary>   Gets or sets the type of the message. </summary>
        ///
        /// <value> The type of the message. </value>

        public LoggerMessageType MessageType { get { return EnumHelper.GetValue<LoggerMessageType>(messageType); } set { messageType = (int)value; } }

        /// <summary>   Gets or sets the text. </summary>
        ///
        /// <value> The text. </value>

        public string Text { get; set; }

        /// <summary>   New message from logger. </summary>
        ///
        /// <remarks>   AM Kozhevnikov, 30.01.2018. </remarks>
        ///
        /// <param name="messageType">  The type of the message. </param>
        /// <param name="text">         The text. </param>
        ///
        /// <returns>   A LoggerMessage. </returns>

        public static LoggerMessage New(LoggerMessageType messageType, string text)
        {
            return new LoggerMessage()
            {
                MessageType = messageType,
                Text = text
            };
        }
    }
}
