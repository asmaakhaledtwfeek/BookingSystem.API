using BookingSystem.Application.Dto.Branchs;
using BookingSystem.Application.Error;
using BookingSystem.Application.Helper;
using BookingSystem.Domin.Specification.EntitiesSpecification.BranchSpacification;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookingSystem.Application.IServices.IHotelBranchServices
{
    public interface IHotelBranchService
    {
        public Pageination<HotelBranchDto> GetAllBranchs(BranchSpecificationParam branchSpecificationParam);
        public Task<HotelBranchDto?> GetBranchById(long Id);
        public Task<ApiResponse> CreateBranch(CreateBranchDto createBranchDto);
        public Task<ApiResponse> UpdateBranch(UpdateBranchDto updateBranchDto);
        public Task<ApiResponse> DeleteBranch(long Id);
    }
}
