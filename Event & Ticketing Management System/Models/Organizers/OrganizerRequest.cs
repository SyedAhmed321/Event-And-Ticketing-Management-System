using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace Event___Ticketing_Management_System.Models.Organizers
{
    public class OrganizerRequest
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; } = string.Empty;

        [BsonRepresentation(BsonType.ObjectId)]
        public string UserId { get; set; } = string.Empty;

        [BsonRepresentation(BsonType.ObjectId)]
        public string OrganizerId { get; set; } = string.Empty;

        public string EventType { get; set; } = string.Empty;
        // Birthday, Wedding, Corporate

        public string Description { get; set; } = string.Empty;

        public string Location { get; set; } = string.Empty;

        public DateTime EventDate { get; set; }

        public int GuestCount { get; set; }

        public decimal Budget { get; set; }

        public string Status { get; set; } = "Pending";
        // Pending, Approved, Rejected, Completed

        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    }
}