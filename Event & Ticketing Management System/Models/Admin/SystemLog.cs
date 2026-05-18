using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace Event___Ticketing_Management_System.Models.Admin
{
    public class SystemLog
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; } = string.Empty;

        public string Level { get; set; } = string.Empty;
        // Info, Warning, Error

        public string Message { get; set; } = string.Empty;

        public string? Exception { get; set; }

        public string? Source { get; set; }
        // Which module (Booking, Event, etc.)

        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    }
}