using Event___Ticketing_Management_System.Models.Vendors;

namespace Event___Ticketing_Management_System.Interfaces.Repositories
{
    public interface IVendorRepository
    {
        //Create vendor
        Task CreateAsync(Vendor vendor);

        //Get vendor by ID
        Task<Vendor?> GetByIdAsync(string vendorId);

        //Get vendor by user (important)
        Task<Vendor?> GetByUserIdAsync(string userId);

        //Get all vendors (optional - marketplace)
        Task<List<Vendor>> GetAllAsync();

        //Update vendor (full replace)
        Task UpdateAsync(Vendor vendor);

        //Delete vendor (optional soft delete recommended)
        Task DeleteAsync(string vendorId);
    }
}
