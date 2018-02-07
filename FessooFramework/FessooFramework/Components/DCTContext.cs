using FessooFramework.Core;
using FessooFramework.Objects;
using FessooFramework.Objects.Data;
using FessooFramework.Objects.SourceData;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FessooFramework.Components
{

    /// <summary>   A dct context.
    ///             Контекст данных - наследуем кастомный контекст </summary>
    ///
    /// <remarks>   AM Kozhevnikov, 25.01.2018. </remarks>
    public class DCTContext : SystemComponent
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
        /// <summary>   Gets the core configuration.
        ///             Глобальная конфигурация ядра </summary>
        ///
        /// <value> The core configuration. </value>
        public SystemCoreConfiguration _Configuration => SystemCore.Current.CoreConfiguration;
        #endregion
        #region Constructor
        /// <summary>   Default constructor. </summary>
        ///
        /// <remarks>   AM Kozhevnikov, 07.02.2018. </remarks>
        public DCTContext()
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
            _Configuration.Store.SaveChanges();
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
            _Configuration.Store.Dispose();
        }

        /// <summary>   Database set by Type. </summary>
        ///
        /// <remarks>   AM Kozhevnikov, 06.02.2018. </remarks>
        ///
        /// <typeparam name="T">    Generic type parameter. </typeparam>
        ///
        /// <returns>   A DbSet&lt;T&gt; </returns>

        public DbSet<T> DbSet<T>() where T : EntityObject
        {
            return _Configuration.Store.DbSet<T>();
        }
        #endregion
        #region Abstraction

        /// <summary>
        ///     Configurings this object. Настройка текущего модуля - наложение кастомных настроек и
        ///     подписки глобальных настроек.
        /// </summary>
        ///
        /// <remarks>   AM Kozhevnikov, 07.02.2018. </remarks>

        public override void _2_Configuring()
        {

        }

        /// <summary>   Loadings this object. Загрузка данных для объекта и обработка данных. </summary>
        ///
        /// <remarks>   AM Kozhevnikov, 07.02.2018. </remarks>

        protected override void _3_Loaded()
        {
        }

        /// <summary>
        ///     Testing this object. Проверка компонента на определённые условия - в будующем в этом
        ///     месте требуется подключение модуля динамической отладки с выводом предпреждений.
        /// </summary>
        ///
        /// <remarks>   AM Kozhevnikov, 07.02.2018. </remarks>
        ///
        /// <returns>
        ///     An enumerator that allows foreach to be used to process testing in this collection.
        /// </returns>

        protected override IEnumerable<TestingCase> _4_Testing()
        {
            return Enumerable.Empty<TestingCase>();
        }

        /// <summary>   Launchings this object. </summary>
        ///
        /// <remarks>   AM Kozhevnikov, 07.02.2018. </remarks>

        public override void _5_Launching()
        {
        }

        /// <summary>   Complitings this object. Работа с компонентом была завершена. </summary>
        ///
        /// <remarks>   AM Kozhevnikov, 07.02.2018. </remarks>

        protected override void _6_Unload()
        {
        }
        #endregion
    }
}
