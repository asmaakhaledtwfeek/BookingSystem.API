namespace BookingSystem.Application.Dto
{
    public class BookingRoomDto
    {
        public long Id { get; set; }
        public long RoomId { get; set; }
        public DateTime CheckInDate { get; set; }
        public DateTime CheckOutDate { get; set; }
        public long CountPerson { get; set; }
        public decimal TotalPrice { get; set; }
    }
}
