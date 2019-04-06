using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace ZakusochnayaServiceDAL.ViewModel
{
    [DataContract]
    public class PokupatelViewModel
    {
        [DataMember]
        public int Id { get; set; }
        [DataMember]
        public string PokupatelFIO { get; set; }
    }
}
