using BookingSystem.Domin.Entities;
using BookingSystem.Domin.Enums;

namespace BookingSystem.Application.Dto.Rooms
{
    public class RoomDto
    {
        public long? Id { get; set; }
        public long BranchId { get; set; }
        public RoomType Roomtype { get; set; }
        public string RoomNumber { get; set; }
        public int Capacity { get; set; }
        public decimal Price { get; set; }
        public bool IsAvailable { get; set; }

    }
}
