namespace Event___Ticketing_Management_System.DTOs.Users
{
    public class UpdateProfileDto
    {
        public string FirstName { get; set; } = string.Empty;
        public string LastName { get; set; } = string.Empty;
        public string Gender { get; set; } = string.Empty;
        public string City { get; set; } = string.Empty;
        public string Country { get; set; } = string.Empty;
        public DateTime? DateOfBirth { get; set; }
        public string Bio { get; set; } = string.Empty;

    }
}
