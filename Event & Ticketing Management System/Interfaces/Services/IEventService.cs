using Event___Ticketing_Management_System.DTOs.Events;

namespace Event___Ticketing_Management_System.Interfaces.Services
{
    public interface IEventService
    {
        Task<List<EventResponseDto>> GetAllAsync();
        Task<EventResponseDto?> GetByIdAsync(string id);
        Task<List<EventResponseDto>> GetByOrganizerIdAsync(string organizerId);

        Task<string> CreateAsync(CreateEventDto dto);
        Task<string> UpdateAsync(string id, UpdateEventDto dto);
        Task<string> DeleteAsync(string id);
    }
}
