using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace Event___Ticketing_Management_System.Models.Vendors
{
    public class VendorBooking
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; } = string.Empty;

        [BsonRepresentation(BsonType.ObjectId)]
        public string VendorId { get; set; } = string.Empty;

        [BsonRepresentation(BsonType.ObjectId)]
        public string UserId { get; set; } = string.Empty;

        public string ServiceId { get; set; } = string.Empty;

        public DateTime EventDate { get; set; }

        public decimal TotalAmount { get; set; }

        public string Status { get; set; } = "Pending";
        // Pending, Confirmed, Cancelled

        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    }
}