using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace ZakusochnayaServiceDAL.ViewModels
{
    [DataContract]
    public class OutputViewModel
    {
        [DataMember]
        public int Id { get; set; }
        [DataMember]
        public string OutputName { get; set; }
        [DataMember]
        public decimal Cost { get; set; }
        [DataMember]
        public List<OutputElementViewModel> OutputElements { get; set; }
    }
}
