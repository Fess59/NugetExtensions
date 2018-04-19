using FessooFramework.Objects.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FessooFramework.Tools.DataContexts.Models
{

    public class UserAccess : EntityObjectALM<UserAccess, UserAccessState>
    {
        #region Property
        public Guid UserProfileId { get; set; }
        public string Login { get; set; }
        public string LoginHash { get; set; }
        public DateTime? BlockedDate { get; set; }
        #endregion
        #region ALM
        protected override IEnumerable<EntityObjectALMConfiguration<UserAccess, UserAccessState>> Configurations => new[] {
            EntityObjectALMConfiguration<UserAccess, UserAccessState>.New(UserAccessState.Create, UserAccessState.Enable, Edited),
              EntityObjectALMConfiguration<UserAccess, UserAccessState>.New(UserAccessState.Enable, UserAccessState.Enable, Edited),
                EntityObjectALMConfiguration<UserAccess, UserAccessState>.New(UserAccessState.Blocked, UserAccessState.Enable, Unblocked),
             EntityObjectALMConfiguration<UserAccess, UserAccessState>.New(UserAccessState.Blocked, UserAccessState.Blocked, Blocked),
        };
        protected override IEnumerable<EntityObjectALMCreator<UserAccess>> CreatorsService => throw new NotImplementedException($" Модель данных Application не может преобразована в модель данных клиента");
        protected override int GetStateValue(UserAccessState state)
        {
            return (int)state;
        }
        #endregion
        #region ALM methods
        private static UserAccess Edited(UserAccess arg1, UserAccess arg2)
        {
            if (arg1.UserProfileId == Guid.Empty)
                arg1.UserProfileId = arg2.UserProfileId;
            arg1.LoginHash = arg2.LoginHash;
            arg1.Login = arg2.Login;
            return arg1;
        }
        private static UserAccess Blocked(UserAccess arg1, UserAccess arg2)
        {
            if (arg1.BlockedDate == null)
                arg1.BlockedDate = DateTime.Now;
            return arg1;
        }
        private static UserAccess Unblocked(UserAccess arg1, UserAccess arg2)
        {
            throw new Exception("Разблокировка доступа для аккаунта пользователя не возможна, пожалуйста восстановите пароль через API");
        }

        protected override UserAccess SetValueDefault(UserAccess oldObj, UserAccess newObj)
        {
            if (oldObj.UserProfileId == Guid.Empty)
                oldObj.UserProfileId = newObj.UserProfileId;
            oldObj.LoginHash = newObj.LoginHash;
            oldObj.Login = newObj.Login;
            return oldObj;
        }
        #endregion
    }
    public enum UserAccessState
    {
        Create = 0,
        Enable = 1,
        Blocked = 2
    }
}
