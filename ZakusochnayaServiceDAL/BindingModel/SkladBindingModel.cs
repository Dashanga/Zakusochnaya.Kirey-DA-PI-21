using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace ZakusochnayaServiceDAL.BindingModel
{
    [DataContract]
    public class SkladBindingModel
    {
        [DataMember]
        public int Id { get; set; }
        [DataMember]
        public string SkladName { get; set; }
    }
}
