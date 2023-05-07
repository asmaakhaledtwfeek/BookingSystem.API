using BookingSystem.Application.Dto.Branchs;
using BookingSystem.Application.Helper;
using BookingSystem.Application.IServices.IHotelBranchServices;
using BookingSystem.Domin.Specification.EntitiesSpecification.BranchSpacification;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BookingSystem.API.Controllers
{
    public class BranchController : BaseController
    {
        private readonly IHotelBranchService _branchService;

        public BranchController(IHotelBranchService branchService)
        {
            _branchService = branchService;
        }

        [HttpPost("Create")]
        public async Task<ActionResult> Create(CreateBranchDto createBranchDto)
                => Ok(await _branchService.CreateBranch(createBranchDto));

        [HttpGet("GetAll")]
        public ActionResult<Pageination<HotelBranchDto>> GetAll([FromQuery] BranchSpecificationParam specParams)
                => Ok(_branchService.GetAllBranchs(specParams));

        [HttpGet("GetById")]
        public async Task<ActionResult> GetById(long Id)
                => Ok(await _branchService.GetBranchById(Id));

        [HttpPut("Update")]
        public async Task<ActionResult> Update(UpdateBranchDto updateBranchDto)
                => Ok(await _branchService.UpdateBranch(updateBranchDto));

        [HttpPost("Delete")]
        public async Task<ActionResult> Delete(long Id)
                => Ok(await _branchService.DeleteBranch(Id));

    }
}
