namespace Event___Ticketing_Management_System.Models.Tickets
{
    public class TicketValidation
    {

        public bool IsValid { get; set; }

        public string Message { get; set; } = string.Empty;

        public string TicketId { get; set; } = string.Empty;

        public string Status { get; set; } = TicketStatuses.Active;

    }
}
