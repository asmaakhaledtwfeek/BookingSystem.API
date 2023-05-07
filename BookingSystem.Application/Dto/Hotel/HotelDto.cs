using BookingSystem.Application.Dto.Branchs;

namespace BookingSystem.Application.Dto
{
    public class HotelDto
    {
        public long Id { get; set; }
        public string Name { get; set; }
        public string Address { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public string Country { get; set; }
        public string ZIPCode { get; set; }
        public string PhoneNumber { get; set; }
        public List<HotelBranchDto> HotelBranches { get; set; } = new List<HotelBranchDto>();
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
    }
}
