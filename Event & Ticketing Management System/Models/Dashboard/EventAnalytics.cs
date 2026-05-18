namespace Event___Ticketing_Management_System.Models.Dashboard
{
    public class EventAnalytics
    {
        public string EventId { get; set; } = string.Empty;

        public string EventTitle { get; set; } = string.Empty;

        public int TicketsSold { get; set; }

        public decimal Revenue { get; set; }
    }
}
