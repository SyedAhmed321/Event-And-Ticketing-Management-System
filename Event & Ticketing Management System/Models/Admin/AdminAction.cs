using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace Event___Ticketing_Management_System.Models.Admin
{
    public class AdminAction
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; } = string.Empty;

        [BsonRepresentation(BsonType.ObjectId)]
        public string AdminId { get; set; } = string.Empty;

        public string ActionType { get; set; } = string.Empty;
        // Example: "DeleteEvent", "BanUser", "ApproveOrganizer"

        public string Description { get; set; } = string.Empty;

        public string? TargetId { get; set; }
        // Optional → ID of affected entity

        public string? TargetType { get; set; }
        // User, Event, Booking, Organizer etc.

        public DateTime PerformedAt { get; set; } = DateTime.UtcNow;
    }
}