using System;

namespace ZakusochnayaModel
{
    public class MessageInfo
    {
        public int Id { get; set; }
        public string MessageId { get; set; }
        public string FromMailAddress { get; set; }
        public string Subject { get; set; }
        public string Body { get; set; }
        public DateTime DateDelivery { get; set; }
        public int? PokupatelId { get; set; }
        public virtual Pokupatel Pokupatel { get; set; }
    }
}
