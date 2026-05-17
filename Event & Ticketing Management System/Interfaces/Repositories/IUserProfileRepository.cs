using Event___Ticketing_Management_System.Models.Users;

namespace Event___Ticketing_Management_System.Interfaces.Repositories
{
    public interface IUserProfileRepository
    {

        Task<UserProfile?> GetByUserIdAsync(string userId);
        Task CreateAsync(UserProfile profile);
        Task UpdateAsync(string userId, UserProfile profile);

    }
}
