using MongoDB.Bson.Serialization.Attributes;

namespace Event___Ticketing_Management_System.Models.Events
{
    public class TicketType
    {
        public string Id { get; set; } = Guid.NewGuid().ToString();

        public string Name { get; set; } = string.Empty;

        public decimal Price { get; set; }

        public int Quantity { get; set; }

        public int Sold { get; set; } = 0;

        public DateTime SalesStart { get; set; }
        public DateTime SalesEnd { get; set; }
    }
}
