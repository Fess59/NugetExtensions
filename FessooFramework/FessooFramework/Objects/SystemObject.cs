using FessooFramework.Tools.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FessooFramework.Objects
{
    /// <summary>   A system object. 
    ///             Основной объект системы</summary>
    ///
    /// <remarks>   AM Kozhevnikov, 23.01.2018. </remarks>

    public abstract class SystemObject : BaseObject, IDisposable
    {
        #region Constructor

        /// <summary>   Default constructor. </summary>
        ///
        /// <remarks>   AM Kozhevnikov, 23.01.2018. </remarks>

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

        /// <summary>   Finaliser.
        ///             При очистке объекта отправляет его в ObjectHelper.Dispose</summary>
        ///
        /// <remarks>   AM Kozhevnikov, 23.01.2018. </remarks>

        ~SystemObject()
        {
            ObjectHelper.Dispose(this);
        }

        /// <summary>
        /// Обертка для IDisposable.Dispose в SystemObject
        /// Создана для возможности очистки объекта во всех наследуемых сущностях
        /// 
        /// Выполняет определяемые приложением задачи, связанные с удалением, высвобождением или сбросом
        /// неуправляемых ресурсов.
        /// </summary>
        ///
        /// <remarks>   AM Kozhevnikov, 23.01.2018. </remarks>

        public virtual void Dispose()
        {
        }

        /// <summary>
        /// Выполняет определяемые приложением задачи, связанные с удалением, высвобождением или сбросом
        /// неуправляемых ресурсов.
        /// </summary>
        ///
        /// <remarks>   AM Kozhevnikov, 23.01.2018. </remarks>

        void IDisposable.Dispose()
        {
            Dispose();
        }
        #endregion
    }
}
