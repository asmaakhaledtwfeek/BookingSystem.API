using BookingSystem.Application.Dto.Rooms;
using BookingSystem.Application.Helper;
using BookingSystem.Application.Services.Rooms;
using BookingSystem.Domin.Specification.EntitiesSpecification.RoomSpecification;
using Microsoft.AspNetCore.Mvc;

namespace BookingSystem.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RoomController : BaseController
    {
        private readonly RoomServices _RoomService;

        public RoomController(RoomServices RoomService)
        {
            _RoomService = RoomService;
        }

        [HttpPost("Create")]
        public async Task<ActionResult> Create(CreateRoomDto createRoomDto)
                => Ok(await _RoomService.CreateRoom(createRoomDto));

        [HttpPost("GetAll")]
        public ActionResult<Pageination<RoomDto>> GetAll([FromQuery] RoomSpecificationParam specParams)
                => Ok(_RoomService.GetAllRooms(specParams));

        [HttpPost("GetById")]
        public ActionResult GetById(long Id)
                => Ok( _RoomService.GetRoomById(Id));

        [HttpPost("Update")]
        public async Task<ActionResult> Update(UpdateRoomDto updateRoomDto)
                => Ok(await _RoomService.UpdateRoom(updateRoomDto));

        [HttpPost("Delete")]
        public async Task<ActionResult> Delete(long Id)
                => Ok(await _RoomService.DeleteRoom(Id));
    }
}
