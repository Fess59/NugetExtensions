using FessooFramework.Objects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Example.Tests.Logger
{
    /// <summary>   A logger module. </summary>
    ///
    /// <remarks>   Fess59, 26.01.2018. </remarks>

    public class LoggerModule : ModuleObject
    {
        public override string _Name => this.GetType().ToString();

        public override void _Compliting()
        {
            throw new NotImplementedException();
        }

        public override void _Configuring()
        {
            throw new NotImplementedException();
        }

        public override void _Launching()
        {
            throw new NotImplementedException();
        }

        public override void _Loading()
        {
            throw new NotImplementedException();
        }
    }
}
