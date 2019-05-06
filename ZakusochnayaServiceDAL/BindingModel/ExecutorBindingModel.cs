using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace ZakusochnayaServiceDAL.BindingModel
{
    [DataContract]
    public class ExecutorBindingModel
    {
        [DataMember]
        public int Id { get; set; }
        [DataMember]
        public string ExecutorFIO { get; set; }
    }
}
