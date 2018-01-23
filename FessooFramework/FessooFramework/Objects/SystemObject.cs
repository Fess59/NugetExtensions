using FessooFramework.Tools.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FessooFramework.Objects
{
    public abstract class SystemObject : BaseObject, IDisposable
    {
        #region Constructor
        public SystemObject()
        {
            Initialization();
        }
        #endregion
        #region Methods
        /// <summary>   Defaults this object.
        ///             Приводит данный объект к исходному состоянию
        ///             </summary>
        ///
        /// <remarks>   AM Kozhevnikov, 22.01.2018. </remarks>
        public virtual void Default()
        {

        }
        /// <summary>   Initializations this object.
        ///             Нижний уровень инициализации объекта</summary>
        ///
        /// <remarks>   AM Kozhevnikov, 23.01.2018. </remarks>
        private void Initialization()
        {
            Default();
        }
        #endregion
        #region IDisposable realization
        ~SystemObject()
        {
            ObjectHelper.Dispose(this);
        }
        /// <summary>
        /// Обёртка IDisposable - вызывает все base.Dispose в наследуемых элементах
        /// </summary>
        protected virtual void Dispose()
        {
            ObjectHelper.Dispose(Change);
        }
        void IDisposable.Dispose()
        {
            Dispose();
        }
        #endregion
    }
}
