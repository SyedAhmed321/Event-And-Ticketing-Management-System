using Event___Ticketing_Management_System.Configurations;
using Event___Ticketing_Management_System.Interfaces.Repositories;
using Event___Ticketing_Management_System.Models.Users;
using MongoDB.Driver;

namespace Event___Ticketing_Management_System.Repositories
{
    public class UserProfileRepository : IUserProfileRepository
    {
        private readonly IMongoCollection<UserProfile> _profiles;

        public UserProfileRepository(MongoDbService mongoDbService)
        {
            _profiles = mongoDbService.Database.GetCollection<UserProfile>("UserProfiles");
        }

        public async Task<UserProfile?> GetByUserIdAsync(string userId)
        {
            return await _profiles.Find(x => x.UserId == userId).FirstOrDefaultAsync();
        }

        public async Task CreateAsync(UserProfile profile)
        {
            await _profiles.InsertOneAsync(profile);
        }

        public async Task UpdateAsync(string userId, UserProfile profile)
        {
            await _profiles.ReplaceOneAsync(x => x.UserId == userId, profile);
        }
    }
}
