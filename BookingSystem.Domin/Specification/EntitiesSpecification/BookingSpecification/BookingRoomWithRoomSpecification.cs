using BookingSystem.Domin.Entities;

namespace BookingSystem.Domin.Specification.EntitiesSpecification.BookingSpecification
{
    public class BookingRoomWithRoomSpecification : BaseSpecifications<BookingRoom>
    {
        public BookingRoomWithRoomSpecification(long bookingId, long roomId) : base(x => x.BookingId == bookingId && x.RoomId == roomId)
        {
            AddInclude(x => x.Room);
        }
    }
}
