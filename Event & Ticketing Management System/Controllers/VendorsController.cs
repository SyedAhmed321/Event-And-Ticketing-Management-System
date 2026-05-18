using Event___Ticketing_Management_System.DTOs.Vendors;
using Event___Ticketing_Management_System.Interfaces.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace Event___Ticketing_Management_System.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize]
    public class VendorsController : ControllerBase
    {
        private readonly IVendorService _vendorService;

        public VendorsController(IVendorService vendorService)
        {
            _vendorService = vendorService;
        }

        //Helper → Get logged-in user ID
        private string GetUserId()
        {
            return User.FindFirst(ClaimTypes.NameIdentifier)?.Value!;
        }

        // =========================================
        //CREATE VENDOR PROFILE
        // =========================================
        [HttpPost]
        public async Task<IActionResult> CreateVendor([FromBody] CreateVendorDto dto)
        {
            var userId = GetUserId();

            var vendorId = await _vendorService.CreateVendorAsync(userId, dto);

            return Ok(new
            {
                message = "Vendor created successfully",
                vendorId
            });
        }

        // =========================================
        //GET MY VENDOR PROFILE
        // =========================================
        [HttpGet("my")]
        public async Task<IActionResult> GetMyVendor()
        {
            var userId = GetUserId();

            var vendor = await _vendorService.GetMyVendorAsync(userId);

            if (vendor == null)
                return NotFound("Vendor not found");

            return Ok(vendor);
        }

        // =========================================
        //UPDATE VENDOR PROFILE
        // =========================================
        [HttpPut]
        public async Task<IActionResult> UpdateVendor([FromBody] UpdateVendorDto dto)
        {
            var userId = GetUserId();

            var result = await _vendorService.UpdateVendorAsync(userId, dto);

            return Ok(new
            {
                message = result
            });
        }

        // =========================================
        //ADD SERVICE
        // =========================================
        [HttpPost("services")]
        public async Task<IActionResult> AddService([FromBody] CreateVendorServiceDto dto)
        {
            var userId = GetUserId();

            var serviceId = await _vendorService.AddServiceAsync(userId, dto);

            return Ok(new
            {
                message = "Service added successfully",
                serviceId
            });
        }

        // =========================================
        //UPDATE SERVICE
        // =========================================
        [HttpPut("services")]
        public async Task<IActionResult> UpdateService([FromBody] UpdateVendorServiceDto dto)
        {
            var userId = GetUserId();

            var result = await _vendorService.UpdateServiceAsync(userId, dto);

            return Ok(new
            {
                message = result
            });
        }

        // =========================================
        //GET ALL VENDORS (MARKETPLACE)
        // =========================================
        [HttpGet]
        [AllowAnonymous] //public listing
        public async Task<IActionResult> GetAllVendors()
        {
            var vendors = await _vendorService.GetAllVendorsAsync();

            return Ok(vendors);
        }
    }
}