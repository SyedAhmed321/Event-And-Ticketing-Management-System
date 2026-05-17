using Event___Ticketing_Management_System.Models.Users;

namespace Event___Ticketing_Management_System.Interfaces.Repositories
{
    public interface IUserRepository
    {
        Task<User?> GetUserByEmailAsync(string email);

        Task<User?> GetUserByIdAsync(string id);

        Task CreateUserAsync(User user);

        Task UpdateUserAsync(User user);

        Task<bool> EmailExistsAsync(string email);


    }
}
