using Event___Ticketing_Management_System.DTOs.Vendors;
using Event___Ticketing_Management_System.Interfaces.Repositories;
using Event___Ticketing_Management_System.Interfaces.Services;
using Event___Ticketing_Management_System.Models.Vendors;

namespace Event___Ticketing_Management_System.Services
{
    public class VendorService : IVendorService
    {
        private readonly IVendorRepository _vendorRepository;

        public VendorService(IVendorRepository vendorRepository)
        {
            _vendorRepository = vendorRepository;
        }

        //CREATE VENDOR
        public async Task<string> CreateVendorAsync(string userId, CreateVendorDto dto)
        {
            var existing = await _vendorRepository.GetByUserIdAsync(userId);

            if (existing != null)
                throw new Exception("Vendor profile already exists");

            var vendor = new Vendor
            {
                UserId = userId,
                Profile = new VendorProfile
                {
                    BusinessName = dto.BusinessName,
                    Description = dto.Description,
                    Category = dto.Category,
                    ContactEmail = dto.ContactEmail,
                    ContactPhone = dto.ContactPhone,
                    City = dto.City
                }
            };

            await _vendorRepository.CreateAsync(vendor);

            return vendor.Id;
        }

        //GET MY VENDOR
        public async Task<VendorResponseDto?> GetMyVendorAsync(string userId)
        {
            var vendor = await _vendorRepository.GetByUserIdAsync(userId);

            if (vendor == null)
                return null;

            return MapToDto(vendor);
        }

        //UPDATE VENDOR PROFILE
        public async Task<string> UpdateVendorAsync(string userId, UpdateVendorDto dto)
        {
            var vendor = await _vendorRepository.GetByUserIdAsync(userId);

            if (vendor == null)
                throw new Exception("Vendor not found");

            var profile = vendor.Profile;

            profile.BusinessName = dto.BusinessName ?? profile.BusinessName;
            profile.Description = dto.Description ?? profile.Description;
            profile.Category = dto.Category ?? profile.Category;
            profile.ContactEmail = dto.ContactEmail ?? profile.ContactEmail;
            profile.ContactPhone = dto.ContactPhone ?? profile.ContactPhone;
            profile.City = dto.City ?? profile.City;

            await _vendorRepository.UpdateAsync(vendor);

            return "Vendor updated successfully";
        }

        //ADD SERVICE
        public async Task<string> AddServiceAsync(string userId, CreateVendorServiceDto dto)
        {
            var vendor = await _vendorRepository.GetByUserIdAsync(userId);

            if (vendor == null)
                throw new Exception("Vendor not found");

            var service = new VendorServiceModel
            {
                ServiceId = Guid.NewGuid().ToString(),
                Name = dto.Name,
                Description = dto.Description,
                Price = dto.Price,
                IsAvailable = true
            };

            vendor.Services.Add(service);

            await _vendorRepository.UpdateAsync(vendor);

            return service.ServiceId;
        }

        //UPDATE SERVICE
        public async Task<string> UpdateServiceAsync(string userId, UpdateVendorServiceDto dto)
        {
            var vendor = await _vendorRepository.GetByUserIdAsync(userId);

            if (vendor == null)
                throw new Exception("Vendor not found");

            var service = vendor.Services.FirstOrDefault(s => s.ServiceId == dto.ServiceId);

            if (service == null)
                throw new Exception("Service not found");

            service.Name = dto.Name ?? service.Name;
            service.Description = dto.Description ?? service.Description;
            service.Price = dto.Price ?? service.Price;
            service.IsAvailable = dto.IsAvailable ?? service.IsAvailable;

            await _vendorRepository.UpdateAsync(vendor);

            return "Service updated successfully";
        }

        //GET ALL VENDORS (OPTIONAL)
        public async Task<List<VendorResponseDto>> GetAllVendorsAsync()
        {
            var vendors = await _vendorRepository.GetAllAsync();

            return vendors.Select(MapToDto).ToList();
        }

        //MAPPING METHOD
        private static VendorResponseDto MapToDto(Vendor v)
        {
            return new VendorResponseDto
            {
                Id = v.Id,
                BusinessName = v.Profile.BusinessName,
                Description = v.Profile.Description,
                Category = v.Profile.Category,
                ContactEmail = v.Profile.ContactEmail,
                ContactPhone = v.Profile.ContactPhone,
                City = v.Profile.City,
                IsVerified = v.Profile.IsVerified,

                Services = v.Services.Select(s => new VendorServiceDto
                {
                    ServiceId = s.ServiceId,
                    Name = s.Name,
                    Description = s.Description,
                    Price = s.Price,
                    IsAvailable = s.IsAvailable
                }).ToList()
            };
        }
    }
}