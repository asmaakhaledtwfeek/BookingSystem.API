using BookingSystem.Application.Dto.Branchs;
using BookingSystem.Application.Error;
using BookingSystem.Application.Helper;
using BookingSystem.Application.IServices.IHotelBranchServices;
using BookingSystem.Domin.Entities;
using BookingSystem.Domin.Interfaces;
using BookingSystem.Domin.Specification.EntitiesSpecification;
using BookingSystem.Domin.Specification.EntitiesSpecification.BranchSpacification;
using MapsterMapper;

namespace BookingSystem.Application.Services.HotelBranchServices
{
    public class HotelBranchService : IHotelBranchService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        public HotelBranchService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<ApiResponse> CreateBranch(CreateBranchDto createBranchDto)
        {
            var hotel = await _unitOfWork.Repository<Hotel>().GetEntityByIdAsync(createBranchDto.HotelId);

            if (hotel == null)
                return new ApiResponse(404, "hotel not found");

            var branch = _mapper.Map<HotelBranch>(createBranchDto);
            
            await _unitOfWork.Repository<HotelBranch>().CreateAsync(branch);
            
            var result = await _unitOfWork.Complete();

            if (result == 0)
                return new ApiResponse(400);

            return new ApiResponse(200);
        }

        public Pageination<HotelBranchDto> GetAllBranchs(BranchSpecificationParam branchSpecificationParam)
        {
            var branchsSpec = new BranchSpecification(branchSpecificationParam);

            var branchs = _unitOfWork.Repository<HotelBranch>().GetAllWithSpec(branchsSpec);

            var totalCount = _unitOfWork.Repository<HotelBranch>().GetCountWithSpec(branchsSpec);

            var BranchsDto = _mapper.Map<IReadOnlyList<HotelBranchDto>>(branchs);
            
            var result = new Pageination<HotelBranchDto>(branchSpecificationParam.PageIndex,branchSpecificationParam.PageSize,totalCount,BranchsDto);

            return result;
        }

        public async Task<HotelBranchDto?> GetBranchById(long Id)
        {
            var branchsSpec = new BranchSpecification(Id);

            var branch = _unitOfWork.Repository<HotelBranch>().GetEntityWithSpec(branchsSpec);

            if (branch == null)
                return null;
            var MappedBranch = _mapper.Map<HotelBranchDto>(branch);

            return MappedBranch;
        }

        public async Task<ApiResponse> DeleteBranch(long Id)
        {
            var branch = await _unitOfWork.Repository<HotelBranch>().GetEntityByIdAsync(Id);

            if (branch == null)
                return new ApiResponse(404);


            _unitOfWork.Repository<HotelBranch>().Delete(branch);

            var result = await _unitOfWork.Complete();
            
            if(result == 0)
                return new ApiResponse(400);

            return new ApiResponse(200);
        }

        public async Task<ApiResponse> UpdateBranch(UpdateBranchDto updateBranchDto)
        {
            if(updateBranchDto == null)
                return new ApiResponse(400);

            var branch = await _unitOfWork.Repository<HotelBranch>().GetEntityByIdAsync(updateBranchDto.Id);

            if (branch == null)
                return new ApiResponse(404);
            
            var MappedBranch = _mapper.Map<HotelBranch>(updateBranchDto);
            
            branch.UpdatedAt = DateTime.UtcNow;

            _unitOfWork.Repository<HotelBranch>().Update(MappedBranch);

            var result = await _unitOfWork.Complete();

            if (result == 0)
                return new ApiResponse(400);

            return new ApiResponse(200);
        }


    }
}
