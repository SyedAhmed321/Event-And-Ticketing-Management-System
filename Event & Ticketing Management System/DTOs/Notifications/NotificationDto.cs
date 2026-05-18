namespace Event___Ticketing_Management_System.DTOs.Notifications
{
    public class NotificationResponseDto
    {
        public string Id { get; set; } = string.Empty;

        public string Title { get; set; } = string.Empty;

        public string Message { get; set; } = string.Empty;

        public string Type { get; set; } = string.Empty;

        public bool IsRead { get; set; }

        public DateTime CreatedAt { get; set; }
    }


    public class MarkAsReadDto
    {
        public string NotificationId { get; set; } = string.Empty;
    }

}