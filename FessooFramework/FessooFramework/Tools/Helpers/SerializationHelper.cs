using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using System.Xml.Serialization;

namespace FessooFramework.Tools.Helpers
{
    /// <summary>   A serialization helper.
    ///             Набор инструментов для сериализации данных </summary>
    ///
    /// <remarks>   AM Kozhevnikov, 31.01.2018. </remarks>

    public static class SerializationHelper
    {
        /// <summary>   A string extension method that converts an XML to a model.
        ///             Конвертирую строку(XML) в модель данных </summary>
        ///
        /// <remarks>   AM Kozhevnikov, 31.01.2018. </remarks>
        ///
        /// <typeparam name="T">    Generic type parameter. </typeparam>
        /// <param name="xml">  The XML to act on. </param>
        ///
        /// <returns>   XML as a T. </returns>

        public static T ToModel<T>(this string xml)
        {
            XDocument d = XDocument.Parse(xml);
            var encrypted = d.Root.Attribute("encrypted");
            T result;
            using (var xw = d.CreateReader())
                result = (T)new XmlSerializer(typeof(T)).Deserialize((xw));
            return result;
        }

        /// <summary>
        ///     A T extension method that convert this object into a string representation.
        ///     Конвертирую модель в строку
        /// </summary>
        ///
        /// <remarks>   AM Kozhevnikov, 31.01.2018. </remarks>
        ///
        /// <typeparam name="T">    Generic type parameter. </typeparam>
        /// <param name="model">    Модель. </param>
        /// <param name="rootName"> (Optional) Name of the root. </param>
        ///
        /// <returns>   The given data converted to a string. </returns>

        public static string ToString<T>(this T model, string rootName = "")
        {
            XmlSerializer serializer;
            if (string.IsNullOrWhiteSpace(rootName))
                serializer = new XmlSerializer(typeof(T));
            else
                serializer = new XmlSerializer(typeof(T), new XmlRootAttribute(rootName));
            using (StringWriter writer = new StringWriter())
            {
                serializer.Serialize(writer, model);
                return writer.ToString();
            }
        }

        /// <summary>   Converts this object to a file.
        ///             Сериализует класс в файл. Файл сохраняется по указанному пути. Если файл или путь не существуют - они будут созданы. В случае существования файла он будет перезаписан. </summary>
        ///
        /// <remarks>   AM Kozhevnikov, 31.01.2018. </remarks>
        ///
        /// <param name="model">    Модель. </param>
        /// <param name="path">     Путь в формате  - "C:\". </param>
        /// <param name="name">     Имя файла в формате - "test". </param>

        public static void ToFile(object model, string path, string name)
        {
            //Получаем полный путь до файла
            var fullName = path + @"\" + name + ".xml";
            //Проверяем путь до файла, при необходимости создаем
            if (!Directory.Exists(path))
                Directory.CreateDirectory(path);
            //Создаем или перезаписываем файл
            using (StreamWriter writer = new StreamWriter(fullName, false))
            {
                XmlSerializerNamespaces ns = new XmlSerializerNamespaces();
                XmlSerializer serializer = new XmlSerializer(model.GetType());
                serializer.Serialize(writer, model, ns);
            }
        }

        /// <summary>   Converts this object to a model from file.
        ///             Десериализует файл по указанному пути в класс</summary>
        ///
        /// <remarks>   AM Kozhevnikov, 31.01.2018. </remarks>
        ///
        /// <throwses cref="DirectoryNotFoundException">
        ///     Thrown when the requested directory is not present.
        /// </throwses>
        ///
        /// <typeparam name="T">    Generic type parameter. </typeparam>
        /// <param name="model">    Модель. </param>
        /// <param name="path">     Путь в формате  - "C:\". </param>
        /// <param name="name">     Имя файла в формате - "test". </param>
        ///
        /// <returns>   The given data converted to a T. </returns>

        public static T ToModelFromFile<T>(object model, string path, string name)
        {
            //Получаем полный путь до файла
            var fullName = path + @"\" + name + ".xml";
            //Проверяем наличие пути
            if (!Directory.Exists(path))
                throw new DirectoryNotFoundException("Путь не найден!");
            //Проверяем наличие файла
            if (!File.Exists(fullName))
                throw new DirectoryNotFoundException("Файл не найден!");
            //Десериализуем файл
            using (StreamReader reader = new StreamReader(fullName))
            {
                XmlSerializerNamespaces ns = new XmlSerializerNamespaces();
                XmlSerializer serializer = new XmlSerializer(model.GetType());
                return (T)serializer.Deserialize(reader);
            }
        }
    }
}
