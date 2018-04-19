using FessooFramework.Objects.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FessooFramework.Tools.DataContexts.Models
{

    public class UserProfile : EntityObjectALM<UserProfile, UserProfileState>
    {
        #region Property
        public Guid ApplicationId { get; set; }
        public string FirstName { get; set; }
        public string SecondName { get; set; }
        public string MiddleName { get; set; }
        public DateTime Birthday { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public string Login { get; set; }
        public DateTime? BlockedDate { get; set; }
        #endregion
        #region ALM
        protected override IEnumerable<EntityObjectALMConfiguration<UserProfile, UserProfileState>> Configurations => new[] {
            EntityObjectALMConfiguration<UserProfile, UserProfileState>.New(UserProfileState.Create, UserProfileState.Enable, Edited),
              EntityObjectALMConfiguration<UserProfile, UserProfileState>.New(UserProfileState.Enable, UserProfileState.Enable, Edited),
                EntityObjectALMConfiguration<UserProfile, UserProfileState>.New(UserProfileState.Blocked, UserProfileState.Enable, Unblocked),
             EntityObjectALMConfiguration<UserProfile, UserProfileState>.New(UserProfileState.Blocked, UserProfileState.Blocked, Blocked),
        };
        protected override IEnumerable<EntityObjectALMCreator<UserProfile>> CreatorsService => throw new NotImplementedException($" Модель данных Application не может преобразована в модель данных клиента");
        protected override int GetStateValue(UserProfileState state)
        {
            return (int)state;
        }
        #endregion
        #region ALM methods
        private static UserProfile Edited(UserProfile arg1, UserProfile arg2)
        {
            arg1.ApplicationId = arg2.ApplicationId;
            arg1.FirstName = arg2.FirstName;
            arg1.SecondName = arg2.SecondName;
            arg1.MiddleName = arg2.MiddleName;
            arg1.Birthday = arg2.Birthday;
            arg1.Email = arg2.Email;
            arg1.Phone = arg2.Phone;
            arg1.Login = arg2.Login;
            return arg1;
        }
        private static UserProfile Blocked(UserProfile arg1, UserProfile arg2)
        {
            if (arg1.BlockedDate == null)
                arg1.BlockedDate = DateTime.Now;
            return arg1;
        }
        private static UserProfile Unblocked(UserProfile arg1, UserProfile arg2)
        {
            if (arg1.BlockedDate != null)
                arg1.BlockedDate = null;
            return arg1;
        }

        protected override UserProfile SetValueDefault(UserProfile oldObj, UserProfile newObj)
        {
            oldObj.ApplicationId = newObj.ApplicationId;
            oldObj.FirstName = newObj.FirstName;
            oldObj.SecondName = newObj.SecondName;
            oldObj.MiddleName = newObj.MiddleName;
            oldObj.Birthday = newObj.Birthday;
            oldObj.Email = newObj.Email;
            oldObj.Phone = newObj.Phone;
            oldObj.Login = newObj.Login;
            return oldObj;
        }
        #endregion
    }
    public enum UserProfileState
    {
        Create = 0,
        Enable = 1,
        Blocked = 2
    }
}
