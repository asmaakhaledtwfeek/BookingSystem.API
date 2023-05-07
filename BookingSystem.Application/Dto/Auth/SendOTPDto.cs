using System.ComponentModel.DataAnnotations;

namespace BookingSystem.Application.Application.Dto
{
    public class SendOTPDto
    {
        [Required]
        [EmailAddress]
        public string Email { get; set; }
    }
}
