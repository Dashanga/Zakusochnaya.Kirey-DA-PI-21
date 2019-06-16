using System;

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
        public int? ExecutorId { get; set; }
        public int Number { get; set; }
        public decimal Summa { get; set; }
        public ZakazStatus Status { get; set; }
        public DateTime DateCreate { get; set; }
        public DateTime? DateImplement { get; set; }
        public virtual Pokupatel Pokupatel { get; set; }
        public virtual Output Output { get; set; }
        public virtual Executor Executor { get; set; }
    }
}
