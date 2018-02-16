using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace FessooFramework.Tools.Helpers
{
    public static class WebHelper
    {
        public static readonly HttpClient Client = client ?? Create();
        private static HttpClient client { get; set; }

        private static HttpClient Create()
        {
            HttpClient hc = new HttpClient();
            hc.MaxResponseContentBufferSize = 99999999;
            hc.Timeout = new TimeSpan(0, 0, 100);
            return hc;
        }
        /// <summary>
        /// Метод отправляет пост запрос с указанным объектом
        /// 1. Сериализует объект
        /// 2. Отправляет запрос
        /// 3. Если успешно возвращает True
        /// </summary>
        /// <param name="resuest">Объект который принимает метод серивиса в качестве параметра</param>
        /// <param name="address">Формат адреса - http://111.111.111.111/ServicePath/Service.svc/WebAPIMethod</param>
        /// <returns>True - успешная отправка, False - отправка не удалась, смотрим Output</returns>
        public static bool SendPOST(object resuest, string address)
        {
            var result = false;
            var id = Guid.NewGuid();
            try
            {
                var messageString = "";
                DataContractSerializer s = new DataContractSerializer(resuest.GetType());
                using (MemoryStream ms = new MemoryStream())
                {
                    XmlWriterSettings xmlWriterSettings = new System.Xml.XmlWriterSettings()
                    {
                        CloseOutput = false,
                        Encoding = Encoding.UTF8,
                        OmitXmlDeclaration = false,
                        Indent = true
                    };
                    using (System.Xml.XmlWriter xw = System.Xml.XmlWriter.Create(ms, xmlWriterSettings))
                        s.WriteObject(xw, resuest);
                    messageString = Encoding.UTF8.GetString(ms.ToArray());
                }

                HttpContent content = new StringContent(messageString);
                content.Headers.ContentType = new System.Net.Http.Headers.MediaTypeHeaderValue("application/xml");
                var url = address;
                using (var response = Client.PostAsync(url, content).Result)
                {

                }
                result = true;
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw e;

            }
            finally
            {
            }
            return result;
        }

        /// <summary>
        /// Метод отправляет пост запрос с указанным объектом
        /// 1. Сериализует объект
        /// 2. Отправляет запрос
        /// 3. Если успешно возвращает True
        /// </summary>
        /// <param name="resuest">Объект который принимает метод серивиса в качестве параметра</param>
        /// <param name="address">Формат адреса - http://111.111.111.111/ServicePath/Service.svc/WebAPIMethod</param>
        /// <returns>True - успешная отправка, False - отправка не удалась, смотрим Output</returns>
        public static TResponse SendPOST<TResponse>(object resuest, string address)
        {
            TResponse result = default(TResponse);
            try
            {
                var messageString = "";
                DataContractSerializer s = new DataContractSerializer(resuest.GetType());
                using (MemoryStream ms = new MemoryStream())
                {
                    XmlWriterSettings xmlWriterSettings = new System.Xml.XmlWriterSettings()
                    {
                        CloseOutput = false,
                        Encoding = Encoding.UTF8,
                        OmitXmlDeclaration = false,
                        Indent = true
                    };
                    using (System.Xml.XmlWriter xw = System.Xml.XmlWriter.Create(ms, xmlWriterSettings))
                        s.WriteObject(xw, resuest);
                    messageString = Encoding.UTF8.GetString(ms.ToArray());
                }

                using (HttpClient hc = new HttpClient())
                {
                    HttpContent content = new StringContent(messageString);
                    hc.MaxResponseContentBufferSize = 99999999;
                    content.Headers.ContentType = new System.Net.Http.Headers.MediaTypeHeaderValue("application/xml");
                    hc.Timeout = new TimeSpan(0, 0, 100);
                    var url = address;
                    using (var response = hc.PostAsync(url, content).Result)
                    {
                        if (response.IsSuccessStatusCode)
                        {
                            using (var responseContent = response.Content)
                            {
                                DataContractSerializer ser = new DataContractSerializer(typeof(TResponse));
                                using (var ms = responseContent.ReadAsStreamAsync().Result)
                                {
                                    var obj = ser.ReadObject(ms);
                                    result = (TResponse)obj;
                                }
                            }
                        }
                        else
                        {
                            using (var responseContent = response.Content)
                            {

                                var exception = "Ошибка при запросе на сервер, пожалуйста сообщите разработчикам:" + Environment.NewLine;
                                exception += response.ToString();
                                DCT.DCT.SendExceptions("WebHelper", exception);

                            }
                        }
                    }
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw e;
            }
            return result;
        }
        /// <summary>
        /// Метод отправляет пост запрос с указанным объектом
        /// 1. Сериализует объект
        /// 2. Отправляет запрос
        /// 3. Если успешно возвращает True
        /// </summary>
        /// <param name="resuest">Объект который принимает метод серивиса в качестве параметра</param>
        /// <param name="address">Формат адреса - http://111.111.111.111/ServicePath/Service.svc/WebAPIMethod</param>
        /// <returns>True - успешная отправка, False - отправка не удалась, смотрим Output</returns>
        public static TResponse SendPOST<TResponse>(string address)
        {
            TResponse result = default(TResponse);
            try
            {
                using (HttpClient hc = new HttpClient())
                {
                    hc.MaxResponseContentBufferSize = 99999999;
                    hc.Timeout = new TimeSpan(0, 0, 5);
                    var url = address;
                    HttpContent content = new StringContent("");
                    content.Headers.ContentType = new System.Net.Http.Headers.MediaTypeHeaderValue("application/xml");
                    using (var response = hc.PostAsync(url, content).Result)
                    {
                        if (response.IsSuccessStatusCode)
                        {
                            using (var responseContent = response.Content)
                            {
                                DataContractSerializer ser = new DataContractSerializer(typeof(TResponse));
                                using (var ms = responseContent.ReadAsStreamAsync().Result)
                                    result = (TResponse)ser.ReadObject(ms);
                            }
                        }
                    }
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }
            return result;
        }
    }
}
