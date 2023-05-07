using System.ComponentModel.DataAnnotations;

namespace BookingSystem.Application.Dto 
{
    public class RegisterDto
    {
        [Required(ErrorMessage = "full name is required")]
        public string FullName { get; set; }
        [Required(ErrorMessage = "user name is required")]
        public string Username { get; set; }
        [Required(ErrorMessage = "email address is required")]
        [EmailAddress] public string Email { get; set; }
        [Required(ErrorMessage = "phone number is required")]
        [Phone] public string PhoneNumber { get; set; }
        [Required(ErrorMessage = "password is required")]
        public string Password { get; set; }
        [Required(ErrorMessage = "user role is required")]
        public string UserRole { get; set; }
    }
}
