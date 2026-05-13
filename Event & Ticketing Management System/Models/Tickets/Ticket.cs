using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
namespace Event___Ticketing_Management_System.Models.Tickets
{
    public class Ticket
    {

        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; } = string.Empty;

        [BsonRepresentation(BsonType.ObjectId)]
        public string BookingId { get; set; } = string.Empty;

        [BsonRepresentation(BsonType.ObjectId)]
        public string EventId { get; set; } = string.Empty;

        [BsonRepresentation(BsonType.ObjectId)]
        public string UserId { get; set; } = string.Empty;

        public string TicketTypeId { get; set; } = string.Empty;
        public string TicketTypeName { get; set; } = string.Empty;
        public string EventTitle { get; set; } = string.Empty;

        public string QRCode { get; set; } = string.Empty; // base64 QR image

        public string Status { get; set; } = "Active"; // Active | Used | Cancelled | Expired

        public bool CheckedIn { get; set; } = false;

        public DateTime IssuedAt { get; set; } = DateTime.UtcNow;
    }
}
