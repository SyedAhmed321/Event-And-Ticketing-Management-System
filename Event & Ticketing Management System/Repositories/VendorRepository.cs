using Event___Ticketing_Management_System.Configurations;
using Event___Ticketing_Management_System.Interfaces.Repositories;
using Event___Ticketing_Management_System.Models.Vendors;
using MongoDB.Driver;

namespace Event___Ticketing_Management_System.Repositories
{
    public class VendorRepository : IVendorRepository
    {
        private readonly IMongoCollection<Vendor> _vendors;

        public VendorRepository(MongoDbService mongoDbService)
        {
            _vendors = mongoDbService.Database.GetCollection<Vendor>("Vendors");
        }

        //Create vendor
        public async Task CreateAsync(Vendor vendor)
        {
            await _vendors.InsertOneAsync(vendor);
        }

        //Get by ID
        public async Task<Vendor?> GetByIdAsync(string vendorId)
        {
            return await _vendors
                .Find(v => v.Id == vendorId)
                .FirstOrDefaultAsync();
        }

        //Get by user ID
        public async Task<Vendor?> GetByUserIdAsync(string userId)
        {
            return await _vendors
                .Find(v => v.UserId == userId)
                .FirstOrDefaultAsync();
        }

        //Get all vendors
        public async Task<List<Vendor>> GetAllAsync()
        {
            return await _vendors
                .Find(_ => true)
                .ToListAsync();
        }

        //Update vendor (full replace)
        public async Task UpdateAsync(Vendor vendor)
        {
            await _vendors.ReplaceOneAsync(
                v => v.Id == vendor.Id,
                vendor
            );
        }

        //Delete vendor
        public async Task DeleteAsync(string vendorId)
        {
            await _vendors.DeleteOneAsync(v => v.Id == vendorId);
        }
    }
}