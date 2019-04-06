using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace ZakusochnayaServiceDAL.ViewModel
{
    [DataContract]
    public class PokupatelZakazsModel
    {
        [DataMember]
        public string PokupatelName { get; set; }
        [DataMember]
        public string DateCreate { get; set; }
        [DataMember]
        public string OutputName { get; set; }
        [DataMember]
        public int Number { get; set; }
        [DataMember]
        public decimal Summa { get; set; }
        [DataMember]
        public string Status { get; set; }
    }
}
