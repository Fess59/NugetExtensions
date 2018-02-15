using FessooFramework.Objects.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FessooFramework.Tools.DataContexts.Models
{

    public class ApplicationAccess : EntityObjectALM<ApplicationAccess, ApplicationState>
    {
        #region Property
        public string Name { get; set; }
        public string Token { get; set; }
        public DateTime? BlockedDate { get; set; }
        #endregion
        #region ALM
        protected override IEnumerable<EntityObjectALMConfiguration<ApplicationAccess, ApplicationState>> Configurations => new[] {
            EntityObjectALMConfiguration<ApplicationAccess, ApplicationState>.New(ApplicationState.Create, ApplicationState.Enable, Edited),
              EntityObjectALMConfiguration<ApplicationAccess, ApplicationState>.New(ApplicationState.Enable, ApplicationState.Enable, Edited),
                EntityObjectALMConfiguration<ApplicationAccess, ApplicationState>.New(ApplicationState.Blocked, ApplicationState.Enable, Unblocked),
             EntityObjectALMConfiguration<ApplicationAccess, ApplicationState>.New(ApplicationState.Blocked, ApplicationState.Blocked, Blocked),
        };
        protected override IEnumerable<ApplicationState> DefaultState => new[] { ApplicationState.Blocked     };
        protected override IEnumerable<EntityObjectALMCreator<ApplicationAccess>> CreatorsService => throw new NotImplementedException($" Модель данных Application не может преобразована в модель данных клиента");
        protected override int GetStateValue(ApplicationState state)
        {
            return (int)state;
        }
        #endregion
        #region ALM methods
        private static ApplicationAccess Edited(ApplicationAccess arg1, ApplicationAccess arg2)
        {
            arg1.Token = arg2.Token;
            arg1.Name = arg2.Name;
            return arg1;
        }
        private static ApplicationAccess Blocked(ApplicationAccess arg1, ApplicationAccess arg2)
        {
            if (arg1.BlockedDate == null)
                arg1.BlockedDate = DateTime.Now;
            return arg1;
        }
        private static ApplicationAccess Unblocked(ApplicationAccess arg1, ApplicationAccess arg2)
        {
            if (arg1.BlockedDate != null)
                arg1.BlockedDate = null;
            return arg1;
        }
        #endregion
    }
    public enum ApplicationState
    {
        Create = 0,
        Enable = 1,
        Blocked = 2
    }
}
