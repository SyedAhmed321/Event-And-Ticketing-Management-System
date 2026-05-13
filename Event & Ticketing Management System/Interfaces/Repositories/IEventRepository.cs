using Event___Ticketing_Management_System.Models.Events;

namespace Event___Ticketing_Management_System.Interfaces.Repositories
{
    public interface IEventRepository
    {
        Task<List<Event>> GetAllAsync();
        Task<Event?> GetByIdAsync(string id);
        Task<List<Event>> GetByOrganizerIdAsync(string organizerId);

        Task<bool> DeductTicketQuantityAsync(string eventId, string ticketTypeId, int quantity);

        Task CreateAsync(Event ev);
        Task UpdateAsync(Event ev);
        Task DeleteAsync(string id);
    }
}
