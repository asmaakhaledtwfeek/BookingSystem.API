using System.ComponentModel.DataAnnotations;

namespace BookingSystem.Application.Dto 
{
    public class RefreshTokenDto
    {
        [Required]
        public string Token { get; set; }
    }
}
