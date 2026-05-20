using Event___Ticketing_Management_System.Models.PersonalEvents;

namespace Event___Ticketing_Management_System.Interfaces.Repositories
{
    public interface IPersonalEventRepository
    {
        // ✅ Create event
        Task CreateAsync(PersonalEvent personalEvent);

        // ✅ Get by ID
        Task<PersonalEvent?> GetByIdAsync(string eventId);

        // ✅ Get user events
        Task<List<PersonalEvent>> GetByUserIdAsync(string userId);

        // ✅ Get organizer events (optional but important)
        Task<List<PersonalEvent>> GetByOrganizerIdAsync(string organizerId);

        // ✅ Update full event
        Task UpdateAsync(PersonalEvent personalEvent);

        // ✅ Delete event
        Task DeleteAsync(string eventId);
    }
}
