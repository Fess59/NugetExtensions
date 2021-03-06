﻿using FessooFramework.Tools.DCT;
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
        #region Property
        /// <summary>
        /// Состояние объекта в Enum
        /// </summary>
        [NotMapped]
        public TStateType StateEnum
        {
            get => EnumHelper.GetValue<TStateType>(State);
            set
            {
                State = GetStateValue(value);
                _Save();
            }
        }
        #endregion
        #region Configuration
        /// <summary>   State configuration.
        ///             Настройка конфигурации для измения состояния жизненного цикла </summary>
        ///
        /// <remarks>   AM Kozhevnikov, 29.01.2018. </remarks>
        protected virtual IEnumerable<EntityObjectALMConfiguration<TObjectType, TStateType>> Configurations => Enumerable.Empty<EntityObjectALMConfiguration<TObjectType, TStateType>>();

        /// <summary>   State configuration.
        ///             Настройка конфигурации для измения состояния жизненного цикла </summary>
        ///
        /// <remarks>   AM Kozhevnikov, 29.01.2018. </remarks>
        private IEnumerable<EntityObjectALMConfiguration<TObjectType, TStateType>> configurations
        {
            get
            {
                if (_Configurations == null)
                    _Configurations = Configurations;
                return _Configurations;
            }
        }
        /// <summary>   State configuration.
        ///             Настройка конфигурации для измения состояния жизненного цикла </summary>
        ///
        /// <remarks>   AM Kozhevnikov, 29.01.2018. </remarks>
        private static IEnumerable<EntityObjectALMConfiguration<TObjectType, TStateType>> _Configurations { get; set; }
        #endregion
        #region Default state
        /// <summary>   State configuration.
        ///             Настройка конфигурации для измения состояния жизненного цикла </summary>
        ///
        /// <remarks>   AM Kozhevnikov, 29.01.2018. </remarks>
        protected abstract TObjectType SetValueDefault(TObjectType oldObj, TObjectType newObj);
        #endregion
        #region DB integrations
        /// <summary>
        /// Получаем DbSet для этой модели
        /// </summary>
        /// <returns></returns>
        public static DbSet<TObjectType> DbSet()
        {
            return DCT.Context.DbSet<TObjectType>();
        }
        /// <summary> Сохраняем изменения объекта  на основании его состояния. Для фиксации измениний в базе не обходимо вызвать SaveChanges</summary>
        ///
        /// <remarks>   Fess59, 02.02.2018. </remarks>
        public void _Save()
        {
            Update(this);
        }
        /// <summary>
        ///Получаем DbSet для этой модели
        /// </summary>
        /// <returns></returns>
        public DbSet<TObjectType> _DbSet()
        {
            return DbSet();
        }
        #endregion
        #region Convertors
        /// <summary>   Gets the convert. </summary>
        ///
        /// <remarks>   AM Kozhevnikov, 06.02.2018. </remarks>
        ///
        /// <typeparam name="TResult">    Generic type parameter. </typeparam>
        ///
        /// <returns>   A T. </returns>
        public override TResult _ConvertToServiceModel<TResult>()
        {
            var result = default(TResult);
            if (creatorsService == null || !creatorsService.Any())
                throw new Exception($"CREATORS to ServiceModel not found any from '{typeof(TObjectType).Name}'");
            var creators = _CreatorsService.Where(q => q.ObjectType == typeof(TObjectType) && q.FinallyType == typeof(TResult));
            if (!creators.Any())
                throw new Exception($"CREATORS to ServiceModel not found from '{typeof(TObjectType).Name}' to '{typeof(TResult).Name}'");
            if (creators.Count() > 1)
                throw new Exception($"CREATORS to ServiceModel найдено несколько схем конвертации для модели '{typeof(TObjectType).Name}' в модель '{typeof(TResult).Name}'");
            var creator = creators.FirstOrDefault();
            result = creator.Execute<TResult>((TObjectType)this, StateEnum.ToString());
            if (result == null)
                throw new NullReferenceException($"CREATORS to ServiceModel from '{typeof(TObjectType).Name}' to '{typeof(TResult).Name}' return NULL");
            return result;
        }
        /// <summary>   Gets the convert. </summary>
        ///
        /// <remarks>   AM Kozhevnikov, 06.02.2018. </remarks>
        ///
        /// <typeparam name="TResult">    Generic type parameter. </typeparam>
        ///
        /// <returns>   A T. </returns>
        public override EntityObject _ConvertToDataModel<TResult>(TResult obj)
        {
            var result = default(EntityObject);
            if (creatorsService == null || !creatorsService.Any())
                throw new Exception($"CREATORS to ServiceModel not found any from '{typeof(TObjectType).Name}'");
            var creators = _CreatorsService.Where(q => q.ObjectType == typeof(TObjectType) && q.FinallyType == typeof(TResult));
            if (!creators.Any())
                throw new Exception($"CREATORS to ServiceModel not found from '{typeof(TObjectType).Name}' to '{typeof(TResult).Name}'");
            if (creators.Count() > 1)
                throw new Exception($"CREATORS to ServiceModel найдено несколько схем конвертации для модели '{typeof(TObjectType).Name}' в модель '{typeof(TResult).Name}'");
            var creator = creators.FirstOrDefault();
            var entity = new TObjectType()._DbSet().FirstOrDefault(q=> q.Id == obj.Id);
            if (entity == null)
                entity = new TObjectType();
            result = creator.ExecuteRollback<TResult>(obj, entity);
            if (result == null)
                throw new NullReferenceException($"CREATORS to ServiceModel from '{typeof(TObjectType).Name}' to '{typeof(TResult).Name}' return NULL");
            return result;
        }
        /// <summary>   State configuration.
        ///             Настройка конфигурации для измения состояния жизненного цикла </summary>
        ///
        /// <remarks>   AM Kozhevnikov, 29.01.2018. </remarks>
        protected abstract IEnumerable<EntityObjectALMCreator<TObjectType>> CreatorsService { get; }

        /// <summary>   State configuration.
        ///             Настройка конфигурации для измения состояния жизненного цикла </summary>
        ///
        /// <remarks>   AM Kozhevnikov, 29.01.2018. </remarks>
        private IEnumerable<EntityObjectALMCreator<TObjectType>> creatorsService
        {
            get
            {
                if (_CreatorsService == null)
                    _CreatorsService = CreatorsService;
                return _CreatorsService;
            }
        }
        /// <summary>
        /// Хранилище конверторов из модели в данных в модель службы
        /// </summary>
        private static IEnumerable<EntityObjectALMCreator<TObjectType>> _CreatorsService { get; set; }
        #endregion
        #region ALM
        /// <summary>
        /// Реализует метод конвертации Enum в int
        /// </summary>
        /// <param name="state"></param>
        /// <returns></returns>
        protected abstract int GetStateValue(TStateType state);
        /// <summary>
        /// Основной метод - обновление модели данных
        /// </summary>
        /// <param name="newObj"></param>
        /// <returns></returns>
        private bool Update(EntityObject newObj)
        {
            var result = false;
            DCT.Execute(c =>
            {
                var dbSet = DCT.Context.DbSet<TObjectType>();
                var OldObj = dbSet.FirstOrDefault(q => q.Id == newObj.Id);
                var NewObj = newObj as TObjectType;
                //Create
                if (OldObj == null)
                {
                    //Проверяю модель в уже добавленные
                    OldObj = DCT.Context.DbSet<TObjectType>().Local.FirstOrDefault(q => q.Id == newObj.Id);
                    if (OldObj == null)
                    {
                        OldObj = new TObjectType();
                        OldObj.Id = newObj.Id;
                        OldObj.State = 0;
                        OldObj = DCT.Context.DbSet<TObjectType>().Add(OldObj);
                    }
                }
                var oldState = OldObj.StateEnum;
                var newState = NewObj.StateEnum;

                //Find configuration
                var configurations = GetStateConfiguration().Where(q => q.State.ToString() == oldState.ToString() && q.NextState.ToString() == newState.ToString());
                var configuration = configurations.FirstOrDefault();
                if (configurations.Count() > 1)
                    DCT.SendWarning($"Для объекта {typeof(TObjectType).Name}, найдено несколько переходов {oldState.ToString()}=>{newState.ToString()}. Рекомендуется проверить свойство Configurations", "EntityObjectALM");

                if (configuration != null)
                {
                    configuration.Execute(OldObj, NewObj);
                    OldObj.State = NewObj.State;
                    result = true;
                }
                else
                {
                    //Replace configuration if newState is default or not realized
                    SetValueDefault(OldObj, NewObj);
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

            if (!configurations.Any())
            {
                DCT.SendWarning($"Для объекта {typeof(TStateType).ToString()} не настроено ни одного перехода жизненного цикла. Проверьте реализацию свойства Configurations", "EntityObjectALM");
                return Enumerable.Empty<EntityObjectALMConfiguration<TObjectType, TStateType>>();
            }
            var countCheck = configurations.GroupBy(q => new { q.State, q.NextState });
            return configurations;
        }
        /// <summary>
        /// Вызывается в случае ошибки обработки перехода объекта в другое состояние
        /// </summary>
        protected virtual int SetError()
        {
            return -1;
        }
        #endregion
        #region Container
        public override EntityObject _ObjectLoadById(Guid id)
        {
            var obj = DbSet().Find(id);
            return obj;
        }
        public override IEnumerable<EntityObject> _CollectionObjectLoad()
        {
            var objs = DbSet().ToArray();
            return objs;
        }
        public override IEnumerable<TDataModel> _CacheSave<TDataModel>(IEnumerable<TDataModel> objs)
        {
            var collections = Enumerable.Empty<TDataModel>();
            DCT.Execute(c =>
            {
                if (objs.Any())
                {
                    foreach (var obj in objs)
                        (obj as TObjectType)._Save();
                    c.SaveChanges();
                }
            });
            return collections;
        }
        #endregion
        #region Custom query
        //public override CacheObject CustomObjectLoad(string code, string sessionUID = "", string hashUID = "", EntityObject obj = null, Guid? id = null)
        //{
        //    throw new NotImplementedException($"Ошибка при вызове метода'{code}'. Для модели данных {typeof(TObjectType).Name}, не реализован абстрактный метод CustomObjectLoad");
        //}
        public override IEnumerable<EntityObject> CustomCollectionLoad(string code, string sessionUID = "", string hashUID = "", IEnumerable<EntityObject> obj = null, IEnumerable<Guid> id = null)
        {
            throw new NotImplementedException($"Ошибка при вызове метода'{code}'. Для модели данных {typeof(TObjectType).Name}, не реализован абстрактный метод CustomCollectionLoad");
        }
        #endregion
    }
}
