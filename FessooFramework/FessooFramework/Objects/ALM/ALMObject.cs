using FessooFramework.Objects;
using FessooFramework.Tools.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FessooFramework.Objects.ALM
{
    /// <summary>   An alm object.
    ///             Объект с управляемым жизненным циклом </summary>
    ///
    /// <remarks>   AM Kozhevnikov, 29.01.2018. </remarks>
    ///
    /// <typeparam name="TEnumState">   Type of the enum state. </typeparam>

    public abstract class ALMObject<TEnumState> : BaseObject 
        where TEnumState :  struct, IConvertible
    {

        /// <summary>   The state.
        ///             Поле которое сохраняется в базу </summary>
        public int state { get; private set; }

        /// <summary>   Gets or sets the state.
        ///             Текущее состояние объекта </summary>
        ///
        /// <value> The state. </value>

        public TEnumState State { get; private set; }

        /// <summary>   Sets a state.
        ///             Изменяем состояние объекта </summary>
        ///
        /// <remarks>   AM Kozhevnikov, 29.01.2018. </remarks>
        ///
        /// <exception cref="Exception">    Thrown when an exception error condition occurs. </exception>
        ///
        /// <param name="newState"> State of the new. </param>

        public void _SetState(TEnumState newState)
        {
            if (!StateCheck(State, newState) && State.ToString() != newState.ToString())
                throw new Exception($"Переход из состояния {State.ToString()} в состояние {newState.ToString()} не поддерживается");
            //ConsoleHelper.SendMessage($"{this.GetType().Name} => Переход из состояния { State.ToString()} в состояние { newState.ToString()}");
            var OldState = State;
            State = newState;
            _StateChanged(State, OldState);
        }
        /// <summary>   State changed.
        ///             Состояние объекта было изменено </summary>
        ///
        /// <remarks>   AM Kozhevnikov, 29.01.2018. </remarks>
        public virtual void _StateChanged(TEnumState newState, TEnumState oldState)
        {

        }
        /// <summary>   Enumerates state configuration in this collection.
        ///             Получаем список настроек переходов объекта в другие состояния</summary>
        ///
        /// <remarks>   AM Kozhevnikov, 29.01.2018. </remarks>
        ///
        /// <returns>
        /// An enumerator that allows foreach to be used to process state configuration in this
        /// collection.
        /// </returns>

        IEnumerable<ALMConf<TEnumState>> StateConfiguration()
        {
            var list = _StateConfiguration();
            if (!list.Any())
                throw new Exception($"Для типа {typeof(TEnumState).ToString()} необходимо настроить переходы жизненного цикла. Реализуйте метод _StateConfiguration");
            var countCheck = list.GroupBy(q => q.State);
            foreach (var item in countCheck.Where(q=>q.Count() > 1))
            {
                throw new Exception($"Для типа {typeof(TEnumState).ToString()} состояние {item.Key.ToString()}, настроено более одного раза");
            }
            return list.ToArray();
        }

        /// <summary>   State configuration.
        ///             Настройка конфигурации для измения состояни жизненного цикла </summary>
        ///
        /// <remarks>   AM Kozhevnikov, 29.01.2018. </remarks>
        ///
        /// <param name="list"> [in,out] The list. </param>

        public virtual IEnumerable<ALMConf<TEnumState>> _StateConfiguration()
        {
            return Enumerable.Empty<ALMConf<TEnumState>>();
        }

        /// <summary>   State check.
        ///             Проверка перехода из одного состояния в новое </summary>
        ///
        /// <remarks>   AM Kozhevnikov, 29.01.2018. </remarks>
        ///
        /// <exception cref="Exception">    Thrown when an exception error condition occurs. </exception>
        ///
        /// <param name="oldState"> State of the old. </param>
        /// <param name="newState"> State of the new. </param>
        ///
        /// <returns>   True if it succeeds, false if it fails. </returns>

        bool StateCheck(TEnumState oldState, TEnumState newState)
        {
            var result = false;
            var configurations = StateConfiguration();
            if (configurations.Any())
            {
                var stateConfiguration = configurations.SingleOrDefault(q => q.State.ToString() == oldState.ToString());
                if (stateConfiguration != null)
                {
                    result = stateConfiguration.NextState.Any(q => q.ToString() == newState.ToString());
                }
                else
                {
                    throw new Exception("Не реализована настройка состояний");
                }
            }
            else
            {
                throw new Exception("Не реализована настройка состояний");
            }
            return result;
        }
    }
}
