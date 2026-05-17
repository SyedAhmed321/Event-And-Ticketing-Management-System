using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace Event___Ticketing_Management_System.Models.Tickets
{
    public class CheckIn
    {

        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; } = string.Empty;

        [BsonRepresentation(BsonType.ObjectId)]
        public string TicketId { get; set; } = string.Empty;

        public DateTime CheckedInAt { get; set; } = DateTime.UtcNow;

        [BsonRepresentation(BsonType.ObjectId)]
        public string CheckedInBy { get; set; } = string.Empty; // staff/admin

    }
}
