using Event___Ticketing_Management_System.DTOs.Users;
using Event___Ticketing_Management_System.Interfaces.Services;
using Event___Ticketing_Management_System.Models.Users;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace Event___Ticketing_Management_System.Controllers
{
    [ApiController]
    [Route ("api/[controller]")]
    [Authorize]
    public class UsersController : ControllerBase
    {
        private readonly IUserService _userService;

        public UsersController(IUserService userService) 
        {
            _userService = userService;
        }


        private string GetUserId()
        {
            return User.FindFirst(ClaimTypes.NameIdentifier)?.Value!;
        }

        [HttpGet("profile")]
        public async Task<IActionResult> GetProfile()
        {
            var userId = GetUserId();

            var profile = await _userService.GetProfileAsync(userId);

            return Ok(profile);

        }

        [HttpPut("profile")]
        public async Task<IActionResult> UpdateProfile([FromBody] UpdateProfileDto dto)
        {
            var userId = GetUserId();

            await _userService.UpdateProfileAsync(userId, dto);

            return Ok(new
            {
                message = "Profile updated successfully"
            });
        }

    }
}
