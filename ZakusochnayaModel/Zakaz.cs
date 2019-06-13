using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ZakusochnayaModel
{
    /// <summary>
    /// Заказ клиента
    /// </summary>
    public class Zakaz
    {
        public int Id { get; set; }
        public int PokupatelId { get; set; }
        public int OutputId { get; set; }
        public int Number { get; set; }
        public decimal Summa { get; set; }
        public ZakazStatus Status { get; set; }
        public DateTime DateCreate { get; set; }
        public DateTime? DateImplement { get; set; }
    }
}
