using Event___Ticketing_Management_System.Configurations;
using Event___Ticketing_Management_System.Interfaces.Repositories;
using Event___Ticketing_Management_System.Models.Organizers;
using MongoDB.Driver;

namespace Event___Ticketing_Management_System.Repositories
{
    public class OrganizerRepository : IOrganizerRepository
    {
        private readonly IMongoCollection<Organizer> _organizers;
        private readonly IMongoCollection<OrganizerRequest> _requests;

        public OrganizerRepository(MongoDbService mongoDbService)
        {
            _organizers = mongoDbService.Database.GetCollection<Organizer>("Organizers");
            _requests = mongoDbService.Database.GetCollection<OrganizerRequest>("OrganizerRequests");
        }

        // =========================
        //ORGANIZER METHODS
        // =========================

        public async Task CreateAsync(Organizer organizer)
        {
            await _organizers.InsertOneAsync(organizer);
        }

        public async Task<Organizer?> GetByIdAsync(string organizerId)
        {
            return await _organizers
                .Find(o => o.Id == organizerId)
                .FirstOrDefaultAsync();
        }

        public async Task<Organizer?> GetByUserIdAsync(string userId)
        {
            return await _organizers
                .Find(o => o.UserId == userId)
                .FirstOrDefaultAsync();
        }

        public async Task<List<Organizer>> GetAllAsync()
        {
            return await _organizers
                .Find(_ => true)
                .ToListAsync();
        }

        public async Task UpdateAsync(Organizer organizer)
        {
            await _organizers.ReplaceOneAsync(
                o => o.Id == organizer.Id,
                organizer
            );
        }

        public async Task DeleteAsync(string organizerId)
        {
            await _organizers.DeleteOneAsync(o => o.Id == organizerId);
        }

        // =========================
        //ORGANIZER REQUESTS
        // =========================

        public async Task CreateRequestAsync(OrganizerRequest request)
        {
            await _requests.InsertOneAsync(request);
        }

        public async Task<OrganizerRequest?> GetRequestByIdAsync(string requestId)
        {
            return await _requests
                .Find(r => r.Id == requestId)
                .FirstOrDefaultAsync();
        }

        public async Task<List<OrganizerRequest>> GetRequestsByOrganizerIdAsync(string organizerId)
        {
            return await _requests
                .Find(r => r.OrganizerId == organizerId)
                .SortByDescending(r => r.CreatedAt)
                .ToListAsync();
        }

        public async Task<List<OrganizerRequest>> GetRequestsByUserIdAsync(string userId)
        {
            return await _requests
                .Find(r => r.UserId == userId)
                .SortByDescending(r => r.CreatedAt)
                .ToListAsync();
        }

        public async Task UpdateRequestAsync(OrganizerRequest request)
        {
            await _requests.ReplaceOneAsync(
                r => r.Id == request.Id,
                request
            );
        }
    }
}