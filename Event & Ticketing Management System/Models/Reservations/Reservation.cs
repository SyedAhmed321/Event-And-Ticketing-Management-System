using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace Event___Ticketing_Management_System.Models.Reservations
{
    public class Reservation
    {

        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; } = string.Empty;

        [BsonRepresentation(BsonType.ObjectId)]
        public string UserId { get; set; } = string.Empty;

        [BsonRepresentation(BsonType.ObjectId)]
        public string EventId { get; set; } = string.Empty;

        public List<ReservationItem> Items { get; set; } = new();

        // ✅ Expiry time (VERY IMPORTANT)
        public DateTime ExpiresAt { get; set; }

        public string Status { get; set; } = "Active";
        // Active, Expired, Confirmed

        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

    }

    public class ReservationItem
    {  
        public string TicketTypeId { get; set; } = string.Empty;

        public string TicketTypeName { get; set; } = string.Empty;
        public int Quantity { get; set; }
    }
}
