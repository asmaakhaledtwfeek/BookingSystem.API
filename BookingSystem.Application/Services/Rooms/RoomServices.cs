using BookingSystem.Application.Dto.Rooms;
using BookingSystem.Application.Error;
using BookingSystem.Application.Helper;
using BookingSystem.Domin.Entities;
using BookingSystem.Domin.Interfaces;
using BookingSystem.Domin.Specification.EntitiesSpecification.RoomSpecification;
using MapsterMapper;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookingSystem.Application.Services.Rooms
{
    public class RoomServices
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly UserManager<ApplicationUser> _userManager;
        public RoomServices(IUnitOfWork unitOfWork, IMapper mapper, UserManager<ApplicationUser> userManager)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _userManager = userManager;
        }

        public async Task<ApiResponse> CreateRoom(CreateRoomDto RoomDto)
        {
            var Room = _mapper.Map<Room>(RoomDto);

            var Branch = await _unitOfWork.Repository<HotelBranch>().GetEntityByIdAsync(RoomDto.BranchId);
            if (Branch == null)
            {
                return new ApiResponse(400);
            }
            await _unitOfWork.Repository<Room>().CreateAsync(Room);

            var result = await _unitOfWork.Complete();

            if (result == 0)
                return new ApiResponse(400);

            return new ApiResponse(200);
        }

        public Pageination<RoomDto> GetAllRooms(RoomSpecificationParam specPrams)
        {
            var RoomSpec = new RoomSpecification(specPrams);

            var Rooms = _unitOfWork.Repository<Room>().GetAllWithSpec(RoomSpec);

            var totalCount = _unitOfWork.Repository<Room>().GetCountWithSpec(RoomSpec);

            var RoomsDto = _mapper.Map<IReadOnlyList<RoomDto>>(Rooms);

            var result = new Pageination<RoomDto>(specPrams.PageIndex, specPrams.PageSize, totalCount, RoomsDto);

            return result;
        }

        public RoomDto? GetRoomById(long id)
        {
            var specRoom = new RoomSpecification(id);
            var Room = _unitOfWork.Repository<Room>().GetAllWithSpec(specRoom);

            if (Room == null) return null;

            var RoomDto = _mapper.Map<RoomDto>(Room);

            return RoomDto;
        }

        public async Task<ApiResponse> UpdateRoom(UpdateRoomDto updateRoomDto)
        {
            var Room = await _unitOfWork.Repository<Room>().GetEntityByIdAsync(updateRoomDto);

            if (Room == null)
                return new ApiResponse(404);

            Room.Capacity= updateRoomDto.Capacity;
            Room.Price = updateRoomDto.Price;
            Room.IsAvailable = updateRoomDto.IsAvailable;
            Room.RoomNumber = updateRoomDto.RoomNumber;
            Room.Roomtype = updateRoomDto.Roomtype;
            Room.UpdatedAt = DateTime.UtcNow;

            _unitOfWork.Repository<Room>().Update(Room);

            var result = await _unitOfWork.Complete();

            if (result == 0)
                return new ApiResponse(400);

            return new ApiResponse(200);
        }

        public async Task<ApiResponse> DeleteRoom(long id)
        {
            var Room = await _unitOfWork.Repository<Room>().GetEntityByIdAsync(id);

            if (Room == null)
                return new ApiResponse(404);
            _unitOfWork.Repository<Room>().Delete(Room);

            var result = await _unitOfWork.Complete();

            if (result == 0)
                return new ApiResponse(400);

            return new ApiResponse(200);
        }

    }
}
