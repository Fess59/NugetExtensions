using FessooFramework.Objects.Data;
using FessooFramework.Tools.Helpers;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FessooFramework.Tools.DataContexts.Models
{
    /// <summary>
    /// Классификатор ролей - при создании базы требуеться генерация
    /// </summary>
    public class UserRole : EntityObject
    {
        public string Name { get; set; }
        public int UID { get; set; }
        /// <summary>
        /// Состояние объекта в Enum
        /// </summary>
        [NotMapped]
        public RoleType StateEnum
        {
            get => EnumHelper.GetValue<RoleType>(UID);
            set => UID = (int)value;
        }
    }

    public enum RoleType
    {
        None = 0,
        User = 1,
        Employee = 2,
        Admin = 3
    }

}
