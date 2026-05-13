using Event___Ticketing_Management_System.Configurations;
using Event___Ticketing_Management_System.Interfaces.Repositories;
using Event___Ticketing_Management_System.Models.Events;
using MongoDB.Driver;

namespace Event___Ticketing_Management_System.Repositories
{
    public class EventRepository : IEventRepository
    {
        private readonly IMongoCollection<Event> _events;

        public EventRepository(MongoDbService db)
        {
            _events = db.Database.GetCollection<Event>("Events");
        }

        public async Task<List<Event>> GetAllAsync()
        {
            return await _events.Find(_ => true).ToListAsync();
        }

        public async Task<Event?> GetByIdAsync(string id)
        {
            return await _events.Find(x => x.Id == id).FirstOrDefaultAsync();
        }

        public async Task<List<Event>> GetByOrganizerIdAsync(string organizerId)
        {
            return await _events.Find(x => x.OrganizerId == organizerId).ToListAsync();
        }

        public async Task CreateAsync(Event ev)
        {
            await _events.InsertOneAsync(ev);
        }

        public async Task UpdateAsync(Event ev)
        {
            await _events.ReplaceOneAsync(x => x.Id == ev.Id, ev);
        }

        public async Task DeleteAsync(string id)
        {
            await _events.DeleteOneAsync(x => x.Id == id);
        }

        public async Task<bool> DeductTicketQuantityAsync(string eventId, string ticketTypeId, int quantity)
        {
            var filter = Builders<Event>.Filter.And(
                Builders<Event>.Filter.Eq(e => e.Id, eventId),
                Builders<Event>.Filter.ElemMatch(e => e.TicketTypes,
                    t => t.Id == ticketTypeId && (t.Quantity - t.Sold) >= quantity)
            );

            var update = Builders<Event>.Update
                .Inc("TicketTypes.$.Sold", quantity);

            var result = await _events.UpdateOneAsync(filter, update);
            return result.ModifiedCount > 0;
        }
    }
}
