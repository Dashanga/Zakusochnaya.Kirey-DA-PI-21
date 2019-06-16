using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ZakusochnayaModel
{
    /// <summary>
    /// Изделие, изготавливаемое в магазине
    /// </summary>
    public class Output
    {
        public int Id { get; set; }
        [Required]
        public string OutputName { get; set; }
        [Required]
        public decimal Cost { get; set; }
        public virtual List<Zakaz> Zakazs { get; set; }
        
    }
}
