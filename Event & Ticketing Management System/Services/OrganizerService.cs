using Event___Ticketing_Management_System.DTOs.Organizers;
using Event___Ticketing_Management_System.Interfaces.Repositories;
using Event___Ticketing_Management_System.Interfaces.Services;
using Event___Ticketing_Management_System.Models.Organizers;
using Event___Ticketing_Management_System.Models.PersonalEvents;
using Event___Ticketing_Management_System.Repositories;
using Event___Ticketing_Management_System.Utilities;

namespace Event___Ticketing_Management_System.Services
{
    public class OrganizerService : IOrganizerService
    {
        private readonly IOrganizerRepository _organizerRepository;
        private readonly IPersonalEventRepository _personalEventRepository;
        private readonly INotificationService _notificationService;

        public OrganizerService(IOrganizerRepository organizerRepository,
            IPersonalEventRepository personalEventRepository,
            INotificationService notificationService)
        {
            _organizerRepository = organizerRepository;
            _personalEventRepository = personalEventRepository;
            _notificationService = notificationService;
        }

        //CREATE ORGANIZER (AUTO OR MANUAL)
        public async Task<string> CreateOrganizerAsync(string userId)
        {
            var existing = await _organizerRepository.GetByUserIdAsync(userId);

            if (existing != null)
                throw new Exception("Organizer already exists");

            var organizer = new Organizer
            {
                UserId = userId
            };

            await _organizerRepository.CreateAsync(organizer);

            return organizer.Id;
        }

        //GET MY ORGANIZER
        public async Task<OrganizerResponseDto?> GetMyOrganizerAsync(string userId)
        {
            var organizer = await _organizerRepository.GetByUserIdAsync(userId);

            if (organizer == null)
                return null;

            return MapToDto(organizer);
        }

        //UPDATE ORGANIZER
        public async Task<string> UpdateOrganizerAsync(string userId, UpdateOrganizerDto dto)
        {
            var organizer = await _organizerRepository.GetByUserIdAsync(userId);

            if (organizer == null)
                throw new Exception("Organizer not found");

            var profile = organizer.Profile;

            profile.BusinessName = dto.BusinessName ?? profile.BusinessName;
            profile.Description = dto.Description ?? profile.Description;
            profile.Specialization = dto.Specialization ?? profile.Specialization;
            profile.ContactEmail = dto.ContactEmail ?? profile.ContactEmail;
            profile.ContactPhone = dto.ContactPhone ?? profile.ContactPhone;
            profile.City = dto.City ?? profile.City;

            await _organizerRepository.UpdateAsync(organizer);

            return "Organizer updated";
        }

        //GET ALL ORGANIZERS (PUBLIC)
        public async Task<List<OrganizerResponseDto>> GetAllOrganizersAsync()
        {
            var organizers = await _organizerRepository.GetAllAsync();

            return organizers.Select(MapToDto).ToList();
        }

        // =========================================
        //CREATE ORGANIZER REQUEST 🔥
        // =========================================
        public async Task<string> CreateRequestAsync(string userId, OrganizerRequestDto dto)
        {
            var request = new OrganizerRequest
            {
                UserId = userId,
                OrganizerId = dto.OrganizerId,
                EventType = dto.EventType,
                Description = dto.Description,
                Location = dto.Location,
                EventDate = dto.EventDate,
                GuestCount = dto.GuestCount,
                Budget = dto.Budget,
                Status = OrganizerRequestStatuses.Pending
            };

            await _organizerRepository.CreateRequestAsync(request);

            return request.Id;
        }

        //USER VIEW THEIR REQUESTS
        public async Task<List<OrganizerRequestResponseDto>> GetMyRequestsAsync(string userId)
        {
            var requests = await _organizerRepository.GetRequestsByUserIdAsync(userId);

            return requests.Select(MapRequestToDto).ToList();
        }

        //ORGANIZER VIEW REQUESTS
        public async Task<List<OrganizerRequestResponseDto>> GetRequestsForOrganizerAsync(string userId)
        {
            var organizer = await _organizerRepository.GetByUserIdAsync(userId);

            if (organizer == null)
                throw new Exception("Organizer not found");

            var requests = await _organizerRepository
                .GetRequestsByOrganizerIdAsync(organizer.Id);

            return requests.Select(MapRequestToDto).ToList();
        }

        //ACCEPT / REJECT REQUEST
        public async Task<string> UpdateRequestStatusAsync(string organizerUserId, UpdateOrganizerRequestStatusDto dto)
        {
            var organizer = await _organizerRepository.GetByUserIdAsync(organizerUserId);

            if (organizer == null)
                throw new Exception("Organizer not found");

            var request = await _organizerRepository.GetRequestByIdAsync(dto.RequestId);

            if (request == null)
                throw new Exception("Request not found");

            if (request.OrganizerId != organizer.Id)
                throw new UnauthorizedAccessException("Unauthorized");

            request.Status = dto.Status;

            await _organizerRepository.UpdateRequestAsync(request);
            if (dto.Status == "Approved")
            {
                //Create PersonalEvent
                var personalEvent = new PersonalEvent
                {
                    UserId = request.UserId,
                    OrganizerId = organizer.Id,
                    Title = request.EventType,
                    EventType = request.EventType,
                    Location = request.Location,
                    EventDate = request.EventDate,
                    GuestCount = request.GuestCount,
                    Budget = new PlanningBudget
                    {
                        TotalBudget = request.Budget
                    }
                };

                await _personalEventRepository.CreateAsync(personalEvent);

                await _notificationService.SendAsync(
                    request.UserId,
                    "Request Approved",
                    "Your request was accepted by organizer",
                    "OrganizerRequest"
                );
            }


            return "Request updated successfully";
        }

        //ADD RATING
        public async Task<string> AddRatingAsync(string userId, OrganizerRatingDto dto)
        {
            var organizer = await _organizerRepository.GetByIdAsync(dto.OrganizerId);

            if (organizer == null)
                throw new Exception("Organizer not found");

            var rating = new OrganizerRating
            {
                UserId = userId,
                Rating = dto.Rating,
                Comment = dto.Comment
            };

            organizer.Ratings.Add(rating);

            //Update average
            organizer.TotalReviews = organizer.Ratings.Count;
            organizer.AverageRating = organizer.Ratings.Average(r => r.Rating);

            await _organizerRepository.UpdateAsync(organizer);

            return "Rating added";
        }

        //MAPPING
        private static OrganizerResponseDto MapToDto(Organizer o)
        {
            return new OrganizerResponseDto
            {
                Id = o.Id,
                BusinessName = o.Profile.BusinessName,
                Description = o.Profile.Description,
                Specialization = o.Profile.Specialization,
                ContactEmail = o.Profile.ContactEmail,
                ContactPhone = o.Profile.ContactPhone,
                City = o.Profile.City,
                IsVerified = o.Profile.IsVerified,
                AverageRating = o.AverageRating,
                TotalReviews = o.TotalReviews
            };
        }

        private static OrganizerRequestResponseDto MapRequestToDto(OrganizerRequest r)
        {
            return new OrganizerRequestResponseDto
            {
                Id = r.Id,
                OrganizerId = r.OrganizerId,
                UserId = r.UserId,
                EventType = r.EventType,
                Description = r.Description,
                Location = r.Location,
                EventDate = r.EventDate,
                GuestCount = r.GuestCount,
                Budget = r.Budget,
                Status = r.Status,
                CreatedAt = r.CreatedAt
            };
        }
    }
}