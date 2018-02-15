using FessooFramework.Objects.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FessooFramework.Tools.DataContexts.Models
{
    public class UserSession : EntityObjectALM<UserSession, UserSessionState>
    {
        #region Property
        public Guid UserProfileId { get; set; }
        /// <summary>
        /// Срок жизни сессии 
        /// </summary>
        public DateTime? TTL { get; set; }
        public DateTime? BlockedDate { get; set; }
        #endregion
        #region ALM
        protected override IEnumerable<EntityObjectALMConfiguration<UserSession, UserSessionState>> Configurations => new[] {
            EntityObjectALMConfiguration<UserSession, UserSessionState>.New(UserSessionState.Create, UserSessionState.Enable, Edited),
              EntityObjectALMConfiguration<UserSession, UserSessionState>.New(UserSessionState.Enable, UserSessionState.Enable, Edited),
                EntityObjectALMConfiguration<UserSession, UserSessionState>.New(UserSessionState.Blocked, UserSessionState.Enable, Unblocked),
             EntityObjectALMConfiguration<UserSession, UserSessionState>.New(UserSessionState.Blocked, UserSessionState.Blocked, Blocked),
        };
        protected override IEnumerable<UserSessionState> DefaultState => new[] { UserSessionState.Blocked };
        protected override IEnumerable<EntityObjectALMCreator<UserSession>> CreatorsService => throw new NotImplementedException($" Модель данных Application не может преобразована в модель данных клиента");
        protected override int GetStateValue(UserSessionState state)
        {
            return (int)state;
        }
        #endregion
        #region ALM methods
        private static UserSession Edited(UserSession arg1, UserSession arg2)
        {
            if (arg1.UserProfileId == Guid.Empty)
                arg1.UserProfileId = arg2.UserProfileId;
            arg1.TTL = arg2.TTL;
            return arg1;
        }
        private static UserSession Blocked(UserSession arg1, UserSession arg2)
        {
            if (arg1.BlockedDate == null)
                arg1.BlockedDate = DateTime.Now;
            return arg1;
        }
        private static UserSession Unblocked(UserSession arg1, UserSession arg2)
        {
            throw new Exception("Разблокировка доступа для аккаунта пользователя не возможна, пожалуйста восстановите пароль через API");
        }
        #endregion
    }
    public enum UserSessionState
    {
        Create = 0,
        Enable = 1,
        Blocked = 2
    }
}
