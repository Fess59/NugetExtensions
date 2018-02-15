using FessooFramework.Tools.Helpers;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace FessooFramework.Objects.Message
{
  
    public class MessageObject : BaseObject
    {
        #region Property
        public string HashUID { get; set; }
        public string SessionUID { get; set; }
        #endregion
    }
}
