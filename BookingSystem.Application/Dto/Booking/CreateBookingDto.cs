namespace BookingSystem.Application.Dto
{
    public class CreateBookingDto
    {
        public string CustomerUserName { get; set; }
        public long HotelBranchId { get; set; }
        public List<CreateBookingRoomDto> BookingRooms { get; set; } = new List<CreateBookingRoomDto>();
    }
}
