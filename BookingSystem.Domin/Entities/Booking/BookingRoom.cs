using System.ComponentModel.DataAnnotations.Schema;

namespace BookingSystem.Domin.Entities
{
    public class BookingRoom : BaseEntity
    {
        [ForeignKey(nameof(BookingId))]
        public virtual Booking Booking { get; set; }
        public long BookingId { get; set; }
        [ForeignKey(nameof(RoomId))]
        public virtual Room Room { get; set; }
        public long RoomId { get; set; }
        public DateTime CheckInDate { get; set; }
        public DateTime CheckOutDate { get; set; }
        public long CountPerson { get; set; }
        public decimal TotalPrice { get; set; }
    }
}
