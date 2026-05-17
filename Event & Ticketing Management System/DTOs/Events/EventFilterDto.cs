namespace Event___Ticketing_Management_System.DTOs.Events
{
    public class EventFilterDto
    {
        public string? Category { get; set; }

        public string? City { get; set; }

        public DateTime? Date { get; set; }

        public decimal? MinPrice { get; set; }

        public decimal? MaxPrice { get; set; }

        public string? SearchTerm { get; set; }

    }
}
