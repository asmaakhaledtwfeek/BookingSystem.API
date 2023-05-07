using BookingSystem.Application.Dto;
using BookingSystem.Application.Error;
using BookingSystem.Application.Helper;
using BookingSystem.Application.IServices.IBooking;
using BookingSystem.Domin.Entities;
using BookingSystem.Domin.Interfaces;
using BookingSystem.Domin.Specification.EntitiesSpecification.BookingSpecification;
using MapsterMapper;
using Microsoft.AspNetCore.Identity;

namespace BookingSystem.Application.Services
{
    public class BookingService : IBookingService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly UserManager<ApplicationUser> _userManager;

        public BookingService(IUnitOfWork unitOfWork, IMapper mapper, UserManager<ApplicationUser> userManager)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _userManager = userManager;
        }

        public async Task<ApiResponse> CreateBooking(CreateBookingDto bookingDto)
        {
            var customer = await _userManager.FindByNameAsync(bookingDto.CustomerUserName);
            
            if (customer == null)
                return new ApiResponse(404, "customer not found");

            var hotelBranch = await _unitOfWork.Repository<HotelBranch>().GetEntityByIdAsync(bookingDto.HotelBranchId);

            if (hotelBranch == null)
                return new ApiResponse(404, "hotel branch not found");

            var booking = new Booking()
            {
                CustomerId = customer.Id,
                HotelBranchId = hotelBranch.Id,
                SubTotalPrice = bookingDto.BookingRooms.Sum(x => x.TotalPrice),
            };

            await _unitOfWork.Repository<Booking>().CreateAsync(booking);

            var createBookingResult = await _unitOfWork.Complete();

            if (createBookingResult == 0)
                return new ApiResponse(400);

            foreach(var bookingRoom in bookingDto.BookingRooms)
            {
                var room = await _unitOfWork.Repository<Room>().GetEntityByIdAsync(bookingRoom.RoomId);

                if (room == null)
                    return new ApiResponse(404, $"room with id {bookingRoom.RoomId} not found");

                if(!room.IsAvailable)
                    return new ApiResponse(404, $"room with id {bookingRoom.RoomId} not available");

                var bookingRoomEntity = _mapper.Map<BookingRoom>(bookingRoom);
                
                bookingRoomEntity.BookingId = booking.Id;

                await _unitOfWork.Repository<BookingRoom>().CreateAsync(bookingRoomEntity);
            }

            var createBookingRoomsResult = await _unitOfWork.Complete();

            if (createBookingRoomsResult == 0)
                return new ApiResponse(400);

            return new ApiResponse(200);
        }

        public Pageination<BookingDto> GetAllBooking(BookingSpecificationPram specPram)
        {
            var bookingSpec = new BookingWithCustomerAndHotelBranchAndBookingRoomsSpecification(specPram);

            var bookings = _unitOfWork.Repository<Booking>().GetAllWithSpec(bookingSpec);

            var totalCount = _unitOfWork.Repository<Booking>().GetCountWithSpec(bookingSpec);

            var bookingsDto = _mapper.Map<IReadOnlyList<BookingDto>>(bookings);

            var result = new Pageination<BookingDto>(specPram.PageIndex, specPram.PageSize, totalCount, bookingsDto);

            return result;
        }

        public BookingDto? GetBookingById(long Id)
        {
            var bookingSpec = new BookingWithCustomerAndHotelBranchAndBookingRoomsSpecification(Id);

            var booking = _unitOfWork.Repository<Booking>().GetAllWithSpec(bookingSpec);

            if (booking == null)
                return null;

            var bookingDto = _mapper.Map<BookingDto>(booking);

            return bookingDto;
        }

        public async Task<ApiResponse> DeleteBooking(long Id)
        {
            var booking = await _unitOfWork.Repository<Booking>().GetEntityByIdAsync(Id);

            if (booking == null)
                return new ApiResponse(404);

             _unitOfWork.Repository<Booking>().Delete(booking);

            var result = await _unitOfWork.Complete();

            if(result == 0)
                return new ApiResponse(400);

            return new ApiResponse(200);
        }

    }
}
