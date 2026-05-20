using Event___Ticketing_Management_System.DTOs.Organizers;

namespace Event___Ticketing_Management_System.Interfaces.Services
{
    public interface IOrganizerService
    {
        //Organizer profile
        Task<string> CreateOrganizerAsync(string userId);

        Task<OrganizerResponseDto?> GetMyOrganizerAsync(string userId);

        Task<string> UpdateOrganizerAsync(string userId, UpdateOrganizerDto dto);

        Task<List<OrganizerResponseDto>> GetAllOrganizersAsync();

        //Organizer Requests (🔥 MAIN FEATURE)
        Task<string> CreateRequestAsync(string userId, OrganizerRequestDto dto);

        Task<List<OrganizerRequestResponseDto>> GetMyRequestsAsync(string userId);

        Task<List<OrganizerRequestResponseDto>> GetRequestsForOrganizerAsync(string userId);

        Task<string> UpdateRequestStatusAsync(string organizerUserId, UpdateOrganizerRequestStatusDto dto);

        //Ratings
        Task<string> AddRatingAsync(string userId, OrganizerRatingDto dto);
    }
}