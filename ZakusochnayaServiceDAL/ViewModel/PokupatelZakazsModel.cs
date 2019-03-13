using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ZakusochnayaServiceDAL.ViewModel
{
    public class PokupatelZakazsModel
    {
        public string PokupatelName { get; set; }
        public string DateCreate { get; set; }
        public string OutputName { get; set; }
        public int Number { get; set; }
        public decimal Summa { get; set; }
        public string Status { get; set; }
    }
}
