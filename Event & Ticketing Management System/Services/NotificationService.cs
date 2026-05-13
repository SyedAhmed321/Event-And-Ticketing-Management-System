using Event___Ticketing_Management_System.Interfaces.Repositories;
using Event___Ticketing_Management_System.Interfaces.Services;
using Event___Ticketing_Management_System.Models.Notifications;
using static Event___Ticketing_Management_System.DTOs.Notifications.NotificationDto;

namespace Event___Ticketing_Management_System.Services
{
    public class NotificationService : INotificationService
    {
        private readonly INotificationRepository _notificationRepository;

        public NotificationService(INotificationRepository notificationRepository)
        {
            _notificationRepository = notificationRepository;
        }

        // ── GET ALL NOTIFICATIONS FOR USER ──────────────────────────
        public async Task<List<NotificationResponseDto>> GetMyNotificationsAsync(string userId)
        {
            var notifications = await _notificationRepository
                .GetByUserIdAsync(userId);

            return notifications.Select(n => new NotificationResponseDto
            {
                Id = n.Id,
                Title = n.Title,
                Message = n.Message,
                Type = n.Type,
                IsRead = n.IsRead,
                CreatedAt = n.CreatedAt
            }).ToList();
        }

        // ── GET UNREAD COUNT ─────────────────────────────────────────
        public async Task<int> GetUnreadCountAsync(string userId)
        {
            return await _notificationRepository.GetUnreadCountAsync(userId);
        }

        // ── MARK SINGLE AS READ ──────────────────────────────────────
        public async Task MarkAsReadAsync(string notificationId)
        {
            await _notificationRepository.MarkAsReadAsync(notificationId);
        }

        // ── MARK ALL AS READ ─────────────────────────────────────────
        public async Task MarkAllAsReadAsync(string userId)
        {
            await _notificationRepository.MarkAllAsReadAsync(userId);
        }

        // ── DELETE NOTIFICATION ──────────────────────────────────────
        public async Task DeleteNotificationAsync(string notificationId)
        {
            await _notificationRepository.DeleteNotificationAsync(notificationId);
        }

        // ── INTERNAL HELPERS ─────────────────────────────────────────
        // These are called from BookingService, VendorService etc.

        public async Task SendBookingConfirmedAsync(
            string userId, string eventTitle, int ticketCount)
        {
            await _notificationRepository.CreateNotificationAsync(new Notification
            {
                UserId = userId,
                Title = "Booking Confirmed!",
                Message = $"Your booking for {eventTitle} is confirmed. " +
                            $"{ticketCount} ticket(s) have been issued.",
                Type = "Booking",
                IsRead = false,
                CreatedAt = DateTime.UtcNow
            });
        }

        public async Task SendBookingCancelledAsync(string userId, string eventTitle)
        {
            await _notificationRepository.CreateNotificationAsync(new Notification
            {
                UserId = userId,
                Title = "Booking Cancelled",
                Message = $"Your booking for {eventTitle} has been cancelled.",
                Type = "Booking",
                IsRead = false,
                CreatedAt = DateTime.UtcNow
            });
        }

        public async Task SendTicketIssuedAsync(string userId, string eventTitle)
        {
            await _notificationRepository.CreateNotificationAsync(new Notification
            {
                UserId = userId,
                Title = "Ticket Issued",
                Message = $"Your ticket for {eventTitle} has been issued. " +
                            $"Check your tickets section.",
                Type = "Ticket",
                IsRead = false,
                CreatedAt = DateTime.UtcNow
            });
        }

        public async Task SendEventReminderAsync(
            string userId, string eventTitle, DateTime eventDate)
        {
            await _notificationRepository.CreateNotificationAsync(new Notification
            {
                UserId = userId,
                Title = "Event Reminder",
                Message = $"Reminder: {eventTitle} is on " +
                            $"{eventDate:dd MMM yyyy} at {eventDate:hh:mm tt}.",
                Type = "Event",
                IsRead = false,
                CreatedAt = DateTime.UtcNow
            });
        }

        public async Task SendVendorRequestAsync(string userId, string vendorName)
        {
            await _notificationRepository.CreateNotificationAsync(new Notification
            {
                UserId = userId,
                Title = "Vendor Request Received",
                Message = $"Your service request has been sent to {vendorName}. " +
                            $"You will be notified once they respond.",
                Type = "Vendor",
                IsRead = false,
                CreatedAt = DateTime.UtcNow
            });
        }

        public async Task SendOrganizerApprovalAsync(string userId, string organizerName)
        {
            await _notificationRepository.CreateNotificationAsync(new Notification
            {
                UserId = userId,
                Title = "Organizer Request Sent",
                Message = $"Your hiring request has been sent to {organizerName}. " +
                            $"You will be notified once they respond.",
                Type = "Organizer",
                IsRead = false,
                CreatedAt = DateTime.UtcNow
            });
        }

        public async Task SendEventCancelledAsync(string userId, string eventTitle)
        {
            await _notificationRepository.CreateNotificationAsync(new Notification
            {
                UserId = userId,
                Title = "Event Cancelled",
                Message = $"Unfortunately, {eventTitle} has been cancelled by the organizer.",
                Type = "Event",
                IsRead = false,
                CreatedAt = DateTime.UtcNow
            });
        }
    }
}
