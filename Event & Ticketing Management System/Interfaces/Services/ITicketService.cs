using Event___Ticketing_Management_System.DTOs.Tickets;

namespace Event___Ticketing_Management_System.Interfaces.Services
{
    public interface ITicketService
    {

        //Generate tickets after booking confirmation
        Task GenerateTicketsAsync(string bookingId, string userId);

        //Get user tickets
        Task<List<TicketDto>> GetMyTicketsAsync(string userId);

        //Validate ticket via QR scan
        Task<TicketValidationResponseDto> ValidateTicketAsync(ValidateTicketDto dto);

        //Check-in ticket
        Task<string> CheckInTicketAsync(CheckInDto dto, string staffUserId);

    }
}