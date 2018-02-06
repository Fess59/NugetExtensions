using FessooFramework.Objects.Data;
using FessooFramework.Tools.DCT;
using FessooFramework.Tools.IOC;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FessooFramework.Tools.Repozitory
{
    /// <summary>   A data ALM. </summary>
    ///
    /// <remarks>   AM Kozhevnikov, 06.02.2018. </remarks>
    ///
    /// <typeparam name="TObjectType">  Type of the object type. </typeparam>
    /// <typeparam name="TEnumState">   Type of the enum state. </typeparam>
    public abstract class DataALM<TObjectType, TEnumState> : DataALMBase
        where TObjectType : EntityObject, new()
        where TEnumState : struct, IConvertible
    {
        #region Property
        /// <summary>
        /// Объект для которого определена ALM
        /// </summary>
        public override Type ObjectType => typeof(TObjectType);
        #endregion
        #region Constructor
        /// <summary>
        /// Базовый конструктор
        /// </summary>
        public DataALM()
        {
            UID = ObjectType.ToString();
        }
        #endregion
        #region Methods
        
        #endregion
        #region Abstractions
        
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
