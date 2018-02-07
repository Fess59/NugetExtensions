using FessooFramework.Components.LoggerComponent.Models;
using FessooFramework.Components.LoggerComponent.Parts;
using FessooFramework.Tools.Helpers;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace FessooFramework.Components.LoggerComponent.Realizations
{
    /// <summary>   A file logger.
    ///             Логирование в файл </summary>
    ///
    /// <remarks>   AM Kozhevnikov, 30.01.2018. </remarks>
    public class FileLogger : LoggerElement
    {
        #region Property
        /// <summary>   The object save file. </summary>
        private object ObjSaveFile = new object();
        #endregion
        #region Constructor
        /// <summary>   Default constructor. 
        ///             Задаём значение Enum для базового класса</summary>
        ///
        /// <remarks>   AM Kozhevnikov, 30.01.2018. </remarks>
        public FileLogger() : base(LoggerElementType.File)
        {
        }
        #endregion
        #region Method

        /// <summary>   Sends a message. </summary>
        ///
        /// <remarks>   AM Kozhevnikov, 30.01.2018. </remarks>
        ///
        /// <param name="message">  The message. </param>
        ///
        /// <returns>   True if it succeeds, false if it fails. </returns>

        public override bool SendMessage(LoggerMessage message)
        {
            //TODO добавить контроллер с агрегацией и последущим вызовом - пока текущий метод обрабатывает полученные значение, в фоне копиться новый пакет. Если данные кончились обработка останавливается
            var result = false;
            try
            {
                //Путь 
                var path = LoggerHelper.FilePath.Value;
                var folder = LoggerHelper.FileFolder.Value;
                if (string.IsNullOrWhiteSpace(path))
                    throw new Exception("Путь до файла не может быть пустым");
                var absolutePath = $@"{path}\{folder}";
                if (!Directory.Exists(absolutePath)) Directory.CreateDirectory(absolutePath);
                //Файл
                var fileName = $"[{DateTime.Now.ToString("MM-dd-yy")}][{message.MessageType.ToString()}] {message.Category}";
                var absoluteFilepath = $@"{absolutePath}\{fileName}.txt";
                //Запись
                var text = $@"[{DateTime.Now.ToString("HH:mm:ss.ffff")}] {message.Text}";
                lock (ObjSaveFile)
                {
                    File.AppendAllText(absoluteFilepath, text.ToString() + Environment.NewLine);
                }
                result = true;
            }
            catch (Exception ex)
            {
                ConsoleHelper.SendException(MethodBase.GetCurrentMethod(), "Ошибка при записи логов в файлы", ex);
            }
            return result;
        }
        #endregion

    }
}
