using Event___Ticketing_Management_System.Configurations;
using Event___Ticketing_Management_System.Interfaces.Repositories;
using Event___Ticketing_Management_System.Models.PersonalEvents;
using MongoDB.Driver;

namespace Event___Ticketing_Management_System.Repositories
{
    public class PersonalEventRepository : IPersonalEventRepository
    {
        private readonly IMongoCollection<PersonalEvent> _events;

        public PersonalEventRepository(MongoDbService mongoDbService)
        {
            _events = mongoDbService.Database.GetCollection<PersonalEvent>("PersonalEvents");
        }

        // ✅ Create
        public async Task CreateAsync(PersonalEvent personalEvent)
        {
            await _events.InsertOneAsync(personalEvent);
        }

        // ✅ Get by ID
        public async Task<PersonalEvent?> GetByIdAsync(string eventId)
        {
            return await _events
                .Find(e => e.Id == eventId)
                .FirstOrDefaultAsync();
        }

        // ✅ Get by User
        public async Task<List<PersonalEvent>> GetByUserIdAsync(string userId)
        {
            return await _events
                .Find(e => e.UserId == userId)
                .SortByDescending(e => e.CreatedAt)
                .ToListAsync();
        }

        // ✅ Get by Organizer
        public async Task<List<PersonalEvent>> GetByOrganizerIdAsync(string organizerId)
        {
            return await _events
                .Find(e => e.OrganizerId == organizerId)
                .SortByDescending(e => e.CreatedAt)
                .ToListAsync();
        }

        // ✅ Update (FULL REPLACE)
        public async Task UpdateAsync(PersonalEvent personalEvent)
        {
            await _events.ReplaceOneAsync(
                e => e.Id == personalEvent.Id,
                personalEvent
            );
        }

        // ✅ Delete
        public async Task DeleteAsync(string eventId)
        {
            await _events.DeleteOneAsync(e => e.Id == eventId);
        }
    }
}
