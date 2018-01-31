using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace FessooFramework.Tools.Helpers
{
    /// <summary>   A cryptography helper.
    ///             Набор инструментов для работы с криптографией </summary>
    ///
    /// <remarks>   AM Kozhevnikov, 31.01.2018. </remarks>

    public static class CryptographyHelper
    {
        /// <summary>   String to sha 256 byte array.
        ///             Конвертирую строку в байт массив шифрованный с помощью SHA256 </summary>
        ///
        /// <remarks>   AM Kozhevnikov, 31.01.2018. </remarks>
        ///
        /// <param name="text"> . </param>
        ///
        /// <returns>   A byte[]. </returns>

        public static byte[] StringToSha256ByteArray(string text)
        {
            byte[] bytes = Encoding.Unicode.GetBytes(text);
            SHA256Managed hashstring = new SHA256Managed();
            byte[] hash = hashstring.ComputeHash(bytes);
            return hash;
        }

        /// <summary>   Конвертирую строку в Hash строку шифрованную с помощью SHA256. </summary>
        ///
        /// <remarks>   AM Kozhevnikov, 31.01.2018. </remarks>
        ///
        /// <param name="login">    The login. </param>
        /// <param name="pass">     The pass. </param>
        ///
        /// <returns>   A string. </returns>

        public static string StringToSha256String(string login, string pass)
        {
            return StringToSha256String(login.Trim().ToLower() + pass);
        }

        /// <summary>   Конвертирую строку в Hash строку шифрованную с помощью SHA256. </summary>
        ///
        /// <remarks>   AM Kozhevnikov, 31.01.2018. </remarks>
        ///
        /// <param name="text"> . </param>
        ///
        /// <returns>   A string. </returns>

        public static string StringToSha256String(string text)
        {
            var hash = StringToSha256ByteArray(text);
            string hashString = string.Empty;
            foreach (byte x in hash)
            {
                hashString += String.Format("{0:x2}", x);
            }
            return hashString;
        }

        /// <summary>   Byte to string.
        ///             Конвертирую строку в массив в байт в строку  </summary>
        ///
        /// <remarks>   AM Kozhevnikov, 31.01.2018. </remarks>
        ///
        /// <param name="bytes">    The bytes. </param>
        ///
        /// <returns>   A string. </returns>

        public static string ByteToString(byte[] bytes)
        {
            string hashString = string.Empty;
            foreach (byte x in bytes)
            {
                hashString += String.Format("{0:x2}", x);
            }
            return hashString;
        }
    }
}
