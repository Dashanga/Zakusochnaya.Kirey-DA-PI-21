using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ZakusochnayaServiceDAL.ViewModels
{
    public class OutputElementViewModel
    {
        public int OutputElementId { get; set; }
        public int OutputId { get; set; }
        public int ElementId { get; set; }
        public string ElementName { get; set; }
        public int Number { get; set; }
    }
}
