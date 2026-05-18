using Event___Ticketing_Management_System.DTOs.Admin;

namespace Event___Ticketing_Management_System.Interfaces.Services
{
    public interface IAdminService
    {
        //User management
        Task<string> SuspendUserAsync(string adminId, SuspendUserDto dto);

        //Vendor verification
        Task<string> VerifyVendorAsync(string adminId, VerifyVendorDto dto);

        //Organizer verification
        Task<string> VerifyOrganizerAsync(string adminId, VerifyOrganizerDto dto);

        //Admin logs
        Task<List<AdminActionResponseDto>> GetAdminActionsAsync();

        Task<List<SystemLogResponseDto>> GetSystemLogsAsync();

        //Platform stats
        Task<PlatformStatisticsDto> GetPlatformStatisticsAsync();
    }
}
