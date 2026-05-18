using Event___Ticketing_Management_System.DTOs.PersonalEvents;

namespace Event___Ticketing_Management_System.Interfaces.Services
{
    public interface IPersonalEventService
    {
        //Create event
        Task<string> CreateEventAsync(string userId, CreatePersonalEventDto dto);

        //Get events
        Task<List<PersonalEventResponseDto>> GetMyEventsAsync(string userId);

        Task<PersonalEventResponseDto?> GetByIdAsync(string eventId);

        Task<List<PersonalEventResponseDto>> GetByOrganizerAsync(string organizerId);

        //Update event
        Task<string> UpdateEventAsync(string userId, string eventId, UpdatePersonalEventDto dto);

        //Requirements
        Task<string> AddRequirementAsync(string userId, string eventId, CreateRequirementDto dto);

        //Timeline
        Task<string> AddTimelineAsync(string userId, string eventId, AddTimelineDto dto);

        Task<string> UpdateTimelineAsync(string userId, string eventId, UpdateTimelineDto dto);

        //Budget
        Task<string> UpdateBudgetAsync(string userId, string eventId, UpdateBudgetDto dto);

        //Delete event
        Task<string> DeleteEventAsync(string userId, string eventId);
    }
}