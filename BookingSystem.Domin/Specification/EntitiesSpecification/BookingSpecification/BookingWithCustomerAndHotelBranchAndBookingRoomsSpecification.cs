using BookingSystem.Domin.Entities;

namespace BookingSystem.Domin.Specification.EntitiesSpecification.BookingSpecification
{
    public class BookingWithCustomerAndHotelBranchAndBookingRoomsSpecification : BaseSpecifications<Booking>
    {
        public BookingWithCustomerAndHotelBranchAndBookingRoomsSpecification(BookingSpecificationPram specPram) 
            : base(x => string.IsNullOrEmpty(specPram.CustomerUsername) || x.Customer.UserName == specPram.CustomerUsername)
        {

            ApplyPaging(specPram.PageSize * (specPram.PageIndex - 1), specPram.PageSize);

            AddInclude(x => x.Customer);

            AddInclude(x => x.BookingRooms!);

            AddInclude(x => x.HotelBranch);

            AddOrderByDescending(x => x.CreatedAt);

        }

        public BookingWithCustomerAndHotelBranchAndBookingRoomsSpecification(long bookingId) : base(x => x.Id == bookingId)
        {
            AddInclude(x => x.Customer);

            AddInclude(x => x.BookingRooms!);

            AddInclude(x => x.HotelBranch);
        }
    }
}
