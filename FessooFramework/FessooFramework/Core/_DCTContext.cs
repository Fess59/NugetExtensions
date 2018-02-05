using FessooFramework.Objects;
using FessooFramework.Objects.SourceData;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FessooFramework.Core
{
    /// <summary>   A dct context.
    ///             Контекст данных - наследуем кастомный контекст </summary>
    ///
    /// <remarks>   AM Kozhevnikov, 25.01.2018. </remarks>

    public class _DCTContext : SystemObject
    {
        #region Property

        /// <summary>   Gets or sets the identifier of the track.
        ///              </summary>
        ///TODO заменить на трэк модуль
        /// <value> The identifier of the track. </value>

        public Guid TrackId { get; set; }

        /// <summary>   Gets or sets the identifier of the parent track. 
        ///             Идентификатор родителя</summary>
        ///
        /// <value> The identifier of the parent track. </value>

        public Guid ParentTrackId { get; internal set; }

        /// <summary>   The store. 
        ///             Данные контекста</summary>
        protected DataContextStore _Store = new DataContextStore();
        #endregion
        #region Constructor
        public _DCTContext()
        {
            //TODO TrackModule
            TrackId = Guid.NewGuid();
        }
        #endregion
        #region Methods

        /// <summary>   Saves the changes.
        ///             Сохраняем все изменения, во всех базах</summary>
        ///
        /// <remarks>   AM Kozhevnikov, 01.02.2018. </remarks>

        public void SaveChanges()
        {
            _Store.SaveChanges();
        }
        /// <summary>
        ///     Обертка для IDisposable.Dispose в SystemObject Создана для возможности очистки объекта во
        ///     всех наследуемых сущностях
        ///     
        ///     Выполняет определяемые приложением задачи, связанные с удалением, высвобождением или
        ///     сбросом неуправляемых ресурсов.
        ///     
        ///     Очищает контекст данных и высвобождает все DbContext
        /// </summary>
        ///
        /// <remarks>   AM Kozhevnikov, 01.02.2018. </remarks>

        public override void Dispose()
        {
            base.Dispose();
            _Store.Dispose();
        }
        #endregion
    }
}
