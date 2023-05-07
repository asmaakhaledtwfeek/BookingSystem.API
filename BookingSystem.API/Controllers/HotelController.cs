using BookingSystem.Application.Dto.Branchs;
using BookingSystem.Application.Dto.Hotel;
using BookingSystem.Application.Helper;
using BookingSystem.Application.IServices.IHotelServies;
using BookingSystem.Domin.Entities;
using BookingSystem.Domin.Specification.EntitiesSpecification.BranchSpacification;
using BookingSystem.Domin.Specification.EntitiesSpecification.HotelSpacification;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Security.AccessControl;

namespace BookingSystem.API.Controllers
{
    public class HotelController : BaseController
    {
        private readonly IHotelService _hotelService;
        public HotelController(IHotelService hotelService)
        {
            _hotelService = hotelService;
        }

        [HttpPost("Create")]
        public async Task<ActionResult> Create(CreateHotelDto createHotelDto)
                => Ok(await _hotelService.CreateHotel(createHotelDto));

        [HttpGet("GetAll")]
        public ActionResult<Pageination<Hotel>> GetAll([FromQuery] HotelSpacificationParam specParams)
                => Ok(_hotelService.GetAllHotels(specParams));

        [HttpGet("GetById")]
        public async Task<ActionResult> GetById(long Id)
                => Ok(await _hotelService.GetHotelById(Id));

        [HttpPut("Update")]
        public async Task<ActionResult> Update(UpdateHotelDto updateHotelDto)
                => Ok(await _hotelService.UpdateHotel(updateHotelDto));

        [HttpPost("Delete")]
        public async Task<ActionResult> Delete(long Id)
                => Ok(await _hotelService.DeleteHotel(Id));
    }
}
