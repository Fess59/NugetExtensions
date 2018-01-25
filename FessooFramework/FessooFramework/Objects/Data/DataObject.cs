using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FessooFramework.Objects.Data
{
    /// <summary>   A data object.
    ///             Базовый объект данных </summary>
    ///
    /// <remarks>   AM Kozhevnikov, 24.01.2018. </remarks>

    public class DataObject : BaseObject
    {
        /// <summary>   Gets or sets a value indicating whether this object has removed.
        ///             Признак что объект удалён - базовая логики контейнера данных учитывает данны признак при работе с объектами</summary>
        ///
        /// <value> True if this object has removed, false if not. </value>

        public bool HasRemoved { get; set; }

        /// <summary>   Removes this object.
        ///             Помечает объект удаленным</summary>
        ///
        /// <remarks>   AM Kozhevnikov, 24.01.2018. </remarks>

        public virtual void _Remove()
        {
            HasRemoved = true;
        }
    }
}
