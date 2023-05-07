using BookingSystem.Application.Dto.Branchs;
namespace BookingSystem.Application.Dto
{
    public class BookingDto
    {
        public long Id { get; set; }
        public ApplicationUserDto Customer { get; set; }
        public HotelBranchDto HotelBranch { get; set; }
        public List<BookingRoomDto> BookingRooms { get; set; } = new List<BookingRoomDto>();
        public decimal SubTotalPrice { get; set; }
    }
}
