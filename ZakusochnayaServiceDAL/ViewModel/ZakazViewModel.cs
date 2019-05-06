using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace ZakusochnayaServiceDAL.ViewModels
{
    [DataContract]
    public class ZakazViewModel
    {
        [DataMember]
        public int Id { get; set; }
        [DataMember]
        public int PokupatelId { get; set; }
        [DataMember]
        public string PokupatelFIO { get; set; }
        [DataMember]
        public int OutputId { get; set; }
        [DataMember]
        public string OutputName { get; set; }
        [DataMember]
        public int? ExecutorId { get; set; }
        [DataMember]
        public string ExecutorName { get; set; }
        [DataMember]
        public int Number { get; set; }
        [DataMember]
        public decimal Summa { get; set; }
        [DataMember]
        public string Status { get; set; }
        [DataMember]
        public string DateCreate { get; set; }
        [DataMember]
        public string DateImplement { get; set; }
    }
}
