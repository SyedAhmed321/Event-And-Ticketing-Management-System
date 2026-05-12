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
            return await _users.Find(x => x.Email == email).FirstOrDefaultAsync();
        }

        public async Task<User?> GetUserByIdAsync(string id)
        {
            return await _users.Find(x => x.Id == id).FirstOrDefaultAsync();
        }

        public async Task CreateUserAsync(User user)
        {
            await _users.InsertOneAsync(user);
        }
    }
}
