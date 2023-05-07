using Microsoft.AspNetCore.Identity;

namespace BookingSystem.Domin.Entities
{
    public class ApplicationUser : IdentityUser
    {
        public string FullName { get; set; }
        public string? OTP { get; set; }
        public DateTime? OTPExpirationDate { get; set; }
        public bool HasBookedPreviously { get; set; } = false;
    }
}
