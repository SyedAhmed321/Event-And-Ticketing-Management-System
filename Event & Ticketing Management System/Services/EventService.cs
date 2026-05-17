using Event___Ticketing_Management_System.DTOs.Events;
using Event___Ticketing_Management_System.Interfaces.Repositories;
using Event___Ticketing_Management_System.Interfaces.Services;
using Event___Ticketing_Management_System.Models.Events;

namespace Event___Ticketing_Management_System.Services
{
    public class EventService : IEventService
    {
        private readonly IEventRepository _repo;

        public EventService(IEventRepository repo)
        {
            _repo = repo;
        }

        public async Task<List<EventResponseDto>> GetAllAsync()
        {
            var events = await _repo.GetAllAsync();

            return events.Select(MapToDto).ToList();
        }

        public async Task<EventResponseDto?> GetByIdAsync(string id)
        {
            var ev = await _repo.GetByIdAsync(id);
            return ev == null ? null : MapToDto(ev);
        }

        public async Task<List<EventResponseDto>> GetByOrganizerIdAsync(string organizerId)
        {
            var events = await _repo.GetByOrganizerIdAsync(organizerId);
            return events.Select(MapToDto).ToList();
        }

        public async Task<string> CreateAsync(CreateEventDto dto, string organizerId)
        {
            var validcategories = new[]
            {
                EventCategory.Concert,
                EventCategory.Wedding,
                EventCategory.Seminar,
                EventCategory.Corporate,
                EventCategory.Party,
                EventCategory.Sports
            };

            var ev = new Event
            {
                OrganizerId = organizerId,
                Title = dto.Title,
                Description = dto.Description,
                Category = dto.Category,
                Venue = dto.Venue,
                City = dto.City,
                BannerImage = dto.BannerImage,
                Images = dto.Images,
                StartDate = dto.StartDate,
                EndDate = dto.EndDate,
                Status = "Published",
                TicketTypes = dto.TicketTypes.Select(t => new TicketType
                {
                    Id = Guid.NewGuid().ToString(),
                    Name = t.Name,
                    Price = t.Price,
                    Quantity = t.Quantity,
                    SalesStart = t.SalesStart,
                    SalesEnd = t.SalesEnd
                }).ToList(),
                CreatedAt = DateTime.UtcNow
            };

            if(!validcategories.Contains(ev.Category))
            { 
                throw new Exception("Invalid category"); 
            }

            await _repo.CreateAsync(ev);

            return ev.Id;
        }

        public async Task<string> UpdateAsync(string id, string organizerId, UpdateEventDto dto)
        {
            var ev = await _repo.GetByIdAsync(id);

            if (ev == null)
                throw new Exception("Event not found");

            
            if (ev.OrganizerId != organizerId)
                throw new UnauthorizedAccessException("Unauthorized");

            ev.Title = dto.Title ?? ev.Title;
            ev.Description = dto.Description ?? ev.Description;
            ev.Category = dto.Category ?? ev.Category;
            ev.Venue = dto.Venue ?? ev.Venue;
            ev.City = dto.City ?? ev.City;
            ev.BannerImage = dto.BannerImage ?? ev.BannerImage;
            ev.Images = dto.Image ?? ev.Images;
            ev.StartDate = dto.StartDate ?? ev.StartDate;
            ev.EndDate = dto.EndDate ?? ev.EndDate;
            ev.Status = dto.Status ?? ev.Status;

            await _repo.UpdateAsync(ev);

            return "Updated Successfully";
        }

        public async Task<string> DeleteAsync(string id, string organizerId)
        {
            var ev = await _repo.GetByIdAsync(id);
            if (ev == null)
                {
                    throw new Exception("Event not found");
            }

            if (ev.OrganizerId != organizerId)
            {
                throw new Exception("Unauthorized");
            }

            await _repo.DeleteAsync(id);
            return "Deleted Successfully";
        }

        private static EventResponseDto MapToDto(Event ev)
        {
            var TicketTypes = ev.TicketTypes.Select(t => new TicketTypeDto
            {
                Id = t.Id,
                Name = t.Name,
                Price = t.Price,
                Quantity = t.Quantity,
            }).ToList();

            return new EventResponseDto
            {
                Id = ev.Id,
                Title = ev.Title,
                Description = ev.Description,
                Category = ev.Category,
                Venue = ev.Venue,
                City = ev.City,
                StartDate = ev.StartDate,
                EndDate = ev.EndDate,
                Status = ev.Status,
                BannerImage = ev.BannerImage,
                TotalTicketsSold = ev.TotalTicketsSold,
                TicketTypes = ev.TicketTypes
                
            };
        }
    }
}
