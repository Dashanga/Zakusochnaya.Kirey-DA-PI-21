using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ZakusochnayaServiceDAL.BindingModel
{
    public class SkladElementBindingModel
    {
        public int Id { get; set; }
        public int SkladId { get; set; }
        public int ElementId { get; set; }
        public int Number { get; set; }
    }
}
