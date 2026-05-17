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
            var update = Builders<Event>.Update
                .Set(e => e.isDeleted, true)
                .Set(e => e.Status, "Cancelled");

            await _events.UpdateOneAsync(e => e.Id == id, update);

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


        public async Task<List<Event>> GetFilteredEventsAsync(
                    string? category,
                    string? city,
                    DateTime? date,
                    decimal? minPrice,
                    decimal? maxPrice,
                    string? searchTerm)
        {
            var filter = Builders<Event>.Filter.Empty;

            //Category
            if (!string.IsNullOrEmpty(category))
                filter &= Builders<Event>.Filter.Eq(e => e.Category.ToString(), category);

            //City
            if (!string.IsNullOrEmpty(city))
                filter &= Builders<Event>.Filter.Eq(e => e.City, city);

            //Date filter
            if (date.HasValue)
            {
                filter &= Builders<Event>.Filter.Lte(e => e.StartDate, date.Value) &
                          Builders<Event>.Filter.Gte(e => e.EndDate, date.Value);
            }

            //Search term
            if (!string.IsNullOrEmpty(searchTerm))
            {
                filter &= Builders<Event>.Filter.Or(
                    Builders<Event>.Filter.Regex(e => e.Title, searchTerm),
                    Builders<Event>.Filter.Regex(e => e.Description, searchTerm)
                );
            }

            //Base conditions (always applied)
            filter &= Builders<Event>.Filter.Eq(e => e.isDeleted, false);
            filter &= Builders<Event>.Filter.Eq(e => e.Status, "Published");

            var events = await _events.Find(filter).ToListAsync();

            //Price filtering (done in memory for nested ticketTypes)
            if (minPrice.HasValue || maxPrice.HasValue)
            {
                events = events
                    .Where(e =>
                        e.TicketTypes.Any(t =>
                            (!minPrice.HasValue || t.Price >= minPrice) &&
                            (!maxPrice.HasValue || t.Price <= maxPrice)
                        ))
                    .ToList();
            }

            return events;
        }

    }
}
