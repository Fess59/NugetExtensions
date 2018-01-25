using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace FessooFramework.Tools.Helpers
{
    /// <summary>   An enum helper.
    ///             Инструменты для работы с сущностями Enum </summary>
    ///
    /// <remarks>   AM Kozhevnikov, 25.01.2018. </remarks>

    public static class EnumHelper
    {
        /// <summary>   Gets a value.
        ///             Конвертация int в указанные тип Enum</summary>
        ///
        /// <remarks>   AM Kozhevnikov, 25.01.2018. </remarks>
        ///
        /// <typeparam name="T">    Generic type parameter. </typeparam>
        /// <param name="value">    The value. </param>
        ///
        /// <returns>   The value. </returns>

        public static T GetValue<T>(int value)
        {
            return GetValue<T>(value.ToString());
        }

        /// <summary>   Gets a value. Конвертация string в указанные тип Enum. </summary>
        ///
        /// <remarks>   AM Kozhevnikov, 25.01.2018. </remarks>
        ///
        /// <typeparam name="T">    Generic type parameter. </typeparam>
        /// <param name="value">    The value. </param>
        ///
        /// <returns>   The value. </returns>

        public static T GetValue<T>(string value)
        {
            var result = default(T);
            try
            {
                result = (T)System.Enum.Parse(typeof(T), value);
            }
            catch (Exception ex)
            {
                Console.Write(ex.ToString());
            }
            return result;
        }

        /// <summary>   An Enum extension method that gets a description.
        ///             Получение описание из указанное значение Enum </summary>
        ///
        /// <remarks>   AM Kozhevnikov, 25.01.2018. </remarks>
        ///
        /// <param name="value">    The value. </param>
        ///
        /// <returns>   The description. </returns>

        public static string GetDescription(this Enum value)
        {
            var fieldInfo = value.GetType().GetField(value.ToString());
            var attribute = fieldInfo.GetCustomAttributes(typeof(DescriptionAttribute), false).FirstOrDefault() as DescriptionAttribute;

            return attribute != null ? attribute.Description : value.ToString();
        }

        /// <summary>   Query if 'description' is value exist for enum.
        ///             Провекка наличия описания у Enum </summary>
        ///
        /// <remarks>   AM Kozhevnikov, 25.01.2018. </remarks>
        ///
        /// <typeparam name="T">    Generic type parameter. </typeparam>
        /// <param name="description">  The description. </param>
        ///
        /// <returns>   True if value exist for enum, false if not. </returns>

        public static bool IsValueExistForEnum<T>(string description)
        {
            try
            {
                MemberInfo[] fis = typeof(T).GetFields();

                foreach (var fi in fis)
                {
                    DescriptionAttribute[] attributes = (DescriptionAttribute[])fi.GetCustomAttributes(typeof(DescriptionAttribute), false);

                    if (attributes != null && attributes.Length > 0 && attributes[0].Description == description)
                    {
                        var value = (T)Enum.Parse(typeof(T), fi.Name);
                        if (value != null)
                            return true;
                    }
                }
            }
            catch (Exception)
            {
                throw;
            }
            return false;
        }
    }
}
