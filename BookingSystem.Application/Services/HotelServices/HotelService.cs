using BookingSystem.Application.Dto;
using BookingSystem.Application.Dto.Branchs;
using BookingSystem.Application.Dto.Hotel;
using BookingSystem.Application.Error;
using BookingSystem.Application.Helper;
using BookingSystem.Application.IServices.IHotelServies;
using BookingSystem.Domin.Entities;
using BookingSystem.Domin.Interfaces;
using BookingSystem.Domin.Specification.EntitiesSpecification;
using BookingSystem.Domin.Specification.EntitiesSpecification.BranchSpacification;
using BookingSystem.Domin.Specification.EntitiesSpecification.HotelSpacification;
using MapsterMapper;

namespace BookingSystem.Application.Services.HotelServices
{
    public class HotelService : IHotelService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        public HotelService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<ApiResponse> CreateHotel(CreateHotelDto createHotelDto)
        {
            var hotel = _mapper.Map<Hotel>(createHotelDto);

            await _unitOfWork.Repository<Hotel>().CreateAsync(hotel);

            var result = await _unitOfWork.Complete();

            if (result == 0)
                return new ApiResponse(400);

            return new ApiResponse(200);
        }

        public Pageination<HotelDto> GetAllHotels(HotelSpacificationParam hotelSpacificationParam)
        {
            var HotelSpec = new HotelSpacification(hotelSpacificationParam);

            var hotels = _unitOfWork.Repository<Hotel>().GetAllWithSpec(HotelSpec);

            var totalCount = _unitOfWork.Repository<Hotel>().GetCountWithSpec(HotelSpec);

            var HotelsDto = _mapper.Map<IReadOnlyList<HotelDto>>(hotels);

            var result = new Pageination<HotelDto>(hotelSpacificationParam.PageIndex, hotelSpacificationParam.PageSize, totalCount, HotelsDto);

            return result;
        }

        public async Task<HotelDto?> GetHotelById(long Id)
        {
            var hotelSpac = new HotelSpacification(Id);

            var hotel = _unitOfWork.Repository<Hotel>().GetEntityWithSpec(hotelSpac);

            if (hotel == null)
                return null;

            var MappedHotel = _mapper.Map<HotelDto>(hotel);

            return MappedHotel;
        }

        public async Task<ApiResponse> DeleteHotel(long Id)
        {
            var hotel = await _unitOfWork.Repository<Hotel>().GetEntityByIdAsync(Id);

            if (hotel == null)
                return new ApiResponse(404);


            _unitOfWork.Repository<Hotel>().Delete(hotel);

            var result = await _unitOfWork.Complete();

            if (result == 0)
                return new ApiResponse(400);

            return new ApiResponse(200);
        }

        public async Task<ApiResponse> UpdateHotel(UpdateHotelDto updateHotelDto)
        {
            if (updateHotelDto == null)
                return new ApiResponse(400);

            var hotel = await _unitOfWork.Repository<Hotel>().GetEntityByIdAsync(updateHotelDto.Id);

            if (hotel == null)
                return new ApiResponse(404);

            var MappedHotel = _mapper.Map<Hotel>(updateHotelDto);

            MappedHotel.UpdatedAt = DateTime.UtcNow;

            _unitOfWork.Repository<Hotel>().Update(MappedHotel);

            var result = await _unitOfWork.Complete();

            if (result == 0)
                return new ApiResponse(400);

            return new ApiResponse(200);
        }

    }
}
