using System.ComponentModel.DataAnnotations;

namespace BookingSystem.Application.Dto 
{
    public class LoginDto
    {
        [Required(ErrorMessage = "email address is required")]
        [EmailAddress] public string Email { get; set; }
        [Required(ErrorMessage = "password is required")]
        public string Password { get; set; }
    }
}
