using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ZakusochnayaServiceDAL.ViewModel
{
    public class SkladsLoadViewModel
    {
        public string SkladName { get; set; }
        public int FullCount { get; set; }
        public IEnumerable<Tuple<string, int>> Elements { get; set; }
    }
}
