using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ZakusochnayaModel
{
    public class Sklad
    {
        public int Id { get; set; }
        [Required]
        public string SkladName { get; set; }
        public virtual List<SkladElement> SkladElements { get; set; }
    }
}
