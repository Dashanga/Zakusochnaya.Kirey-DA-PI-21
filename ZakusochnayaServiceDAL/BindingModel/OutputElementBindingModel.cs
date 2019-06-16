using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ZakusochnayaServiceDAL.BindingModels
{
    public class OutputElementBindingModel
    {
        public int Id { get; set; }
        public int OutputId { get; set; }
        public int ElementId { get; set; }
        public int Number { get; set; }
    }
}
