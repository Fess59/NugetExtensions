using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FessooFramework.Objects.Data
{
    /// <summary>   Values that represent cache states.
    ///             Состояния объекта в кэше клиента</summary>
    ///
    /// <remarks>   AM Kozhevnikov, 25.01.2018. </remarks>

    public enum CacheState
    {
        /// <summary>   Объект был получен с сервера и его нет в кэше клиента. </summary>
        None = 0,
        /// <summary> Объект создан на клиенте - требуеться отправка на сервер  . </summary>
        Create = 1,
        /// <summary>   Объект был изменён на клиенте. </summary>
        Edited =2,
        /// <summary>   Объект уже сохранён в кеше клиента. </summary>
        Completed = 3
    }
}
