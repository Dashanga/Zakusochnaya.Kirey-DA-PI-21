using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ZakusochnayaModel
{
    /// <summary>
    /// Компонент, требуемый для изготовления изделия
    /// </summary>
    public class Element
    {
        public int Id { get; set; }
        [Required]
        public string ElementName { get; set; }
        public virtual List<OutputElement> OutputElements { get; set; }
        public virtual List<SkladElement> SkladElements { get; set; }
    }
}
