using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ZakusochnayaServiceDAL.ViewModel
{
    public class SkladViewModel
    {
        public int Id { get; set; }
        public string SkladName { get; set; }
        public List<SkladElementViewModel> SkladElements { get; set; }
    }
}
