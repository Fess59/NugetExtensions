using FessooFramework.Objects.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace ExampleDataServiceModels
{
    [DataContract(IsReference = false)]
    public class ExampleDataModel : CacheObject
    {
        [DataMember]
        public string Description { get; set; }
    }
}
