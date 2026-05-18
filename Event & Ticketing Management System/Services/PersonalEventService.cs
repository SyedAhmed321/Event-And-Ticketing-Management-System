using Event___Ticketing_Management_System.DTOs.PersonalEvents;
using Event___Ticketing_Management_System.Interfaces.Repositories;
using Event___Ticketing_Management_System.Interfaces.Services;
using Event___Ticketing_Management_System.Models.PersonalEvents;
using Event___Ticketing_Management_System.Utilities;

namespace Event___Ticketing_Management_System.Services
{
    public class PersonalEventService : IPersonalEventService
    {
        private readonly IPersonalEventRepository _repository;

        public PersonalEventService(IPersonalEventRepository repository)
        {
            _repository = repository;
        }

        //CREATE EVENT
        public async Task<string> CreateEventAsync(string userId, CreatePersonalEventDto dto)
        {
            var ev = new PersonalEvent
            {
                UserId = userId,
                Title = dto.Title,
                EventType = dto.EventType,
                Location = dto.Location,
                EventDate = dto.EventDate,
                GuestCount = dto.GuestCount,
                Status = PersonalEventStatuses.Planning,
                Budget = new PlanningBudget
                {
                    TotalBudget = dto.TotalBudget,
                    EstimatedCost = 0,
                    ActualCost = 0
                }
            };

            await _repository.CreateAsync(ev);

            return ev.Id;
        }

        //GET MY EVENTS
        public async Task<List<PersonalEventResponseDto>> GetMyEventsAsync(string userId)
        {
            var events = await _repository.GetByUserIdAsync(userId);

            return events.Select(MapToDto).ToList();
        }

        //GET BY ID
        public async Task<PersonalEventResponseDto?> GetByIdAsync(string eventId)
        {
            var ev = await _repository.GetByIdAsync(eventId);

            return ev == null ? null : MapToDto(ev);
        }


        //GET BY ORGANIZER ID
        public async Task<List<PersonalEventResponseDto>> GetByOrganizerAsync(string organizerId)
        {
            var events = await _repository.GetByOrganizerIdAsync(organizerId);

            return events.Select(MapToDto).ToList();
        }


        //UPDATE EVENT
        public async Task<string> UpdateEventAsync(string userId, string eventId, UpdatePersonalEventDto dto)
        {
            var ev = await _repository.GetByIdAsync(eventId);

            if (ev == null)
                throw new Exception("Event not found");

            if (ev.UserId != userId)
                throw new UnauthorizedAccessException("Unauthorized");

            ev.Title = dto.Title ?? ev.Title;
            ev.Location = dto.Location ?? ev.Location;
            ev.EventDate = dto.EventDate ?? ev.EventDate;
            ev.GuestCount = dto.GuestCount ?? ev.GuestCount;
            ev.Status = dto.Status ?? ev.Status;

            await _repository.UpdateAsync(ev);

            return "Event updated";
        }

        //ADD REQUIREMENT
        public async Task<string> AddRequirementAsync(string userId, string eventId, CreateRequirementDto dto)
        {
            var ev = await _repository.GetByIdAsync(eventId);

            if (ev == null || ev.UserId != userId)
                throw new Exception("Unauthorized or not found");

            ev.Requirements.Add(new EventRequirement
            {
                RequirementId = Guid.NewGuid().ToString(),
                Category = dto.Category,
                Description = dto.Description
            });

            await _repository.UpdateAsync(ev);

            return "Requirement added";
        }

        //ADD TIMELINE
        public async Task<string> AddTimelineAsync(string userId, string eventId, AddTimelineDto dto)
        {
            var ev = await _repository.GetByIdAsync(eventId);

            if (ev == null || ev.UserId != userId)
                throw new Exception("Unauthorized");

            ev.Timeline.Add(new EventTimeline
            {
                TimelineId = Guid.NewGuid().ToString(),
                Title = dto.Title,
                Description = dto.Description,
                StartTime = dto.StartTime,
                EndTime = dto.EndTime
            });

            await _repository.UpdateAsync(ev);

            return "Timeline added";
        }

        //UPDATE TIMELINE
        public async Task<string> UpdateTimelineAsync(string userId, string eventId, UpdateTimelineDto dto)
        {
            var ev = await _repository.GetByIdAsync(eventId);

            if (ev == null || ev.UserId != userId)
                throw new Exception("Unauthorized");

            var t = ev.Timeline.FirstOrDefault(x => x.TimelineId == dto.TimelineId);

            if (t == null)
                throw new Exception("Timeline not found");

            t.Title = dto.Title ?? t.Title;
            t.Description = dto.Description ?? t.Description;
            t.StartTime = dto.StartTime ?? t.StartTime;
            t.EndTime = dto.EndTime ?? t.EndTime;

            await _repository.UpdateAsync(ev);

            return "Timeline updated";
        }

        //UPDATE BUDGET
        public async Task<string> UpdateBudgetAsync(string userId, string eventId, UpdateBudgetDto dto)
        {
            var ev = await _repository.GetByIdAsync(eventId);

            if (ev == null || ev.UserId != userId)
                throw new Exception("Unauthorized");

            var budget = ev.Budget;

            budget.TotalBudget = dto.TotalBudget ?? budget.TotalBudget;
            budget.EstimatedCost = dto.EstimatedCost ?? budget.EstimatedCost;
            budget.ActualCost = dto.ActualCost ?? budget.ActualCost;

            await _repository.UpdateAsync(ev);

            return "Budget updated";
        }

        //DELETE EVENT
        public async Task<string> DeleteEventAsync(string userId, string eventId)
        {
            var ev = await _repository.GetByIdAsync(eventId);

            if (ev == null || ev.UserId != userId)
                throw new Exception("Unauthorized");

            await _repository.DeleteAsync(eventId);

            return "Event deleted";
        }

        //DTO MAPPING
        private static PersonalEventResponseDto MapToDto(PersonalEvent ev)
        {
            return new PersonalEventResponseDto
            {
                Id = ev.Id,
                Title = ev.Title,
                EventType = ev.EventType,
                Location = ev.Location,
                EventDate = ev.EventDate,
                GuestCount = ev.GuestCount,
                Status = ev.Status,

                Requirements = ev.Requirements.Select(r => new RequirementResponseDto
                {
                    RequirementId = r.RequirementId,
                    Category = r.Category,
                    Description = r.Description,
                    IsFulfilled = r.IsFulfilled,
                    VendorId = r.VendorId
                }).ToList(),

                Timeline = ev.Timeline.Select(t => new TimelineResponseDto
                {
                    TimelineId = t.TimelineId,
                    Title = t.Title,
                    Description = t.Description,
                    StartTime = t.StartTime,
                    EndTime = t.EndTime
                }).ToList(),

                Budget = new BudgetResponseDto
                {
                    TotalBudget = ev.Budget.TotalBudget,
                    EstimatedCost = ev.Budget.EstimatedCost,
                    ActualCost = ev.Budget.ActualCost,
                    RemainingBudget = ev.Budget.RemainingBudget
                }
            };
        }
    }
}