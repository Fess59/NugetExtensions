using FessooFramework.Objects.ALM;
using FessooFramework.Tools.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
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
        /// <summary>   Loaded this object. </summary>
        ///
        /// <remarks>   AM Kozhevnikov, 31.01.2018. </remarks>
        protected virtual void _3_Loaded()
        {
        }
        /// <summary>   Testings this object. </summary>
        ///
        /// <remarks>   AM Kozhevnikov, 31.01.2018. </remarks>
        protected virtual IEnumerable<TestingCase> _4_Testing()
        {
            return Enumerable.Empty<TestingCase>();
        }
        /// <summary>   Unloaded this object. </summary>
        ///
        /// <remarks>   AM Kozhevnikov, 31.01.2018. </remarks>
        protected virtual void _6_Unload()
        {

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
        protected override void _StateChanged(SystemState newState, SystemState oldState)
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
                    _3_Loaded();
#if DEBUG
                    _SetState(SystemState.Testing);
#else
                    _SetState(SystemState.Launched);
#endif
                    break;
                case SystemState.Testing:
                    Testing();
                    _SetState(SystemState.Launched);
                    break;
                case SystemState.Launched:
                    break;
                case SystemState.Unload:
                    _6_Unload();
                    break;
                default:
                    break;
            }
        }
        protected override IEnumerable<ALMConf<SystemState>> _StateConfiguration()
        {
            return new ALMConf<SystemState>[]
            {
                ALMConf<SystemState>.New(SystemState.Created, new[] { SystemState.Initialized }),
                ALMConf<SystemState>.New(SystemState.Initialized, new[] { SystemState.Configured }),
                ALMConf<SystemState>.New(SystemState.Configured, new[] { SystemState.Loaded }),
                ALMConf<SystemState>.New(SystemState.Loaded, new[] { SystemState.Launched, SystemState.Testing }),
                ALMConf<SystemState>.New(SystemState.Testing, new[] { SystemState.Launched }),
                ALMConf<SystemState>.New(SystemState.Launched, new[] { SystemState.Unload })
            };
        }
        #endregion
        #region Abstractions
        /// <summary>
        ///     Testing this object. Проверка компонента на определённые условия - в будующем в этом
        ///     месте требуется подключение модуля динамической отладки с выводом предпреждений.
        /// </summary>
        ///
        /// <remarks>   AM Kozhevnikov, 30.01.2018. </remarks>
        protected void Testing()
        {
            var cases = _4_Testing();
            if (cases != null && cases.Any())
            {
                foreach (var c in cases)
                {
                    if (c.ComponentCase == null)
                    {
                        ConsoleHelper.SendWarning(MethodBase.GetCurrentMethod(), $"Case не возможно выполнить. Func не может быть NULL. Описание -  '{c.Description}' - тело вызова не может быть NULL");
                    }
                    else
                    {
                        try
                        {
                            if (!c.ComponentCase.Invoke())
                            {
                                ConsoleHelper.SendWarning(MethodBase.GetCurrentMethod(), $"Case не пройден! Описание - '{c.Description}'");
                            }
                        }
                        catch (Exception ex)
                        {
                            ConsoleHelper.SendWarning(MethodBase.GetCurrentMethod(), $"Case ошибка при выполнении! Описание - '{c.Description}'");
                        }

                    }
                }
            }
        }
        #endregion
    }

    public struct TestingCase
    {
        public Func<bool> ComponentCase { get; set; }
        public string Description { get; set; }
        TestingCase(string description, Func<bool> componentCase)
        {
            Description = description;
            ComponentCase = componentCase;
        }
        public static TestingCase New(string description, Func<bool> componentCase)
        {
            return new TestingCase()
            {
                Description = description,
                ComponentCase = componentCase
            };
        }
    }
}
