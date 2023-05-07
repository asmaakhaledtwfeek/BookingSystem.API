using System.ComponentModel.DataAnnotations.Schema;

namespace BookingSystem.Domin.Entities
{
    public class Booking : BaseEntityWithId
    {
        [ForeignKey(nameof(CustomerId))]
        public virtual ApplicationUser Customer { get; set; }
        public string CustomerId { get; set; }
        [ForeignKey(nameof(HotelBranchId))]
        public virtual HotelBranch HotelBranch { get; set; }
        public long HotelBranchId { get; set; }
        public decimal SubTotalPrice { get; set; }
        public virtual ICollection<BookingRoom>? BookingRooms { get; set; }
    }
}
