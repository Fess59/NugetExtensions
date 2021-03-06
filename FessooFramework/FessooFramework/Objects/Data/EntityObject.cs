﻿using FessooFramework.Tools;
using FessooFramework.Tools.Helpers;
using FessooFramework.Tools.Repozitory;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FessooFramework.Objects.Data
{
    /// <summary>   An entity object.
    ///             Базовый объект базы данныx
    ///             Реализует концепцию опционального ведения истории изменения объекта 
    ///             TODO CORE Подвязку сохранения изменний вынести в настройки ядра </summary>
    ///
    ///
    /// <remarks>   AM Kozhevnikov, 24.01.2018. </remarks>
    public abstract class EntityObject : DataObject
    {
        #region Property
        /// <summary>   Gets or sets the save changes. </summary>
        ///
        /// <value> The save changes. </value>
        public static Action<EntityHistory> _SendToHistory { get; set; }
        /// <summary>   Gets or sets the identifier of the last change. </summary>
        ///
        /// <value> The identifier of the last change. </value>
        public Guid? LastChangeId { get; set; }

        /// <summary>   Gets or sets the state.
        ///             Default state = 0 </summary>
        ///
        /// <value> The state. </value>
        public int State { get; set; }
        #endregion
        #region Constructor
        /// <summary>   Default constructor. </summary>
        ///
        /// <remarks>   AM Kozhevnikov, 11.01.2018. </remarks>
        public EntityObject() : base()
        {
            State = 0;
            _ToHistory("Create");
        }
        #endregion
        #region Methods
        /// <summary>   Send this object to a history. </summary>
        ///
        /// <remarks>   AM Kozhevnikov, 24.01.2018. </remarks>
        ///
        /// <param name="comment">  (Optional) The comment. </param>
        /// <param name="userId">   (Optional) Identifier for the user. </param>
        public void _ToHistory(string comment = "Change", Guid? userId = null)
        {
            try
            {
                _SendToHistory?.Invoke(EntityHistory.New(Id, GetType().Name, userId, comment));
            }
            catch (Exception ex)
            {
                ConsoleHelper.SendException(ex);
            }
        }
        /// <summary>   Removes this object. Помечает объект удаленным. </summary>
        ///
        /// <remarks>   AM Kozhevnikov, 24.01.2018. </remarks>
        public override void _Remove()
        {
            HasRemoved = true;
            _ToHistory("Remove");
        }
        /// <summary>
        /// Конвертация модели данных в модель представления
        /// </summary>
        /// <typeparam name="TResult"></typeparam>
        /// <returns></returns>
        public abstract TResult _ConvertToServiceModel<TResult>() where TResult : CacheObject;
        /// <summary>
        /// Конвертация модели представления в модель данных
        /// </summary>
        /// <typeparam name="TResult"></typeparam>
        /// <param name="obj"></param>
        /// <returns></returns>
        public abstract EntityObject _ConvertToDataModel<TResult>(TResult obj) where TResult : CacheObject;
        /// <summary>
        /// Получение объекта по идентификатору
        /// </summary>
        /// <param name="Id"></param>
        /// <returns></returns>
        public abstract EntityObject _ObjectLoadById(Guid Id);
        /// <summary>
        /// Получение коллекции объектов на основе данных авторизации и сессии
        /// </summary>
        /// <returns></returns>
        public abstract IEnumerable<EntityObject> _CollectionObjectLoad();
        /// <summary>
        /// Сохранение данных в базу
        /// </summary>
        /// <typeparam name="TDataModel"></typeparam>
        /// <param name="objs"></param>
        /// <returns></returns>
        public abstract IEnumerable<TDataModel> _CacheSave<TDataModel>(IEnumerable<TDataModel> objs) where TDataModel : EntityObject;
        internal void SetProperty(Guid id, DateTime createDate, bool hasRemoved)
        {
            Id = id;
            CreateDate = createDate;
            HasRemoved = hasRemoved;
        }

        #endregion
        #region Custom query from Data model
        //public abstract CacheObject CustomObjectLoad(string code, string sessionUID = "", string hashUID = "", EntityObject obj = null, Guid? id = null);
        public abstract IEnumerable<EntityObject> CustomCollectionLoad(string code, string sessionUID = "", string hashUID = "", IEnumerable<EntityObject> obj = null, IEnumerable<Guid> id = null);
        #endregion
    }
}
