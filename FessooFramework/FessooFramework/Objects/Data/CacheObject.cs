using FessooFramework.Tools.Helpers;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace FessooFramework.Objects.Data
{
    /// <summary>   A cache object.
    ///             Объект кеширования на клиенте и визуальных моделей
    ///             Базовый модель тип модели и схема его данных определяется на основе Version и DataType </summary>
    ///
    /// <remarks>   Fess59, 24.01.2018. </remarks>
    public class CacheObject : DataObject
    {
        #region Property
        /// <summary>   Gets or sets the TTL.
        ///             Время жизни объекта
        ///             К примеру TTL = TimeSpan.FromSecond(100).Ticks, время жизни 100 секунд
        ///             Логика контроля время жизни реализована в  методе _TTLCheck</summary>
        ///
        /// <value> The TTL. </value>
        /// 
        [JsonProperty("TTL")]
        public long TTL { get; set; }

        /// <summary>   Gets or sets the timestamp.
        ///             Таймштамп объекта выдаётся сервером в момент создания или фиксации изменения объекта в базе
        ///             По этому параметру формируется список загрузки объектов для клиента</summary>
        ///
        /// <value> The timestamp. </value>
        /// 
        [JsonProperty("Timestamp")]
        public long Timestamp { get; set; }

        /// <summary>   Gets or sets the version.
        ///             Версия системы в которой данная модель была реализована
        ///             Используется при формировании списка загрузки объектов клиента - если версия клиента ниже, часть списка он не загрузит</summary>
        ///
        /// <value> The version. </value>
        /// 

        [JsonProperty("Version")]
        public string Version { get; set; }

        /// <summary>   Gets or sets the state.
        ///             Состояние жизненного цикла объекта кэша в формате int</summary>
        ///
        /// <value> The state. </value>
        /// 
        [JsonProperty("State")]
        public int State { get; set; }

        /// <summary>   Gets or sets the state enum.
        ///             Состояние жизненного цикла объекта кэша в формате CacheState</summary>
        ///
        /// <value> The state enum. </value>

        public CacheState StateEnum
        {
            get { return EnumHelper.GetValue<CacheState>(State); }
            set { State = (int)value; }
        }

        /// <summary>   Gets or sets the type of the data.
        ///             Тип объекта кэша в формате int </summary>
        ///
        /// <value> The type of the data. </value>
        [JsonProperty("DataType")]
        public string DataType { get; set; }

        /// <summary>   Data type enum.
        ///             Тип объекта кэша в формате CacheState </summary>
        ///
        /// <remarks>   AM Kozhevnikov, 25.01.2018. </remarks>
        ///
        /// <typeparam name="T">    Generic type parameter. </typeparam>
        ///
        /// <returns>   A T. </returns>

        public T DataTypeEnum<T>()
        {
            return EnumHelper.GetValue<T>(DataType);
        }
        #endregion
        #region Constructor
        public CacheObject()
        {
            //TTL = SetTTL().Ticks;
        }
        #endregion
        #region Methods
        /// <summary>   Determines if we can TTL check.
        ///             Метод проверяет актуалность объекта по жизненному циклу и при необходимости помечает его на удаление
        ///             Если текущая дата больше даты создания + TTL объект будет удален</summary>
        ///
        /// <remarks>   AM Kozhevnikov, 25.01.2018. </remarks>
        ///
        /// <returns>   True if it succeeds, false if it fails. </returns>
        public void _TTLCheck()
        {
            if (DateTime.Now.Ticks > CreateDate.Ticks + TTL)
                _Remove();
        }
        internal void SetProperty(Guid id, DateTime createDate, bool hasRemoved, string dataType, Version version)
        {
            Id = id;
            CreateDate= createDate;
            HasRemoved = hasRemoved;
            DataType = dataType;
            Version = version.ToString();
        }
        #endregion
    }
}
