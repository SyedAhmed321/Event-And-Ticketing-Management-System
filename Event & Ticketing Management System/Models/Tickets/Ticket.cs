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

        //QR Code unique string
        public string QRCode { get; set; } = string.Empty;

        //Ticket status
        public string Status { get; set; } = TicketStatuses.Active;
        // Active, Used, Cancelled, Expired

        public DateTime IssuedAt { get; set; } = DateTime.UtcNow;

        public bool CheckedIn { get; set; } = false;

    }
}
