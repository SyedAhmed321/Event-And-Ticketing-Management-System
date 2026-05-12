using Event___Ticketing_Management_System.DTOs.Auth;

namespace Event___Ticketing_Management_System.Interfaces.Services
{
    public interface IAuthService
    {
        Task<string> RegisterAsync(RegisterDto dto);
        Task<string> LoginAsync(LoginDto dto);
    }
}
