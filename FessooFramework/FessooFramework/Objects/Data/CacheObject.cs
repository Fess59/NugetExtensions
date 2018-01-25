using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FessooFramework.Objects.Data
{
    /// <summary>   A cache object.
    ///             Объект кеширования на клиенте и визуальных моделей </summary>
    ///
    /// <remarks>   Fess59, 24.01.2018. </remarks>

    public class CacheObject : DataObject
    {
        /// <summary>   Gets or sets the TTL.
        ///             Время жизни объекта в кеше. К примеру TimeSpan.FromSecond(100).Ticks, объект живёт на клиенте не более 100 секунд
        ///             Для удаление  </summary>
        ///
        /// <value> The TTL. </value>

        public long TTL { get; set; }
        public long Timestamp { get; set; }
        public long Version { get; set; }
        public CacheObject()
        {

        }
        public bool _TTLCheck()
        {
            var result = false;

            return result;
        }
    }
}
