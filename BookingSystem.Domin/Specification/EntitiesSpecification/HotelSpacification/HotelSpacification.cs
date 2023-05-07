using BookingSystem.Domin.Entities;
using BookingSystem.Domin.Specification.EntitiesSpecification.BranchSpacification;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace BookingSystem.Domin.Specification.EntitiesSpecification.HotelSpacification
{
    public class HotelSpacification : BaseSpecifications<Hotel>
    {
        public HotelSpacification(HotelSpacificationParam hotelSpacificationParam) : base(x => string.IsNullOrEmpty(hotelSpacificationParam.Search) || x.Name.ToLower().Contains(hotelSpacificationParam.Search.ToLower()))
        {
            ApplyPaging(hotelSpacificationParam.PageSize * (hotelSpacificationParam.PageIndex - 1), hotelSpacificationParam.PageSize);

            AddOrderByDescending(x => x.CreatedAt);

            AddInclude(x => x.HotelBranches);
        }

        public HotelSpacification(long id):base(x => x.Id == id)
        {
            AddInclude(x => x.HotelBranches);
        }
    }
}
