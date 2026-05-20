using Event___Ticketing_Management_System.Models.Organizers;

namespace Event___Ticketing_Management_System.Interfaces.Repositories
{
    public interface IOrganizerRepository
    {
        //Organizer profile
        Task CreateAsync(Organizer organizer);

        Task<Organizer?> GetByIdAsync(string organizerId);

        Task<Organizer?> GetByUserIdAsync(string userId);

        Task<List<Organizer>> GetAllAsync();

        Task UpdateAsync(Organizer organizer);

        Task DeleteAsync(string organizerId);

        //Organizer Requests

        Task CreateRequestAsync(OrganizerRequest request);

        Task<OrganizerRequest?> GetRequestByIdAsync(string requestId);

        Task<List<OrganizerRequest>> GetRequestsByOrganizerIdAsync(string organizerId);

        Task<List<OrganizerRequest>> GetRequestsByUserIdAsync(string userId);

        Task UpdateRequestAsync(OrganizerRequest request);
    }
}
