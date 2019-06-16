using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ZakusochnayaModel
{
    public class SkladElement
    {
        public int Id { get; set; }
        public int SkladId { get; set; }
        public int ElementId { get; set; }
        public int Number { get; set; }
        public virtual Element Element { get; set; }
        public virtual Sklad Sklad { get; set; }
    }
}
