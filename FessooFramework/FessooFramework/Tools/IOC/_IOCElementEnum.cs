using FessooFramework.Objects;
using FessooFramework.Tools.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FessooFramework.Tools.IOC
{
    /// <summary>   An IOC element.
    ///             Элемент контейнера с привязкой UID к Enum</summary>
    ///
    /// <remarks>   AM Kozhevnikov, 11.01.2018. </remarks>

    public class _IOCElementEnum<TElementEnum> : _IOCElement
        where TElementEnum : struct, IConvertible
    {
       /// <summary>    Gets or sets the UID enum.
       ///              Реализация UID c использованием Enum </summary>
       ///
       /// <value>  The UID enum. </value>

       public TElementEnum UIDEnum { get { return EnumHelper.GetValue<TElementEnum>(UID); } set { UID = value.ToString(); } }

        /// <summary>   Constructor. </summary>
        ///
        /// <remarks>   AM Kozhevnikov, 30.01.2018. </remarks>
        ///
        /// <param name="type"> The type. </param>

        public _IOCElementEnum(TElementEnum type)
        {
            UIDEnum = type;
        }
    }
}
