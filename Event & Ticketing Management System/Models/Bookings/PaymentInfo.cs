using Event___Ticketing_Management_System.Utilities;

namespace Event___Ticketing_Management_System.Models.Bookings
{
    public class PaymentInfo
    {
        public string PaymentStatus { get; set; } = PaymentStatusConstatants.Pending; // Pending, Completed, Failed, Refunded

        public string PaymentMethod { get; set; } = PaymentMethodConstants.None; // Card, JazzCash, EasyPaisa, etc.

        public string TransactionId { get; set; } = string.Empty;

        public DateTime? PaidAt { get; set; }

    }

    public class PaymentMethodConstants
    {
        public const string Card = "Card";
        public const string JazzCash = "JazzCash";
        public const string EasyPaisa = "EasyPaisa";
        public const string None = "None";
    }

}
