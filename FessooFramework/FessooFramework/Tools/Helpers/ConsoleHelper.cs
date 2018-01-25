using System;

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

        internal static void SendException(Exception ex)
        {
            SendMessage($"EXCEPTION {ex.ToString()}");
        }

        /// <summary>   Sends a warning. Отправляет предупреждение в консоль </summary>
        ///
        /// <remarks>   AM Kozhevnikov, 19.01.2018. </remarks>
        ///
        /// <param name="text"> The text. </param>

        public static void SendWarning(string text)
        {
            SendMessage($"WARNING {text}");
        }
    }
}
