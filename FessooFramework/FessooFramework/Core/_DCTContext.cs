using FessooFramework.Objects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FessooFramework.Core
{
    /// <summary>   A dct context.
    ///             Контекст данных - наследуем кастомный контекст </summary>
    ///
    /// <remarks>   AM Kozhevnikov, 25.01.2018. </remarks>

    public class _DCTContext : SystemObject
    {
        #region TrackModule

        /// <summary>   Gets or sets the identifier of the track.
        ///              </summary>
        ///TODO заменить на трэк модуль
        /// <value> The identifier of the track. </value>

        public Guid TrackId { get; set; }

        #endregion
        #region Constructor
        public _DCTContext()
        {
            //TODO 
            TrackId = Guid.NewGuid();
        }
        #endregion
    }
}
