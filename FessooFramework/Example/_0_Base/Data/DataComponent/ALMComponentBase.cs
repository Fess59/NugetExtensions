using FessooFramework.Objects.ALM;
using FessooFramework.Objects.Data;
using FessooFramework.Tools.DCT;
using FessooFramework.Tools.IOC;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Example._0_Base.Data.DataComponent
{
    public abstract class ALMComponentBase : _IOCElement
    {
        /// <summary>   Gets the type of the object. </summary>
        ///
        /// <value> The type of the object. </value>

        public abstract Type ObjectType { get; }
        public abstract bool Update(EntityObject oldObj, EntityObject newObj);
    }

    public abstract class ALMComponent<TObjectType, TEnumState> : ALMComponentBase
        where TObjectType : EntityObject, new()
        where TEnumState : struct, IConvertible
    {
        #region Property
        public override Type ObjectType => typeof(TObjectType);
        #endregion
        #region Constructor
        public ALMComponent()
        {
            UID = ObjectType.ToString();
        }
        #endregion
        #region Methods
        public override bool Update(EntityObject oldObj, EntityObject newObj)
        {
            var result = false;
            DCTExample.Execute(c =>
            {
                var OldObj = (TObjectType)oldObj;
                var NewObj = (TObjectType)newObj;
                //Create
                if (OldObj == null)
                {
                    OldObj = NewObj;
                    OldObj.State = 0;
                    OldObj = DataContainer.DbSet<TObjectType>().Add(OldObj);
                }
                var oldState = DataContainer.DbSet<TObjectType>().FirstOrDefault(q=>q.Id == oldObj.Id).State;
                var newState = GetType(NewObj);
                if (DefaultState == null)
                    throw new NullReferenceException($"ALMComponent exception => DefaultState can't be NULL");

                //Find configuration
                var configuration = GetStateConfiguration().SingleOrDefault(q=>q.State.ToString() == oldState.ToString() && q.NextState.ToString() == newState.ToString());

                //Replace configuration if newState is default 
                if (DefaultState.Any(q => q.ToString() == newState.ToString()))
                    configuration = GetStateConfiguration().SingleOrDefault(q => q.NextState.ToString() == newState.ToString());

                if (configuration != null)
                {
                    configuration.Execute(OldObj, NewObj);
                    SetType(OldObj, GetType(NewObj));
                    result = true;
                }
            });
            return result;
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
        IEnumerable<ALMConfiguration<TObjectType, TEnumState>> GetStateConfiguration()
        {
            if (!Configurations.Any())
                throw new Exception($"Для типа {typeof(TEnumState).ToString()} необходимо настроить переходы жизненного цикла. Реализуйте метод _StateConfiguration");
            var countCheck = Configurations.GroupBy(q => new { q.State, q.NextState } );
            foreach (var item in countCheck.Where(q => q.Count() > 1))
            {
                throw new Exception($"Для типа {typeof(TEnumState).ToString()} состояние {item.Key.ToString()}, настроено более одного раза");
            }
            return Configurations;
        }
        #endregion
        #region Abstractions
        /// <summary>   State configuration.
        ///             Настройка конфигурации для измения состояния жизненного цикла </summary>
        ///
        /// <remarks>   AM Kozhevnikov, 29.01.2018. </remarks>
        ///
        /// <param name="list"> [in,out] The list. </param>
        protected abstract IEnumerable<ALMConfiguration<TObjectType, TEnumState>> Configurations { get; }
        /// <summary>
        /// Базовые состояния, переход в которые возможен из любого состояния
        /// </summary>
        protected abstract IEnumerable<TEnumState> DefaultState { get; }
        /// <summary>
        /// Получаем тип объекта из модели
        /// </summary>
        /// <param name="obj">Объект данных</param>
        /// <returns></returns>
        protected abstract TEnumState GetType(TObjectType obj);
        /// <summary>
        /// Получаем тип объекта из модели
        /// </summary>
        /// <param name="obj">Объект данных</param>
        /// <returns></returns>
        protected abstract void SetType(TObjectType obj, TEnumState state);
        #endregion
    }
}
