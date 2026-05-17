namespace Event___Ticketing_Management_System.DTOs.Events
{
    public class TicketTypeDto
    {
        public required string Id { get; set; }
        public required string Name { get; set; }
        public decimal Price { get; set; }
        public int Quantity { get; set; }
        public DateTime SalesStart { get; set; }
        public DateTime SalesEnd { get; set; }
    }
}
