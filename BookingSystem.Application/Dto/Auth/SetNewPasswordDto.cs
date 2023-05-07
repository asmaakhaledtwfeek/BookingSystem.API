using System.ComponentModel.DataAnnotations;

namespace BookingSystem.Application.Application.Dto
{
    public class SetNewPasswordDto
    {
        [Required]
        [EmailAddress]
        public string Email { get; set; }

        [Required]
        public string Password { get; set; }
    }
}
