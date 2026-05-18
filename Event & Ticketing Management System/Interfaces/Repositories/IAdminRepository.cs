using Event___Ticketing_Management_System.Models.Admin;

namespace Event___Ticketing_Management_System.Interfaces.Repositories
{
    public interface IAdminRepository
    {
        // ✅ Admin Actions (Audit Trail)
        Task CreateAdminActionAsync(AdminAction action);

        Task<List<AdminAction>> GetAdminActionsAsync();

        // ✅ System Logs
        Task CreateSystemLogAsync(SystemLog log);

        Task<List<SystemLog>> GetSystemLogsAsync();
    }
}