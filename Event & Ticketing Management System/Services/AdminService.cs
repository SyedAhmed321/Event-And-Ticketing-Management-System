using Event___Ticketing_Management_System.DTOs.Admin;
using Event___Ticketing_Management_System.Interfaces.Repositories;
using Event___Ticketing_Management_System.Interfaces.Services;
using Event___Ticketing_Management_System.Models.Admin;

namespace Event___Ticketing_Management_System.Services
{
    public class AdminService : IAdminService
    {
        private readonly IUserRepository _userRepository;
        private readonly IVendorRepository _vendorRepository;
        private readonly IOrganizerRepository _organizerRepository;
        private readonly IDashboardRepository _dashboardRepository;
        private readonly IAdminRepository _adminRepository;

        public AdminService(
            IUserRepository userRepository,
            IVendorRepository vendorRepository,
            IOrganizerRepository organizerRepository,
            IDashboardRepository dashboardRepository,
            IAdminRepository adminRepository)
        {
            _userRepository = userRepository;
            _vendorRepository = vendorRepository;
            _organizerRepository = organizerRepository;
            _dashboardRepository = dashboardRepository;
            _adminRepository = adminRepository;
        }

        // =========================================
        // ✅ SUSPEND USER
        // =========================================
        public async Task<string> SuspendUserAsync(string adminId, SuspendUserDto dto)
        {
            var user = await _userRepository.GetUserByIdAsync(dto.UserId);

            if (user == null)
                throw new Exception("User not found");

            user.IsSuspended = dto.IsSuspended;

            await _userRepository.UpdateUserAsync(user);

            // ✅ Log admin action
            await _adminRepository.CreateAdminActionAsync(new AdminAction
            {
                AdminId = adminId,
                ActionType = "SuspendUser",
                Description = dto.IsSuspended
                    ? "User suspended"
                    : "User unsuspended",
                TargetId = user.Id,
                TargetType = "User"
            });

            return "User status updated successfully";
        }

        // =========================================
        // ✅ VERIFY VENDOR
        // =========================================
        public async Task<string> VerifyVendorAsync(string adminId, VerifyVendorDto dto)
        {
            var vendor = await _vendorRepository.GetByIdAsync(dto.VendorId);

            if (vendor == null)
                throw new Exception("Vendor not found");

            vendor.Profile.IsVerified = dto.IsVerified;

            await _vendorRepository.UpdateAsync(vendor);

            await _adminRepository.CreateAdminActionAsync(new AdminAction
            {
                AdminId = adminId,
                ActionType = "VerifyVendor",
                Description = dto.IsVerified ? "Vendor verified" : "Vendor unverified",
                TargetId = vendor.Id,
                TargetType = "Vendor"
            });

            return "Vendor verification updated";
        }

        // =========================================
        // ✅ VERIFY ORGANIZER
        // =========================================
        public async Task<string> VerifyOrganizerAsync(string adminId, VerifyOrganizerDto dto)
        {
            var organizer = await _organizerRepository.GetByIdAsync(dto.OrganizerId);

            if (organizer == null)
                throw new Exception("Organizer not found");

            organizer.Profile.IsVerified = dto.IsVerified;

            await _organizerRepository.UpdateAsync(organizer);

            await _adminRepository.CreateAdminActionAsync(new AdminAction
            {
                AdminId = adminId,
                ActionType = "VerifyOrganizer",
                Description = dto.IsVerified ? "Organizer verified" : "Organizer unverified",
                TargetId = organizer.Id,
                TargetType = "Organizer"
            });

            return "Organizer verification updated";
        }

        // =========================================
        // ✅ GET ADMIN ACTIONS
        // =========================================
        public async Task<List<AdminActionResponseDto>> GetAdminActionsAsync()
        {
            var actions = await _adminRepository.GetAdminActionsAsync();

            return actions.Select(a => new AdminActionResponseDto
            {
                Id = a.Id,
                AdminId = a.AdminId,
                ActionType = a.ActionType,
                Description = a.Description,
                TargetId = a.TargetId,
                TargetType = a.TargetType,
                PerformedAt = a.PerformedAt
            }).ToList();
        }

        // =========================================
        // ✅ GET SYSTEM LOGS
        // =========================================
        public async Task<List<SystemLogResponseDto>> GetSystemLogsAsync()
        {
            var logs = await _adminRepository.GetSystemLogsAsync();

            return logs.Select(l => new SystemLogResponseDto
            {
                Id = l.Id,
                Level = l.Level,
                Message = l.Message,
                Exception = l.Exception,
                Source = l.Source,
                CreatedAt = l.CreatedAt
            }).ToList();
        }

        // =========================================
        // ✅ PLATFORM STATISTICS
        // =========================================
        public async Task<PlatformStatisticsDto> GetPlatformStatisticsAsync()
        {
            return new PlatformStatisticsDto
            {
                TotalUsers = await _dashboardRepository.GetTotalUsersAsync(),
                TotalEvents = await _dashboardRepository.GetTotalEventsAsync(),
                TotalBookings = await _dashboardRepository.GetTotalBookingsAsync(),
                TotalRevenue = await _dashboardRepository.GetTotalRevenueAsync(),

                // optional values (can improve later)
                TotalOrganizers = 0,
                TotalVendors = 0,

                GeneratedAt = DateTime.UtcNow
            };
        }
    }
}