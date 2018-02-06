using FessooFramework.Tools.DCT;
using FessooFramework.Tools.Helpers;
using FessooFramework.Tools.Repozitory;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FessooFramework.Objects.Data
{
    /// <summary>   An entity object a ALM. </summary>
    ///
    /// <remarks>   AM Kozhevnikov, 06.02.2018. </remarks>
    ///
    /// <typeparam name="TObjectType">  Type of the object type. </typeparam>
    /// <typeparam name="TStateType">   Type of the state type. </typeparam>

    public abstract class EntityObjectALM<TObjectType, TStateType> : EntityObject
        where TObjectType : EntityObjectALM<TObjectType, TStateType>, new()
         where TStateType : struct, IConvertible
    {
        /// <summary>   State configuration.
        ///             Настройка конфигурации для измения состояния жизненного цикла </summary>
        ///
        /// <remarks>   AM Kozhevnikov, 29.01.2018. </remarks>
        ///
        /// <param name="list"> [in,out] The list. </param>
        protected abstract IEnumerable<EntityObjectALMConfiguration<TObjectType, TStateType>> Configurations { get; }
        /// <summary>
        /// Базовые состояния, переход в которые возможен из любого состояния
        /// </summary>
        protected abstract IEnumerable<TStateType> DefaultState { get; }
        /// <summary>
        /// Реализует метод конвертации Enum в int
        /// </summary>
        /// <param name="state"></param>
        /// <returns></returns>
        protected abstract int GetStateValue(TStateType state);
        /// <summary>
        /// Состояние объекта в Enum
        /// </summary>
        [NotMapped]
        public TStateType StateEnum
        {
            get => EnumHelper.GetValue<TStateType>(State);
            set => State = GetStateValue(value);
        }
        ///// <summary> Сохраняем изменения объекта  на основании его состояния. Для фиксации измениний в базе не обходимо вызвать SaveChanges</summary>
        /////
        ///// <remarks>   Fess59, 02.02.2018. </remarks>
        public void _Save()
        {
            Update(this);
        }
        /// <summary> Database set. </summary>
        ///
        /// <remarks> AM Kozhevnikov, 06.02.2018. </remarks>
        ///
        /// <typeparam name="T">  Generic type parameter. </typeparam>
        ///
        /// <returns> A DbSet&lt;T&gt; </returns>
        public DbSet<TObjectType> _DbSet()
        {
            return DCTDefault.Context.DbSet<TObjectType>();
        }
        ///// <summary>   Gets the convert. </summary>
        /////
        ///// <remarks>   AM Kozhevnikov, 06.02.2018. </remarks>
        /////
        ///// <typeparam name="T">    Generic type parameter. </typeparam>
        /////
        ///// <returns>   A T. </returns>
        public TResult _Convert<TResult>() where TResult : class => DataContainer.Convert<TResult>(this);
        /// <summary>   Database set. </summary>
        ///
        /// <remarks>   AM Kozhevnikov, 06.02.2018. </remarks>
        ///
        /// <typeparam name="T">    Generic type parameter. </typeparam>
        ///
        /// <returns>   A DbSet&lt;T&gt; </returns>
        public static DbSet<TObjectType> DbSet()
        {
            return DCTDefault.Context.DbSet<TObjectType>();
        }
        #region ALM
        /// <summary>
        /// Основной метод - обновление модели данных
        /// </summary>
        /// <param name="newObj"></param>
        /// <returns></returns>
        private bool Update(EntityObject newObj)
        {
            var result = false;
            DCTDefault.Execute(c =>
            {
                var dbSet = DCTDefault.Context.DbSet<TObjectType>();
                var OldObj = dbSet.FirstOrDefault(q => q.Id == newObj.Id);
                var NewObj = newObj as TObjectType;
                //Create
                if (OldObj == null)
                {
                    OldObj = new TObjectType();
                    OldObj.Id = newObj.Id;
                    OldObj.State = 0;
                    OldObj = DCTDefault.Context.DbSet<TObjectType>().Add(OldObj);
                }
                var oldState = OldObj.StateEnum;
                var newState = NewObj.StateEnum;
                if (DefaultState == null)
                    throw new NullReferenceException($"ALMComponent exception => DefaultState can't be NULL");

                //Find configuration
                var configuration = GetStateConfiguration().SingleOrDefault(q => q.State.ToString() == oldState.ToString() && q.NextState.ToString() == newState.ToString());

                //Replace configuration if newState is default 
                if (DefaultState.Any(q => q.ToString() == newState.ToString()))
                    configuration = GetStateConfiguration().SingleOrDefault(q => q.NextState.ToString() == newState.ToString());

                if (configuration != null)
                {
                    configuration.Execute(OldObj, NewObj);
                    OldObj.State = NewObj.State;
                    result = true;
                }
                else
                {
                    OldObj.State = SetError();
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
        IEnumerable<EntityObjectALMConfiguration<TObjectType, TStateType>> GetStateConfiguration()
        {
            if (!Configurations.Any())
                throw new Exception($"Для типа {typeof(TStateType).ToString()} необходимо настроить переходы жизненного цикла. Реализуйте метод _StateConfiguration");
            var countCheck = Configurations.GroupBy(q => new { q.State, q.NextState });
            //foreach (var item in countCheck.Where(q => q.Count() > 1))
            //{
            //    throw new Exception($"Для типа {typeof(TStateType).ToString()} состояние {item.Key.ToString()}, настроено более одного раза");
            //}
            return Configurations;
        }
        /// <summary>
        /// Вызывается в случае ошибки обработки перехода объекта в другое состояние
        /// </summary>
        /// <param name="obj"></param>
        /// <param name="state"></param>
        protected virtual int SetError()
        {
            return -1;
        }
        #endregion
    }
}
