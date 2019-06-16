using System.Runtime.Serialization;

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
