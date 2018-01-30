using FessooFramework.Objects.ALM;
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

    public abstract class SystemObject : ALMObject<SystemState>, IDisposable
    {
        #region Constructor

        /// <summary>   Default constructor. </summary>
        ///
        /// <remarks>   AM Kozhevnikov, 23.01.2018. </remarks>

        public SystemObject()
        {
            _SetState(SystemState.Created);
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
        #region ALM realization
        public override void _StateChanged(SystemState newState, SystemState oldState)
        {
            switch (newState)
            {
                case SystemState.Created:
                    _SetState(SystemState.Initialized);
                    break;
                case SystemState.Initialized:
                    Initialization();
                    _SetState(SystemState.Configured);
                    break;
                case SystemState.Configured:
                    _SetState(SystemState.Loaded);
                    break;
                case SystemState.Loaded:
                    _SetState(SystemState.Launched);
                    break;
                case SystemState.Launched:
                    break;
                case SystemState.Pause:
                    break;
                case SystemState.Complete:
                    break;
                default:
                    break;
            }
        }
        public override IEnumerable<ALMConf<SystemState>> _StateConfiguration()
        {
            return new ALMConf<SystemState>[] 
            {
                ALMConf<SystemState>.New(SystemState.None, new[] { SystemState.Created }),
                ALMConf<SystemState>.New(SystemState.Created, new[] { SystemState.Initialized }),
                ALMConf<SystemState>.New(SystemState.Initialized, new[] { SystemState.Configured }),
                ALMConf<SystemState>.New(SystemState.Configured, new[] { SystemState.Loaded }),
                ALMConf<SystemState>.New(SystemState.Loaded, new[] { SystemState.Launched, SystemState.Warnings }),
                ALMConf<SystemState>.New(SystemState.Warnings, new[] { SystemState.Launched }),
                ALMConf<SystemState>.New(SystemState.Launched, new[] { SystemState.Complete })
            };
        }
        #endregion
    }
}
