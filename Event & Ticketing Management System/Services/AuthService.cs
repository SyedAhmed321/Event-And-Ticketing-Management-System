using BCrypt.Net;
using Event___Ticketing_Management_System.DTOs.Auth;
using Event___Ticketing_Management_System.Interfaces.Repositories;
using Event___Ticketing_Management_System.Interfaces.Services;
using Event___Ticketing_Management_System.Models.Users;
using Event___Ticketing_Management_System.Utilities;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace Event___Ticketing_Management_System.Services
{
    public class AuthService : IAuthService
    {
        private readonly IUserRepository _userRepository;
        private readonly IConfiguration _configuration;
        private readonly IUserProfileRepository _userProfileRepository;
        private readonly IVendorService _vendorService;
        private readonly IOrganizerService _organizerService;

        public AuthService(IUserRepository userRepository, IConfiguration configuration, IUserProfileRepository userProfileRepository, IVendorService vendorService, IOrganizerService organizerService)
        {
            _userRepository = userRepository;
            _configuration = configuration;
            _userProfileRepository = userProfileRepository;
            _vendorService = vendorService;
            _organizerService = organizerService;
        }

        public async Task<string> RegisterAsync(RegisterDto dto)
        {
            var allowedRoles = new[]
            {
                RoleConstants.User,
                RoleConstants.Organizer,
                RoleConstants.Vendor
            };

            var normalizedEmail = dto.Email.Trim().ToLower();
            var existingUser = await _userRepository
                .GetUserByEmailAsync(normalizedEmail);

            if (existingUser != null)
            {
                throw new Exception("User already exists");
            }

            if(!allowedRoles.Contains(dto.Role))
            {
                throw new Exception("Invalid role");
            }

            var user = new User
            {
                FullName = dto.FullName,
                Email = normalizedEmail,
                PasswordHash = BCrypt.Net.BCrypt.HashPassword(dto.Password),
                Phone = dto.Phone,
                Role = dto.Role
            };

            await _userRepository.CreateUserAsync(user);

            var profile = new UserProfile
            {
                UserId = user.Id,
                FirstName = dto.FullName.Split(' ')[0],
                LastName = dto.FullName.Contains(' ') ? dto.FullName.Substring(dto.FullName.IndexOf(' ') + 1) : "",
                CreatedAt = DateTime.UtcNow
            };

            await _userProfileRepository.CreateAsync(profile);

            if(dto.Role == RoleConstants.Vendor)
            {
                await _vendorService.CreateVendorAsync(user.Id, new DTOs.Vendors.CreateVendorDto
                {
                    BusinessName = "New Vendor",
                    Description = "",
                    Category = "",
                    ContactEmail = dto.Email,
                    ContactPhone = "",
                    City = ""

                });
            }

            if(dto.Role == RoleConstants.Organizer)
            {
                await _organizerService.CreateOrganizerAsync(user.Id);
            }

            return GenerateJwtToken(user);
        }

        public async Task<string> LoginAsync(LoginDto dto)
        {
            var normalizedEmail = dto.Email.Trim().ToLower();
            var user = await _userRepository
                .GetUserByEmailAsync(normalizedEmail);

            if (user == null)
            {
                throw new Exception("Invalid credentials");
            }

            bool isPasswordCorrect = BCrypt.Net.BCrypt.Verify(dto.Password, user.PasswordHash);

            if (!isPasswordCorrect)
            {
                throw new Exception("Invalid credentials");
            }

            return GenerateJwtToken(user);
        }

        private string GenerateJwtToken(User user)
        {
            var claims = new[]
            {
            new Claim(ClaimTypes.NameIdentifier, user.Id),
            new Claim(ClaimTypes.Email, user.Email),
            new Claim(ClaimTypes.Role, user.Role)
        };

            var key = new SymmetricSecurityKey(
                Encoding.UTF8.GetBytes(
                    _configuration["JwtSettings:Secret"]!));

            var credentials = new SigningCredentials(
                key,
                SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(
                claims: claims,
                expires: DateTime.UtcNow.AddHours(1),
                signingCredentials: credentials
            );

            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}
