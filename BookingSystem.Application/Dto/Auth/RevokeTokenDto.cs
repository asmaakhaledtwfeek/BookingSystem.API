using System.ComponentModel.DataAnnotations;

namespace BookingSystem.Application.Dto 
{
    public class RevokeTokenDto
    {
        [Required]
        public string Token { get; set; }
    }
}
