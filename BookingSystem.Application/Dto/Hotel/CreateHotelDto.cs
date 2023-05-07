using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookingSystem.Application.Dto.Hotel
{
    public class CreateHotelDto
    {
        public string Name { get; set; }
        public string Address { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public string Country { get; set; }
        public string ZIPCode { get; set; }
        public string PhoneNumber { get; set; }
    }
}
