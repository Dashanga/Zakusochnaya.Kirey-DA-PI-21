using System;

namespace ZakusochnayaServiceDAL.BindingModel
{
    public class OtchetBindingModel
    {
        public string FileName { get; set; }
        public DateTime? DateFrom { get; set; }
        public DateTime? DateTo { get; set; }
    }
}
