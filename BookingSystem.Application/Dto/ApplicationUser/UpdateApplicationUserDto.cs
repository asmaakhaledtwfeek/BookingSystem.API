
using System.ComponentModel.DataAnnotations;

namespace BookingSystem.Application.Dto 
{
    public class UpdateApplicationUserDto
    {
        public string? UserName { get; set; }
        public string? FullName { get; set; }
        [EmailAddress]
        public string? Email { get; set; }
        [Phone]
        public string? PhoneNumber { get; set; }
    }
}
