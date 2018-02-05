using FessooFramework.Objects.Data;
using FessooFramework.Tools.DCT;
using FessooFramework.Tools.IOC;
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
        private IOContainer<DataSourceBase> Container = new IOContainer<DataSourceBase>();
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
        public void Add<T>()
            where T : DbContext, new()
        {
            Container.Add(new DataSourceDbContext<T>());
        }
        public void SaveChanges()
        {
            foreach (var item in Container.GetAll())
                item.SaveChanges();
        }
        public override void Dispose()
        {
            base.Dispose();
            foreach (var item in Container.GetAll())
                item.Dispose();
        }
        public T Context<T>() where T : DbContext
        {
            var name = typeof(T).ToString();
            var element = Container.GetByName(name);
            var context = element.GetContext();
            var dbContext = context as T;
            return dbContext;
        }
        public DbSet<T> DbSet<T>() where T : EntityObject
        {
            var result = default(DbSet<T>);
            DCTDefault.Execute(c =>
            {

                var dbContexts = Container.Where(q => q.CheckType<T>());
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
