using BookingSystem.Domin.Enums;
using System.ComponentModel.DataAnnotations.Schema;

namespace BookingSystem.Domin.Entities
{
    public class Room: BaseEntityWithId
    {
        [ForeignKey(nameof(BranchId))]
        public virtual HotelBranch Branch { get; set; }
        public long BranchId { get; set; }
        public RoomType Roomtype { get; set; }
        public string RoomNumber { get; set; }
        public int Capacity { get; set; }
        public decimal Price { get; set;}
        public bool IsAvailable { get; set; } = true;
    }
}
