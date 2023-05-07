using BookingSystem.Domin.Entities;
using System.Security.Cryptography.X509Certificates;

namespace BookingSystem.Domin.Specification.EntitiesSpecification.BranchSpacification
{
    public class BranchSpecification : BaseSpecifications<HotelBranch>
    {
        public BranchSpecification(BranchSpecificationParam branchSpecificationParam) : base(x => string.IsNullOrEmpty(branchSpecificationParam.Search) || x.Name.ToLower().Contains(branchSpecificationParam.Search.ToLower()))
        {
            ApplyPaging(branchSpecificationParam.PageSize * (branchSpecificationParam.PageIndex - 1), branchSpecificationParam.PageSize);

            AddOrderByDescending(x => x.CreatedAt);

            AddInclude(x => x.Hotel);
        }
        public BranchSpecification(long id) : base(x => x.Id == id)
        {
            AddInclude(x => x.Hotel);
        }
    }
}
