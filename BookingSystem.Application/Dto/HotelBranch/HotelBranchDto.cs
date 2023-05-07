namespace BookingSystem.Application.Dto.Branchs
{
    public class HotelBranchDto
    {
        public long Id { get; set; }
        public string Name { get; set; }
        public string Address { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public string Country { get; set; }
        public string ZIPCode { get; set; }
        public string PhoneNumber { get; set; }
        public HotelDto Hotel { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
    }
}
