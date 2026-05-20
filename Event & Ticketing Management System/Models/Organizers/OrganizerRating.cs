using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson;

namespace Event___Ticketing_Management_System.Models.Organizers
{
    public class OrganizerRating
    {
        [BsonRepresentation(BsonType.ObjectId)]
        public string UserId { get; set; } = string.Empty;

        public int Rating { get; set; } // 1–5

        public string Comment { get; set; } = string.Empty;

        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    }
}