using FessooFramework.Objects.ALM;
using FessooFramework.Tools.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Example.Tests
{
    public class ALMModel : ALMObject<ALMModelState>
    {
        public override void _StateChanged(ALMModelState newState, ALMModelState oldState)
        {
            ConsoleHelper.SendMessage($"Объект {typeof(ALMModelState).ToString()} осуществил пререход из {newState} в {oldState}");
        }
        public override IEnumerable<ALMConf<ALMModelState>> _StateConfiguration()
        {
            return new ALMConf<ALMModelState>[] 
            {
                ALMConf<ALMModelState>.New(ALMModelState.None, new ALMModelState[] { ALMModelState.Create }),
                ALMConf<ALMModelState>.New(ALMModelState.Create, new ALMModelState[] { ALMModelState.First }),
                ALMConf<ALMModelState>.New(ALMModelState.First, new ALMModelState[] { ALMModelState.Second }),
                ALMConf<ALMModelState>.New(ALMModelState.Second, new ALMModelState[] { ALMModelState.Third })
            };
        }
    }

    public enum ALMModelState
    {
        None =0,
        First = 1,
        Second = 2,
        Third = 3,
        Create = 4
    }
}
