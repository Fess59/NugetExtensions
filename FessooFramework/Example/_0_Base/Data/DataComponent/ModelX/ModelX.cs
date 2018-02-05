using FessooFramework.Objects.Data;
using FessooFramework.Tools.Helpers;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Example._0_Base.Data.DataComponent.ModelX
{
    public class ModelX : EntityObject
    {
        public string Description { get; set; }
        public int State { get; set; }
        public ModelXState StateEnum
        {
            get => EnumHelper.GetValue<ModelXState>(State);
            set => State = (int)value;
        }

        /// <summary> Сохраняем изменения объекта  на основании его состояния. Для фиксации измениний в базе не обходимо вызвать SaveChanges</summary>
        ///
        /// <remarks>   Fess59, 02.02.2018. </remarks>

        public void Save()
        {
            DataContainer.Save(this);
        }

        public static DbSet<ModelX> DbSet()
        {
            return DataContainer.DbSet(new ModelX());
        }

        public T Convert<T>() => DataContainer.Convert<T>(this);
    }

   //TODO Create classicator from state
    public enum ModelXState
    {
        Create = 0,
        Edited = 1,
        Edited2 = 2,
        Edited3 = 3
    }
}
