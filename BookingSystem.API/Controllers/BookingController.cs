using BookingSystem.Application.Dto;
using BookingSystem.Application.Error;
using BookingSystem.Application.Helper;
using BookingSystem.Application.IServices.IBooking;
using BookingSystem.Domin.Specification.EntitiesSpecification.BookingSpecification;
using Microsoft.AspNetCore.Mvc;

namespace BookingSystem.API.Controllers
{
    public class BookingController : BaseController
    {
        private readonly IBookingService _bookingService;

        public BookingController(IBookingService bookingService)
        {
            _bookingService = bookingService;
        }

        [HttpPost("Create")]
        public async Task<ActionResult<ApiResponse>> CreateBooking(CreateBookingDto bookingDto)
            => Ok(await _bookingService.CreateBooking(bookingDto));

        [HttpGet("GetAll")]
        public ActionResult<Pageination<BookingDto>> GetAll([FromQuery] BookingSpecificationPram specPram)
            => Ok(_bookingService.GetAllBooking(specPram));

        [HttpGet("GetById")]
        public ActionResult<BookingDto?> GetById(long id)
        {
            var result = _bookingService.GetBookingById(id);

            if (result == null)
                return Ok(new ApiResponse(404));

            return Ok(result);
        }

        [HttpDelete("Delete")]
        public ActionResult<BookingDto?> Delete(long id)
            => Ok(_bookingService.DeleteBooking(id));
    }
}
