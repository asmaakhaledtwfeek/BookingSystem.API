using System.ComponentModel.DataAnnotations;

namespace BookingSystem.Application.Dto
{
    public class CreateBookingRoomDto
    {
        [Required]
        public long RoomId { get; set; }
        [Required]
        public DateTime CheckInDate { get; set; }
        [Required]
        public DateTime CheckOutDate { get; set; }
        [Required]
        public long CountPerson { get; set; }
        [Required]
        public decimal TotalPrice { get; set; }
    }
}
