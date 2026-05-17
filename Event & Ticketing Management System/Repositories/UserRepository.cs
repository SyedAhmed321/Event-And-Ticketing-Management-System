using Event___Ticketing_Management_System.Interfaces.Repositories;
using Event___Ticketing_Management_System.Configurations;
using Event___Ticketing_Management_System.Models.Users;
using MongoDB.Driver;

namespace Event___Ticketing_Management_System.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly IMongoCollection<User> _users;

        public UserRepository(MongoDbService mongoDbService)
        {
            _users = mongoDbService.Database.GetCollection<User>("Users");
        }

        public async Task<User?> GetUserByEmailAsync(string email)
        {
            var normalizedEmail = email.ToLowerInvariant();
            return await _users.Find(x => x.Email.ToLower() == normalizedEmail).FirstOrDefaultAsync();
        }

        public async Task<User?> GetUserByIdAsync(string id)
        {
            return await _users.Find(x => x.Id == id).FirstOrDefaultAsync();
        }

        public async Task CreateUserAsync(User user)
        {
            await _users.InsertOneAsync(user);
        }

        public async Task<bool> EmailExistsAsync(string email)
        {
            var normalizedEmail = email.ToLowerInvariant();
            return await _users.Find(x => x.Email.ToLower() == normalizedEmail).AnyAsync();
        }


        public async Task UpdateUserAsync(User user)
        {
            await _users.ReplaceOneAsync(x => x.Id == user.Id, user);
        }

        
    }
}
