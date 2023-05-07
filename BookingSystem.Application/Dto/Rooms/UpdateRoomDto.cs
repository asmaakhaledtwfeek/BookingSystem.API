using BookingSystem.Domin.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookingSystem.Application.Dto.Rooms
{
    public class UpdateRoomDto
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
