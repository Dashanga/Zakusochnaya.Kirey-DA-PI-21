using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace ZakusochnayaServiceDAL.ViewModel
{
    [DataContract]
    public class SkladsLoadViewModel
    {
        [DataMember]
        public string SkladName { get; set; }
        [DataMember]
        public int FullCount { get; set; }
        [DataMember]
        public IEnumerable<Tuple<string, int>> Elements { get; set; }
    }
}
