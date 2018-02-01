using FessooFramework.Objects.Data;
using System;

namespace Example._0_Base.ClientModels
{
    public class ClientCacheModel2 : CacheObject
    {
        public override Enum SetDataType()
        {
            return CurrentDataType.ClientModel;
        }
        public override TimeSpan SetTTL()
        {
            return TimeSpan.FromDays(30);
        }

        public override Version SetVersion()
        {
            return new Version(1,0,0,0);
        }
    }
    public class ClientCacheModel : CacheObject
    {
        public override Enum SetDataType()
        {
            throw new NotImplementedException();
        }

        public override TimeSpan SetTTL()
        {
            throw new NotImplementedException();
        }

        public override Version SetVersion()
        {
            throw new NotImplementedException();
        }
    }

    public enum CurrentDataType
    {
        None = 0,
        ClientModel = 1
    }
}
