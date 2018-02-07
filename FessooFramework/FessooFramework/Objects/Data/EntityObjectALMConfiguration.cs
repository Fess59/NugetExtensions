using FessooFramework.Tools.DCT;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FessooFramework.Objects.Data
{
    /// <summary>   A data alm configuration. 
    ///             Entity Object ALM Configuration</summary>
    ///
    /// <remarks>   AM Kozhevnikov, 06.02.2018. </remarks>
    ///
    /// <typeparam name="TObjectType">  Type of the object type. </typeparam>
    /// <typeparam name="TEnumState">   Type of the enum state. </typeparam>

    public class EntityObjectALMConfiguration<TObjectType, TEnumState>
       where TObjectType : EntityObject
       where TEnumState : struct, IConvertible
    {
        #region Property
        /// <summary>
        /// Текущее состояние
        /// </summary>
        public TEnumState State { get; }
        /// <summary>
        /// Новое состояние
        /// </summary>
        public TEnumState NextState { get; }
        /// <summary>
        /// Метод который обрабатывает переход состояния
        /// </summary>
        private Func<TObjectType, TObjectType, TObjectType> _Execute { get; }
        #endregion
        #region Constructor
        /// <summary>
        /// Базовый конструктор
        /// </summary>
        /// <param name="state"></param>
        /// <param name="nextState"></param>
        /// <param name="execute"></param>
        public EntityObjectALMConfiguration(TEnumState state, TEnumState nextState, Func<TObjectType, TObjectType, TObjectType> execute)
        {
            State = state;
            NextState = nextState;
            _Execute = execute;
        }
        #endregion
        #region Methods
        /// <summary>
        /// Выполняем переход в другое состояние
        /// </summary>
        /// <param name="oldObj"></param>
        /// <param name="newObj"></param>
        /// <returns></returns>
        public TObjectType Execute(TObjectType oldObj, TObjectType newObj)
        {
            var result = oldObj;
            DCT.Execute(q => result = _Execute(oldObj, newObj));
            return result;
        }
        /// <summary>
        /// Создание новый конфигурации ALM
        /// </summary>
        /// <param name="state"></param>
        /// <param name="nextState"></param>
        /// <param name="execute"></param>
        /// <returns></returns>
        public static EntityObjectALMConfiguration<TObjectType, TEnumState> New(TEnumState state, TEnumState nextState, Func<TObjectType, TObjectType, TObjectType> execute)
        {
            return new EntityObjectALMConfiguration<TObjectType, TEnumState>(state, nextState, execute);
        }
        #endregion
    }
}
