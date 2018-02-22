using FessooFramework.Tools.DCT;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FessooFramework.Objects.Data
{
    /// <summary>   A data objet convertor configurations to service object. 
    ///             Entity Object ALM Configuration</summary>
    ///
    /// <remarks>   AM Kozhevnikov, 10.02.2018. </remarks>
    ///
    ///   <typeparam name="TObjectType">  Type of the object type. </typeparam>
    public class EntityObjectALMCreator<TObjectType> 
          where TObjectType : EntityObject
    {
        #region Property
        /// <summary>
        /// Тип модели службы
        /// </summary>
        public Type ObjectType { get; private set; }
        /// <summary>
        /// Тип модели службы
        /// </summary>
        public Type FinallyType { get; private set; }
        /// <summary>
        /// Тип модели службы
        /// </summary>
        public Version Version { get; private set; }
        /// <summary>
        /// Метод который обрабатывает конвертацию объект данных в объект службы
        /// </summary>
        private Func<TObjectType, object> _Execute { get; }
        /// <summary>
        /// Метод который обратной конвертации объекта службы в объект данных
        /// </summary>
        private Func<object, TObjectType> _RollbackExecute { get; set; }
        #endregion
        #region Constructor
        /// <summary>
        /// Базовый конструктор
        /// </summary>
        /// <param name="execute">Метод конвертации объекта данных в объект службы</param>
        public EntityObjectALMCreator(Func<TObjectType, object> execute, Func<object, TObjectType> rollback)
        {
            _Execute = execute;
            _RollbackExecute = rollback;
        }
        #endregion
        #region Methods
        /// <summary>
        /// Выполняем конвертацию объекта данных в объкт службы
        /// </summary>
        /// <param name="obj"></param>
        /// <typeparam name="TServiceModelType">   Type of the finally object state. </typeparam>
        /// <returns></returns>
        public TServiceModelType Execute<TServiceModelType>(TObjectType obj)
            where TServiceModelType : CacheObject
        {
            var result = default(TServiceModelType);
            DCT.Execute(q => {
                var modelObj = _Execute(obj);
                result = (TServiceModelType)modelObj;
                result.SetProperty(result.Id, result.CreateDate, result.HasRemoved, typeof(TObjectType).ToString(), Version);
            });
            return result;
        }
        /// <summary>
        /// Выполняем конвертацию объекта данных в объкт службы
        /// </summary>
        /// <param name="obj"></param>
        /// <typeparam name="TServiceModelType">   Type of the finally object state. </typeparam>
        /// <returns></returns>
        public TObjectType ExecuteRollback<TServiceModelType>(TServiceModelType obj)
            where TServiceModelType : CacheObject
        {
            var result = default(TObjectType);
            DCT.Execute(q => {
                var entity = _RollbackExecute(obj);
                entity.SetProperty(obj.Id, obj.CreateDate, obj.HasRemoved);
                result = entity;
            });
            return result;
        }
        /// <summary>
        /// Создаёт новый шаблон для конвертации модели данных в модель службы 
        /// </summary>
        /// <param name="finallyType"></param>
        /// <param name="execute"></param>
        /// <param name="version"></param>
        /// <returns></returns>
        public static EntityObjectALMCreator<TObjectType> New<TFinalyType>(Func<TObjectType, TFinalyType> execute, Func<TFinalyType, TObjectType> roolbackExecute, Version version)
            where TFinalyType : CacheObject
        {
            return new EntityObjectALMCreator<TObjectType>(execute, (a) => roolbackExecute((TFinalyType)a))
            {
                FinallyType = typeof(TFinalyType),
                ObjectType = typeof(TObjectType),
                Version = version,
            };
        }
        #endregion
    }
}
