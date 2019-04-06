using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;


namespace ZakusochnayaServiceDAL.BindingModel
{
    [DataContract]
    public class SkladElementBindingModel
    {
        [DataMember]
        public int Id { get; set; }
        [DataMember]
        public int SkladId { get; set; }
        [DataMember]
        public int ElementId { get; set; }
        [DataMember]
        public int Number { get; set; }
    }
}
