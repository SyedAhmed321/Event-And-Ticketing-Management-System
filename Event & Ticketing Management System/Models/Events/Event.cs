using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace Event___Ticketing_Management_System.Models.Events
{
    public class Event
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; } = null!;

        [BsonRepresentation(BsonType.ObjectId)]
        public string OrganizerId { get; set; } = string.Empty;
        public string Title { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public string  Category { get; set; } = string.Empty;
        public string Venue { get; set; } = string.Empty;
        public string City { get; set; } = string.Empty;
        public string BannerImage { get; set; } = string.Empty;
        public List<string> Images { get; set; } = new();
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public string Status { get; set; } = "Draft";
        public List<TicketType> TicketTypes { get; set; } = new();
        public int TotalTicketsSold { get; set; } = 0;
        public bool isDeleted { get; set; } = false;
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    }
}
