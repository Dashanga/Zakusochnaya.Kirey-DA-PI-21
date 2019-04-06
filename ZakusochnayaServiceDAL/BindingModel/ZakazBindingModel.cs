using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.Serialization;

namespace ZakusochnayaServiceDAL.BindingModels
{
    [DataContract]
    public class ZakazBindingModel
    {
        [DataMember]
        public int Id { get; set; }
        [DataMember]
        public int PokupatelId { get; set; }
        [DataMember]
        public int OutputId { get; set; }
        [DataMember]
        public int Number { get; set; }
        [DataMember]
        public decimal Summa { get; set; }
    }
}
