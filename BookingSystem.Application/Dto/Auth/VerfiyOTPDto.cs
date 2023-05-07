using System.ComponentModel.DataAnnotations;

namespace BookingSystem.Application.Application.Dto
{
    public class VerfiyOTPDto
    {
        [Required]
        [EmailAddress]
        public string Email { get; set; }
        [Required]
        public string OTP { get; set; }
    }
}
