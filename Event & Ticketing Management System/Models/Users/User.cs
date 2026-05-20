using Event___Ticketing_Management_System.Utilities;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace Event___Ticketing_Management_System.Models.Users
{
    public class User
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; } = string.Empty;

        [BsonElement("fullName")]
        public string FullName { get; set; } = string.Empty;

        [BsonElement("email")]
        public string Email { get; set; } = string.Empty;

        [BsonElement("passwordHash")]
        public string PasswordHash { get; set; } = string.Empty;

        [BsonElement("role")]
        public string Role { get; set; } = string.Empty;

        [BsonElement("phone")]
        public string Phone {get; set; } = string.Empty;

        [BsonElement("profileImage")]
        public string ProfileImage { get; set; } = string.Empty;

        [BsonElement("isVerified")]
        public bool IsVerified { get; set; } = false;

        [BsonIgnoreIfNull]
        [BsonElement("IsSuspended")]
        public bool IsSuspended { get; set; } = false;

        [BsonElement("createdAt")]
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

    }
}
