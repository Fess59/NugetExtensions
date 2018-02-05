using FessooFramework.Objects.Data;
using FessooFramework.Tools.Helpers;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Example._0_Base.Data.DataComponent.ModelX
{
    public class ModelX : EntityObject
    {
        public string Description { get; set; }
        [NotMapped]
        public ModelXState StateEnum
        {
            get => EnumHelper.GetValue<ModelXState>(State);
            set => State = (int)value;
        }

        /// <summary> Сохраняем изменения объекта  на основании его состояния. Для фиксации измениний в базе не обходимо вызвать SaveChanges</summary>
        ///
        /// <remarks>   Fess59, 02.02.2018. </remarks>

        public void _Save()
        {
            DataContainer.Save(this);
        }
        public DbSet<ModelX> _DbSet()
        {
            return DbSet();
        }
        public T _Convert<T>() where T : class => DataContainer.Convert<T>(this);

        public static DbSet<ModelX> DbSet()
        {
            return DataContainer.DbSet<ModelX>();
        }
      
    }

   //TODO Create classicator from state
    public enum ModelXState
    {
        Create = 0,
        Edited = 1,
        Edited2 = 2,
        Edited3 = 3,
        Error = 4
    }
}
