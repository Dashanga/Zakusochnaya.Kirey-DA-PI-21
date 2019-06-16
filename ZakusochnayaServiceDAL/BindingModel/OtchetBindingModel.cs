using System;
using System.Runtime.Serialization;

namespace ZakusochnayaServiceDAL.BindingModel
{
    [DataContract]
    public class OtchetBindingModel
    {
        [DataMember]
        public string FileName { get; set; }
        [DataMember]
        public DateTime? DateFrom { get; set; }
        [DataMember]
        public DateTime? DateTo { get; set; }
    }
}
