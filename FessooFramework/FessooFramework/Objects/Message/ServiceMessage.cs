using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Json;
using System.Text;
using System.Threading.Tasks;

namespace FessooFramework.Objects.Message
{
    [DataContract]
    public class ServiceMessage 
    {
        [DataMember]
        public string JSON { get; set; }
        [DataMember]
        public byte[] Bytes { get; set; }
        [DataMember]
        public string AssemblyQualifiedName { get; set; }
        [DataMember]
        public string FullName { get; set; }
        #region Constructor
        public ServiceMessage(object obj)
        {
            Serialize(obj);
        }
        #endregion
        #region Methods
        private void Serialize(object obj)
        {
            AssemblyQualifiedName = obj.GetType().AssemblyQualifiedName.ToString();
            JSON =JsonConvert.SerializeObject(obj);
           
            //DataContractSerializer ser = new DataContractSerializer(obj.GetType());
            //using (var ms = new MemoryStream())
            //{
            //    ser.WriteObject(ms, obj);
            //    var f = ms.ToArray();
            //    //var s = Lz4.CompressBytes(f, 0, f.Length, Lz4Net.Lz4Mode.Fast);
            //    //return s.ToArray();
            //    Bytes = f;
            //}
        }
        public T Desirialize<T>()
        {
            //DataContractSerializer ser = new DataContractSerializer(Type.GetType(AssemblyQualifiedName));
            ////var unzip = Lz4Net.Lz4.DecompressBytes(buff);
            //using (var ms = new MemoryStream(Bytes))
            //    return (T)ser.ReadObject(ms);
            var obj = JsonConvert.DeserializeObject(JSON, Type.GetType(AssemblyQualifiedName));
            return (T)obj;
        }

        public object Desirialize()
        {
            var type = Type.GetType(AssemblyQualifiedName);
            //if (type == null)
            //    throw new NullReferenceException($"Ошибка при получении сериализации запроса тип '{AssemblyQualifiedName}' - не найден в этом проекте или проектах на которые есть ссылки. Проверьте ссылку наличии референса на модели");
            //var basedType = type.BaseType.FullName;
            //DataContractSerializer ser = new DataContractSerializer(type);
            ////var unzip = Lz4Net.Lz4.DecompressBytes(buff);
            //using (var ms = new MemoryStream(Bytes))
            //    return ser.ReadObject(ms);

            var obj = JsonConvert.DeserializeObject(JSON, type);
            return obj;
        }
        #endregion
        #region Static methods
        internal static ServiceMessage New(object obj)
        {
            return new ServiceMessage(obj);
        }
        #endregion
    }
}
