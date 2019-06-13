using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ZakusochnayaServiceDAL.ViewModels
{
    public class OutputViewModel
    {
        public int OutputId { get; set; }
        public string OutputName { get; set; }
        public decimal Cost { get; set; }
        public List<OutputElementViewModel> OutputElements { get; set; }
    }
}
