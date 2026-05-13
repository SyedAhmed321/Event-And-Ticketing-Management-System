using Event___Ticketing_Management_System.Interfaces.Repositories;
using Event___Ticketing_Management_System.Models.Users;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace Event___Ticketing_Management_System.Controllers
{
    [ApiController]
    [Route("api/tickets")]
    public class TicketsController : ControllerBase
    {
        private readonly ITicketRepository _ticketRepository;

        public TicketsController(ITicketRepository ticketRepository)
        {
            _ticketRepository = ticketRepository;
        }

        // User views all their tickets
        [HttpGet("my-tickets")]
        [Authorize]
        public async Task<IActionResult> GetMyTickets()
        {
            var userId = User.FindFirst(ClaimTypes.NameIdentifier)!.Value;
            var tickets = await _ticketRepository.GetTicketsByUserIdAsync(userId);
            return Ok(tickets);
        }

        // User views single ticket
        [HttpGet("{ticketId}")]
        [Authorize]
        public async Task<IActionResult> GetTicket(string ticketId)
        {
            var ticket = await _ticketRepository.GetTicketByIdAsync(ticketId);
            if (ticket == null) return NotFound();
            return Ok(ticket);
        }
    }
}
