using System.ComponentModel.DataAnnotations;

namespace BookingSystem.Application.Dto 
{
    public class ChangePasswordDto
    {
        [Required(ErrorMessage = "username is required")]
        public string Username { get; set; }
        [Required(ErrorMessage = "old password is required")]
        public string OldPassword { get; set; }
        [Required(ErrorMessage = "new password is required")]
        public string NewPassword { get; set; }
    }
}
