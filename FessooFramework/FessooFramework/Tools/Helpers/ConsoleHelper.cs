using System;
using System.Reflection;

namespace FessooFramework.Tools.Helpers
{
    /// <summary>   A console helper.
    ///             Для взаимодействия с консолью
    ///             Помогает централизовано управлять выводом в консоль, его оформлением и публикацией при переключении debug и release
    ///             </summary>
    ///
    /// <remarks>   AM Kozhevnikov, 19.01.2018. </remarks>

    public static class ConsoleHelper
    {
        /// <summary>   Send this message.
        ///             Отправка данных в консоль с настроенным форматом
        ///             Работает при наличии символов условной компиляции DEBUG</summary>
        ///
        /// <remarks>   AM Kozhevnikov, 19.01.2018. </remarks>
        ///
        /// <param name="text"> The text. </param>

        public static void SendMessage(string text)
        {
#if DEBUG
            Console.WriteLine($"[{DateTime.Now.ToString("o")}] {text}");
#endif
        }
        /// <summary>   Sends an exception.
        ///             Отправка информации об ошибке в консоль </summary>
        ///
        /// <remarks>   AM Kozhevnikov, 22.01.2018. </remarks>
        ///
        /// <param name="ex">   The ex. </param>

        public static void SendException(MethodBase owner, string text, Exception ex)
        {
            SendException($"{owner.DeclaringType}.{owner.Name} - {text}: {Environment.NewLine + ex}");
        }
        /// <summary>   Sends an exception.
        ///             Отправка информации об ошибке в консоль </summary>
        ///
        /// <remarks>   AM Kozhevnikov, 22.01.2018. </remarks>
        ///
        /// <param name="ex">   The ex. </param>

        public static void SendWarning(MethodBase owner, string text)
        {
            SendWarning($"{owner.DeclaringType}.{owner.Name} - {text}");
        }

        

        /// <summary>   Sends an exception.
        ///             Отправка информации об ошибке в консоль </summary>
        ///
        /// <remarks>   AM Kozhevnikov, 22.01.2018. </remarks>
        ///
        /// <param name="ex">   The ex. </param>

        public static void SendException(Exception ex)
        {
            SendException(ex.ToString());
        }

        /// <summary>   Sends an exception.
        ///             Отправка информации об ошибке в консоль </summary>
        ///
        /// <remarks>   AM Kozhevnikov, 22.01.2018. </remarks>
        ///
        /// <param name="ex">   The ex. </param>

        public static void SendException(string ex)
        {
            SendMessage($"[EXCEPTION] {ex}");
        }

        /// <summary>   Sends a warning. Отправляет предупреждение в консоль </summary>
        ///
        /// <remarks>   AM Kozhevnikov, 19.01.2018. </remarks>
        ///
        /// <param name="text"> The text. </param>

        public static void SendWarning(string text)
        {
            SendMessage($"[WARNING] {text}");
        }

        public static void Send(string type, string text)
        {
            switch (type)
            {
                case "Warning":
                    SendWarning(text);
                    break;
                case "Exception":
                    SendException(text);
                    break;
                default:
                    SendMessage(text);
                    break;
            }
        }
    }
}
