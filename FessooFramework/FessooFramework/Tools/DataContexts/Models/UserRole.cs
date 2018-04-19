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

        public override IEnumerable<EntityObject> CustomCollectionLoad(string code, string sessionUID = "", string hashUID = "", IEnumerable<EntityObject> obj = null, IEnumerable<Guid> id = null)
        {
            throw new NotImplementedException();
        }

        public override IEnumerable<TDataModel> _CacheSave<TDataModel>(IEnumerable<TDataModel> objs)
        {
            throw new NotImplementedException();
        }

        public override IEnumerable<EntityObject> _CollectionObjectLoad()
        {
            throw new NotImplementedException();
        }

        public override EntityObject _ConvertToDataModel<TResult>(TResult obj)
        {

            throw new NotImplementedException("Передача модели данных UserRole с клиента не поддерживается");
        }

        public override TResult _ConvertToServiceModel<TResult>()
        {
            throw new NotImplementedException("Передача модели данных UserRole на клиент не поддерживается");
        }

        public override EntityObject _ObjectLoadById(Guid Id)
        {
            throw new NotImplementedException();
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
