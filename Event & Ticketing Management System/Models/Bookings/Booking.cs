using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace Event___Ticketing_Management_System.Models.Bookings
{
    public class Booking
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; } = string.Empty;

        [BsonRepresentation(BsonType.ObjectId)]
        public string UserId { get; set; } = string.Empty;

        [BsonRepresentation(BsonType.ObjectId)]
        public string EventId { get; set; } = string.Empty;

        public string EventTitle { get; set; } = string.Empty;
        public string UserName { get; set; } = string.Empty;

        public List<BookingItem> Items { get; set; } = new();

        public decimal TotalAmount { get; set; }

        public string BookingStatus { get; set; } = "Confirmed"; // Confirmed | Cancelled

        public DateTime BookingDate { get; set; } = DateTime.UtcNow;
    }
}