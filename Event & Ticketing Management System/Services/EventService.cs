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

        public async Task<string> CreateAsync(CreateEventDto dto)
        {
            var ev = new Event
            {
                OrganizerId = dto.OrganizerId,
                Title = dto.Title,
                Description = dto.Description,
                Category = dto.Category,
                Venue = dto.Venue,
                City = dto.City,
                BannerImage = dto.BannerImage,
                StartDate = dto.StartDate,
                EndDate = dto.EndDate,
                Status = "Published",
                TicketTypes = dto.TicketTypes.Select(t => new TicketType
                {
                    Name = t.Name,
                    Price = t.Price,
                    Quantity = t.Quantity,
                    SalesStart = t.SalesStart,
                    SalesEnd = t.SalesEnd
                }).ToList()
            };

            await _repo.CreateAsync(ev);

            return ev.Id;
        }

        public async Task<string> UpdateAsync(string id, UpdateEventDto dto)
        {
            var ev = await _repo.GetByIdAsync(id);

            if (ev == null)
                throw new Exception("Event not found");

            ev.Title = dto.Title;
            ev.Description = dto.Description;
            ev.Category = dto.Category;
            ev.Venue = dto.Venue;
            ev.City = dto.City;
            ev.BannerImage = dto.BannerImage;
            ev.StartDate = dto.StartDate;
            ev.EndDate = dto.EndDate;
            ev.Status = dto.Status;

            await _repo.UpdateAsync(ev);

            return "Updated Successfully";
        }

        public async Task<string> DeleteAsync(string id)
        {
            await _repo.DeleteAsync(id);
            return "Deleted Successfully";
        }

        private static EventResponseDto MapToDto(Event ev)
        {
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
                TicketTypes = ev.TicketTypes,
                CreatedAt = ev.CreatedAt
            };
        }
    }
}
