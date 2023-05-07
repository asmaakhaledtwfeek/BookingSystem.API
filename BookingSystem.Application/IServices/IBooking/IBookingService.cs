using BookingSystem.Application.Dto;
using BookingSystem.Application.Error;
using BookingSystem.Application.Helper;
using BookingSystem.Domin.Specification.EntitiesSpecification.BookingSpecification;

namespace BookingSystem.Application.IServices.IBooking
{
    public interface IBookingService
    {
        public Task<ApiResponse> CreateBooking(CreateBookingDto bookingDto);
        public Pageination<BookingDto> GetAllBooking(BookingSpecificationPram specPram);
        public BookingDto? GetBookingById(long Id);
        public Task<ApiResponse> DeleteBooking(long Id);
    }
}
