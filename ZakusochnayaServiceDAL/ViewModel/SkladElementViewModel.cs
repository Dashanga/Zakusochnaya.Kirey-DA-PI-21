using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ZakusochnayaServiceDAL.ViewModel
{
    public class SkladElementViewModel
    {
        public int Id { get; set; }
        public int SkladId { get; set; }
        public int ElementId { get; set; }
        public string ElementName { get; set; }
        public int Number { get; set; }
    }
}
