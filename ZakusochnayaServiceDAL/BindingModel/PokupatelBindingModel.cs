using System.Runtime.Serialization;

namespace ZakusochnayaServiceDAL.BindingModel
{
    [DataContract]
    public class PokupatelBindingModel
    {
        [DataMember]
        public int Id { get; set; }
        [DataMember]
        public string Mail { get; set; }
        [DataMember]
        public string PokupatelFIO { get; set; }
    }
}
