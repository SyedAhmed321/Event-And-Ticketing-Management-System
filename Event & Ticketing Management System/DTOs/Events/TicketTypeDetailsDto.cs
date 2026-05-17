namespace Event___Ticketing_Management_System.DTOs.Events
{
    public class TicketTypeDetailsDto
    {
        public required string Id { get; set; }

        public required string Name { get; set; }

        public decimal Price { get; set; }

        public int Quantity { get; set; }

        public int Sold { get; set; }

        public int Available { get; set; }

    }
}
