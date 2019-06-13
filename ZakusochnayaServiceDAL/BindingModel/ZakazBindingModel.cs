using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ZakusochnayaServiceDAL.BindingModels
{
    public class ZakazBindingModel
    {
        public int Id { get; set; }
        public int PokupatelId { get; set; }
        public int OutputId { get; set; }
        public int Number { get; set; }
        public decimal Summa { get; set; }
    }
}
