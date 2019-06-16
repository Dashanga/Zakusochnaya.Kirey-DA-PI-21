using System;
using System.Collections.Generic;

namespace ZakusochnayaServiceDAL.BindingModels
{
    public class OutputBindingModel
    {
        public int Id { get; set; }
        public string OutputName { get; set; }
        public decimal Cost { get; set; }
        public List<OutputElementBindingModel> OutputElements { get; set; }
    }
}
