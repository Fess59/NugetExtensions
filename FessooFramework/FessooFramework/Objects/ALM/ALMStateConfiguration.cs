using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FessooFramework.Objects.ALM
{
    /// <summary>   An alm state configuration. </summary>
    ///
    /// <remarks>   AM Kozhevnikov, 29.01.2018. </remarks>
    ///
    /// <typeparam name="TEnumState">   Type of the enum state. </typeparam>

    public class ALMConf<TEnumState>
        where TEnumState : struct, IConvertible
    {
        /// <summary>   Constructor. </summary>
        ///
        /// <remarks>   AM Kozhevnikov, 29.01.2018. </remarks>
        ///
        /// <param name="state">        The state. </param>
        /// <param name="nextState">    State of the next. </param>

        public ALMConf(TEnumState state, IEnumerable<TEnumState> nextState)
        {
            State = state;
            NextState = nextState;
        }

        /// <summary>   Gets the state. </summary>
        ///
        /// <value> The state. </value>

        public TEnumState State { get; }

        /// <summary>   Gets the state of the next. </summary>
        ///
        /// <value> The next state. </value>

        public IEnumerable<TEnumState> NextState { get; }

        /// <summary>   Create new ALMStateConfiguration. </summary>
        ///
        /// <remarks>   AM Kozhevnikov, 29.01.2018. </remarks>
        ///
        /// <param name="state">        The state. </param>
        /// <param name="nextState">    State of the next. </param>
        ///
        /// <returns>   An ALMStateConfiguration&lt;TEnumState&gt; </returns>

        public static ALMConf<TEnumState> New(TEnumState state, IEnumerable<TEnumState> nextState)
        {
            return new ALMConf<TEnumState>(state, nextState);
        }
    }
}
