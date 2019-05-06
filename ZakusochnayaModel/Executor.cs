using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ZakusochnayaModel
{
    public class Executor
    {
        public int Id { get; set; }
        [Required]
        public string ExecutorFIO { get; set; }
        [ForeignKey("ExecutorId")]
        public virtual List<Zakaz> Zakazs { get; set; }
    }
}
