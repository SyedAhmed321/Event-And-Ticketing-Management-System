using Event___Ticketing_Management_System.DTOs.Vendors;

namespace Event___Ticketing_Management_System.Interfaces.Services
{
    public interface IVendorService
    {
        //Create vendor profile
        Task<string> CreateVendorAsync(string userId, CreateVendorDto dto);

        //Get logged-in vendor
        Task<VendorResponseDto?> GetMyVendorAsync(string userId);

        //Update vendor profile
        Task<string> UpdateVendorAsync(string userId, UpdateVendorDto dto);

        //Add service to vendor
        Task<string> AddServiceAsync(string userId, CreateVendorServiceDto dto);

        //Update vendor service
        Task<string> UpdateServiceAsync(string userId, UpdateVendorServiceDto dto);

        //Get all vendors (optional marketplace)
        Task<List<VendorResponseDto>> GetAllVendorsAsync();
    }
}