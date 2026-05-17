using Event___Ticketing_Management_System.Utilities;
using System.ComponentModel.DataAnnotations;

namespace Event___Ticketing_Management_System.DTOs.Auth
{
    public class RegisterDto
    {
        [Required]
        public string FullName { get; set; } = string.Empty;

        [Required]
        [EmailAddress]
        public string Email { get; set; } = string.Empty;

        [Required]
        public string Password { get; set; } = string.Empty;

        [Required]
        public string Role { get; set; } = string.Empty;

        public string Phone { get; set; } = string.Empty;
    }
}
