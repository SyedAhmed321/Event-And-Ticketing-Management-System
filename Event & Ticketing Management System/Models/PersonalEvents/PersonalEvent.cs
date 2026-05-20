using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace Event___Ticketing_Management_System.Models.PersonalEvents
{
    public class PersonalEvent
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; } = string.Empty;

        [BsonRepresentation(BsonType.ObjectId)]
        public string UserId { get; set; } = string.Empty;

        [BsonRepresentation(BsonType.ObjectId)]
        public string? OrganizerId { get; set; } // optional

        public string Title { get; set; } = string.Empty;

        public string EventType { get; set; } = string.Empty;
        // Birthday / Wedding / etc.

        public string Location { get; set; } = string.Empty;

        public DateTime EventDate { get; set; }

        public int GuestCount { get; set; }

        public string Status { get; set; } = "Planning";
        // Planning, Confirmed, Completed

        public List<EventRequirement> Requirements { get; set; } = new();

        public PlanningBudget Budget { get; set; } = new();

        public List<EventTimeline> Timeline { get; set; } = new();

        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    }
}