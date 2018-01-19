using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FessooFramework.Tools
{
    /// <summary>   A console helper.
    ///             Для взаимодействия с консолью
    ///             Помогает централизовано управлять выводом в консоль, его оформлением и самой публикацией в при переключении debug и релиз
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

        public static void Send(string text)
        {
#if DEBUG
            Console.WriteLine($"[{DateTime.Now.ToString("o")}] {text}");
#endif
        }

        /// <summary>   Sends a warning. Отправляет предупреждение в консоль </summary>
        ///
        /// <remarks>   AM Kozhevnikov, 19.01.2018. </remarks>
        ///
        /// <param name="text"> The text. </param>

        public static void SendWarning(string text)
        {
            Send($"WARNING! {text}");
        }
    }
}
