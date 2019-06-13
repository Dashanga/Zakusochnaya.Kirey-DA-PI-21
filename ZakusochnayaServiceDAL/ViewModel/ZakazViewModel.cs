using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ZakusochnayaServiceDAL.ViewModels
{
    public class ZakazViewModel
    {
        public int ZakazId { get; set; }
        public int PokupatelId { get; set; }
        public string PokupatelFIO { get; set; }
        public int OutputId { get; set; }
        public string OutputName { get; set; }
        public int Number { get; set; }
        public decimal Summa { get; set; }
        public string Status { get; set; }
        public string DateCreate { get; set; }
        public string DateImplement { get; set; }
    }
}
