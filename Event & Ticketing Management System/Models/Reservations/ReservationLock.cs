using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace Event___Ticketing_Management_System.Models.Reservations
{
    public class ReservationLock
    {

        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; } = string.Empty;

        [BsonRepresentation(BsonType.ObjectId)]
        public string EventId { get; set; } = string.Empty;

        public string TicketTypeId { get; set; } = string.Empty;

        //Total locked tickets
        public int LockedQuantity { get; set; }

        public DateTime ExpiresAt { get; set; }

        public string UserId { get; set; } = string.Empty;
    }

}

