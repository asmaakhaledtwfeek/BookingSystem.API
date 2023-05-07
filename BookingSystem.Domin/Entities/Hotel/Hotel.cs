namespace BookingSystem.Domin.Entities
{
    public class Hotel : BaseEntityWithId
    {
        public string Name { get; set; }
        public string Address { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public string Country { get; set; }
        public string ZIPCode { get; set; }
        public string PhoneNumber { get; set; }
        public virtual ICollection<HotelBranch>? HotelBranches{ get; set; }
    }
}
