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
        #endregion
        #region Constructor
        /// <summary>
        /// Базовый конструктор
        /// </summary>
        /// <param name="execute">Метод конвертации объекта данных в объект службы</param>
        public EntityObjectALMCreator(Func<TObjectType, object> execute)
        {
            _Execute = execute;
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
                result.DataType = typeof(TObjectType).ToString();
                result.Version = Version.ToString();
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
        public static EntityObjectALMCreator<TObjectType> New<TFinalyType>(Func<TObjectType, TFinalyType> execute, Version version)
            where TFinalyType : CacheObject
        {
            return new EntityObjectALMCreator<TObjectType>(execute)
            {
                FinallyType = typeof(TFinalyType),
                ObjectType = typeof(TObjectType),
                Version = version
            };
        }
        #endregion
    }
}
