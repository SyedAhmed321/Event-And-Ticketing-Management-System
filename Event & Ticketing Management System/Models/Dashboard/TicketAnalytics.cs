namespace Event___Ticketing_Management_System.Models.Dashboard
{
    public class TicketAnalytics
    {
        public int TotalTickets { get; set; }

        public int TicketsSold { get; set; }

        public int TicketsAvailable { get; set; }

        public List<TicketTypeAnalytics> TicketTypes { get; set; } = new();
    }

    public class TicketTypeAnalytics
    {
        public string Name { get; set; } = string.Empty;

        public int Sold { get; set; }

        public int Total { get; set; }
    }
}
