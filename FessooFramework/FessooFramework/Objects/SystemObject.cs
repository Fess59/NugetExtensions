using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FessooFramework.Objects
{
    public abstract class SystemObject : BaseObject
    {
        /// <summary>   Defaults this object.
        ///             Приводит данный объект к конфигурации по умолчанию </summary>
        ///
        /// <remarks>   AM Kozhevnikov, 22.01.2018. </remarks>
        public abstract void Default();
        //public virtual void Initialization()
        //{
        //    Def
        //}
    }
}
