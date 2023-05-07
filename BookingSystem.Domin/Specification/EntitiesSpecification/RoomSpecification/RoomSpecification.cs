using BookingSystem.Domin.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookingSystem.Domin.Specification.EntitiesSpecification.RoomSpecification
{
    public class RoomSpecification: BaseSpecifications<Room>
    {
        public RoomSpecification(RoomSpecificationParam specPram) : base(x=>x.Id!=0)
        {
            ApplyPaging(specPram.PageSize * (specPram.PageIndex - 1),
                        specPram.PageSize);

            AddOrderByDescending(x => x.CreatedAt);
           
        }
        public RoomSpecification(long Id) : base(x => x.Id == Id)
        {
           

            AddOrderByDescending(x => x.CreatedAt);
        }
    }
}
