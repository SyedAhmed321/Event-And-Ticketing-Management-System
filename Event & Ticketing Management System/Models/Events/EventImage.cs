using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace Event___Ticketing_Management_System.Models.Events
{
    public class EventImage
    {

        [BsonRepresentation(BsonType.ObjectId)]
        public string EventId { get; set; } = string.Empty;

        public string ImageUrl { get; set; } = string.Empty;

        public bool IsPrimary { get; set; } = false;

    }
}
