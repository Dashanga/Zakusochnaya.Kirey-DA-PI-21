using System;
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace ZakusochnayaServiceDAL.BindingModels
{
    [DataContract]
    public class OutputBindingModel
    {
        [DataMember]
        public int Id { get; set; }
        [DataMember]
        public string OutputName { get; set; }
        [DataMember]
        public decimal Cost { get; set; }
        [DataMember]
        public List<OutputElementBindingModel> OutputElements { get; set; }
    }
}
