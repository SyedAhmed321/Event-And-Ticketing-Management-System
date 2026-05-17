using Event___Ticketing_Management_System.Interfaces.Services;
using Event___Ticketing_Management_System.Interfaces.Repositories;
using Event___Ticketing_Management_System.DTOs.Users;
using Event___Ticketing_Management_System.Models.Users;

namespace Event___Ticketing_Management_System.Services
{
    public class UserService : IUserService
    {
        private readonly IUserProfileRepository _userProfileRepository;

        public UserService(IUserProfileRepository userProfileRepository)
        {
            _userProfileRepository = userProfileRepository;
        }


        public async Task<UserProfile> GetProfileAsync(string userId)
        {
            var profile = await _userProfileRepository.GetByUserIdAsync(userId);

            if (profile == null)
                throw new Exception("User profile not found");

            return profile;
        }


        public async Task UpdateProfileAsync(string userId, UpdateProfileDto dto)
        {
            var profile = await _userProfileRepository.GetByUserIdAsync(userId);

            if (profile == null)
                throw new Exception("User profile not found");

            profile.FirstName = dto.FirstName;
            profile.LastName = dto.LastName;
            profile.Gender = dto.Gender;
            profile.DateOfBirth = dto.DateOfBirth;
            profile.Bio = dto.Bio;
            profile.City = dto.City;
            profile.Country = dto.Country;
            profile.UpdatedAt = DateTime.UtcNow;

            await _userProfileRepository.UpdateAsync(userId, profile);
        }


    }
}
