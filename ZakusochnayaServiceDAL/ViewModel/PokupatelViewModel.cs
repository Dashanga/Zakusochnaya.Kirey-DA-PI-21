using System.Collections.Generic;
using System.Runtime.Serialization;

namespace ZakusochnayaServiceDAL.ViewModel
{
    [DataContract]
    public class PokupatelViewModel
    {
        [DataMember]
        public int Id { get; set; }
        [DataMember]
        public string Mail { get; set; }
        [DataMember]
        public string PokupatelFIO { get; set; }
        [DataMember]
        public List<MessageInfoViewModel> Messages { get; set; }
    }
}
