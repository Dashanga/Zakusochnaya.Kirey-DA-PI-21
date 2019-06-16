using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ZakusochnayaModel
{
    /// <summary>
    /// Клиент магазина
    /// </summary>
    public class Pokupatel
    {
        public int Id { get; set; }
        [Required]
        public string PokupatelFIO { get; set; }
        [ForeignKey("PokupatelId")]
        public virtual List<Zakaz> Zakazs { get; set; }
    }
}
