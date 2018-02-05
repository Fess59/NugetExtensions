using FessooFramework.Objects.Data;
using FessooFramework.Tools.DCT;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Example._0_Base.Data.DataComponent
{

    public class ALMConfiguration<TObjectType, TEnumState>
        where TObjectType : EntityObject
        where TEnumState : struct, IConvertible
    {
        #region Property
        public TEnumState State { get; }
        public TEnumState NextState { get; }
        private Func<TObjectType, TObjectType, TObjectType> _Execute { get; }
        #endregion
        #region Constructor
        public ALMConfiguration(TEnumState state, TEnumState nextState, Func<TObjectType, TObjectType, TObjectType> execute)
        {
            State = state;
            NextState = nextState;
            _Execute = execute;
        }
        #endregion
        #region Methods
        public TObjectType Execute(TObjectType oldObj, TObjectType newObj)
        {
            var result = oldObj;
            DCTExample.Execute(q => result = _Execute(oldObj, newObj));
            return result;
        }
        public static ALMConfiguration<TObjectType, TEnumState> New(TEnumState state, TEnumState nextState, Func<TObjectType, TObjectType, TObjectType> execute)
        {
            return new ALMConfiguration<TObjectType, TEnumState>(state, nextState, execute);
        }
        #endregion
    }
}
