using FessooFramework.Objects.Data;
using FessooFramework.Tools.DCT;
using FessooFramework.Tools.IOC;
using FessooFramework.Tools.Web.DataService;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FessooFramework.Objects.SourceData
{
    /// <summary>   A data source store.
    ///             Источник данных </summary>
    ///
    /// <remarks>   AM Kozhevnikov, 31.01.2018. </remarks>

    public class DataContextStore : SystemObject
    {
        #region Property source
        private IOContainer<DataSourceBase> DataContextContainer = new IOContainer<DataSourceBase>();
        private IOContainer<DataSourceServiceBase> DataContextServiceContainer = new IOContainer<DataSourceServiceBase>();
        
        #endregion
        #region Property sets
        //public DbSet<FirstModel> First => GetContext<DefaultDB>().GetSet<FirstModel>();
        #endregion
        #region Constructor
        //public DataContextStore()
        //{
        //}
        #endregion
        #region Methods

        /// <summary>   Add - удалённая база. Добавить контекст данных, при кастомном DCTContext или при инициализации копии базового _DCTContext  </summary>
        ///
        /// <remarks>   AM Kozhevnikov, 07.02.2018. </remarks>
        ///
        /// <typeparam name="T">    Generic type parameter. </typeparam>

        public void Add<T>(string dbName, string address, string login, string password)
            where T : DbContext, new()
        {
            DataContextContainer.Add(new DataSourceDbContext<T>(dbName, address, login, password));
        }
        /// <summary>   Add - локальная база. Добавить контекст данных, при кастомном DCTContext или при инициализации копии базового _DCTContext  </summary>
        ///
        /// <remarks>   AM Kozhevnikov, 07.02.2018. </remarks>
        ///
        /// <typeparam name="T">    Generic type parameter. </typeparam>

        public void Add<T>(string dbName)
            where T : DbContext, new()
        {
            DataContextContainer.Add(new DataSourceDbContext<T>(dbName));
        }
        public void Add<T>()
           where T : DataServiceClient, new()
        {
            DataContextServiceContainer.Add(new DataSourceServiceContext<T>());
        }
        /// <summary>   Saves the changes. Сохраняет изменения во всех контекстах данных прошедших инициализацию</summary>
        ///
        /// <remarks>   AM Kozhevnikov, 07.02.2018. </remarks>

        public void SaveChanges()
        {
            foreach (var item in DataContextContainer.GetAll())
                item.SaveChanges();
            foreach (var item in DataContextServiceContainer.GetAll())
                item.SaveChanges();
        }

        /// <summary>
        ///     Вызывает очистку у всех инициализированных контекстах
        /// 
        ///     Обертка для IDisposable.Dispose в SystemObject Создана для возможности очистки объекта во
        ///     всех наследуемых сущностях
        ///     
        ///     Выполняет определяемые приложением задачи, связанные с удалением, высвобождением или
        ///     сбросом неуправляемых ресурсов.
        /// </summary>
        ///
        /// <remarks>   AM Kozhevnikov, 07.02.2018. </remarks>
        public override void Dispose()
        {
            base.Dispose();
            foreach (var item in DataContextContainer.GetAll())
                item.Dispose();
            foreach (var item in DataContextServiceContainer.GetAll())
                item.Dispose();
        }
        /// <summary>   Gets the context. Получение DbContext по типу </summary>
        ///
        /// <remarks>   AM Kozhevnikov, 07.02.2018. </remarks>
        ///
        /// <typeparam name="T">    Generic type parameter. </typeparam>
        ///
        /// <returns>   A T. </returns>
        public T Context<T>() where T : DbContext
        {
            var name = typeof(T).ToString();
            var element = DataContextContainer.GetByName(name);
            var context = element.GetContext();
            var dbContext = context as T;
            return dbContext;
        }
        /// <summary>   Gets the context. Получение DbContext по типу </summary>
        ///
        /// <remarks>   AM Kozhevnikov, 07.02.2018. </remarks>
        ///
        /// <typeparam name="T">    Generic type parameter. </typeparam>
        ///
        /// <returns>   A T. </returns>
        public T ServiceContext<T>() where T : DataServiceClient
        {
            var name = typeof(T).ToString();
            var element = DataContextServiceContainer.GetByName(name);
            var context = element.GetContext();
            var dbContext = context as T;
            return dbContext;
        }
        /// <summary>   Database set. Получение DbSet по модели данных </summary>
        ///
        /// <remarks>   AM Kozhevnikov, 07.02.2018. </remarks>
        ///
        /// <throwses cref="Exception"> Thrown when an exception error condition occurs. </throwses>
        ///
        /// <typeparam name="T">    Generic type parameter. </typeparam>
        ///
        /// <returns>   A DbSet&lt;T&gt; </returns>
        public DbSet<T> DbSet<T>() where T : EntityObject
        {
            var result = default(DbSet<T>);
            DCT.Execute(c =>
            {
                var type = typeof(T).ToString();

                var dbContexts = DataContextContainer.Where(q => q.CheckType<T>());
                if (dbContexts.Count() > 0)
                {
                    if (dbContexts.Count() > 1)
                        throw new Exception($"DataContextStore найдено более одного контекста данных для модели {typeof(T).Name}");
                    var db = dbContexts.FirstOrDefault();
                    result = db.DbSet<T>();
                }
            });
            return result;
        }
        #endregion
    }
}
