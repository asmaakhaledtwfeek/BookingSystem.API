using BookingSystem.Application.Dto;
using BookingSystem.Application.Dto.Hotel;
using BookingSystem.Application.Error;
using BookingSystem.Application.Helper;
using BookingSystem.Domin.Specification.EntitiesSpecification.HotelSpacification;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookingSystem.Application.IServices.IHotelServies
{
    public interface IHotelService
    {
        public Pageination<HotelDto> GetAllHotels(HotelSpacificationParam hotelSpacificationParam);
        public Task<HotelDto?> GetHotelById(long Id);
        public Task<ApiResponse> CreateHotel(CreateHotelDto createHotelDto);
        public Task<ApiResponse> UpdateHotel(UpdateHotelDto updateHotelDto);
        public Task<ApiResponse> DeleteHotel(long Id);
    }
}
