using Event___Ticketing_Management_System.DTOs.Users;
using Event___Ticketing_Management_System.Models.Users;

namespace Event___Ticketing_Management_System.Interfaces.Services
{
    public interface IUserService
    {
        // ✅ PROFILE
        Task<UserProfile> GetProfileAsync(string userId);

        Task UpdateProfileAsync(string userId, UpdateProfileDto dto);

        // ✅ ADDRESS
        //Task<List<UserAddress>> GetAddressesAsync(string userId);

        //Task AddAddressAsync(string userId, CreateAddressDto dto);

        //Task UpdateAddressAsync(string userId, string addressId, UpdateAddressDto dto);

        //Task DeleteAddressAsync(string userId, string addressId);

    }
}
