using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FessooFramework.Tools.Helpers
{
    /// <summary>   A file logger helper.
    ///             Логгирование данных в файл, определяется путь и остальные  </summary>
    ///
    /// <remarks>   Fess59, 26.01.2018. </remarks>
    public static class LoggerFileHelper
    {
        #region Property
        /// <summary>   The lock add. </summary>
        private static object LockAdd = new object();
        #endregion
        #region Methods
        /// <summary>    Adds a text. </summary>
        ///
        /// <remarks>    Fess59, 26.01.2018. </remarks>
        ///
        /// <param name="directory"> Pathname of the directory. </param>
        /// <param name="filename">  Filename of the file. Имя файла, без расширения - "ГлавноеОкно" </param>
        /// <param name="message">   The message. Текстовое сообщение </param>
        /// <param name="type">      The type. Тип сообщения - "Ошибка", "Информация", "Предупреждение"  </param>

        public static void AddText(string directory, string filename, string message, string type)
        {
            try
            {
                if (!Directory.Exists(directory)) Directory.CreateDirectory(directory);
                var fileName = $@"{directory}\{filename}.txt";
                var text = $"[{DateTime.Now.ToString("o")}][{type}] {message}";
                lock (LockAdd)
                {
                    File.AppendAllText(fileName, text.ToString() + Environment.NewLine);
                }
            }
            catch (Exception ex)
            {
                ConsoleHelper.SendException(ex);
            }
        }

        /// <summary>   Logs an information.
        ///             Логирование сообщений </summary>
        ///
        /// <remarks>   Fess59, 26.01.2018. </remarks>
        ///
        /// <param name="directory">    Pathname of the directory. </param>
        /// <param name="filename">     Filename of the file. Имя файла, без расширения - "ГлавноеОкно". </param>
        /// <param name="message">      The message. Текстовое сообщение. </param>

        public static void LogInformation(string directory, string filename, string message)
        {
            AddText(directory, filename, message, "Information");
        }

        /// <summary>   Logs an exception.
        ///             Логирование ошибок </summary>
        ///
        /// <remarks>   Fess59, 26.01.2018. </remarks>
        ///
        /// <param name="directory">    Pathname of the directory. </param>
        /// <param name="filename">     Filename of the file. Имя файла, без расширения - "ГлавноеОкно". </param>
        /// <param name="message">      The message. Текстовое сообщение. </param>

        public static void LogException(string directory, string filename, string message)
        {
            AddText(directory, filename, message, "EXCEPTION");
        }

        /// <summary>   Logs an exception. Логирование ошибок. </summary>
        ///
        /// <remarks>   Fess59, 26.01.2018. </remarks>
        ///
        /// <param name="directory">    Pathname of the directory. </param>
        /// <param name="filename">     Filename of the file. Имя файла, без расширения - "ГлавноеОкно". </param>
        /// <param name="message">      The message. Текстовое сообщение. </param>

        public static void LogException(string directory, string filename, Exception message)
        {
            AddText(directory, filename, message.ToString(), "EXCEPTION");
        }

        /// <summary>   Logs a warning.
        ///             Логирование предупреждений </summary>
        ///
        /// <remarks>   Fess59, 26.01.2018. </remarks>
        ///
        /// <param name="directory">    Pathname of the directory. </param>
        /// <param name="filename">     Filename of the file. Имя файла, без расширения - "ГлавноеОкно". </param>
        /// <param name="message">      The message. Текстовое сообщение. </param>

        public static void LogWarning(string directory, string filename, string message)
        {
            AddText(directory, filename, message, "Warning");
        }
        #endregion
    }
}
