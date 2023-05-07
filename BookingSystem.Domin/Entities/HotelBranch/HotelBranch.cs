using System.ComponentModel.DataAnnotations.Schema;

namespace BookingSystem.Domin.Entities
{
    public class HotelBranch : BaseEntityWithId
    {
        [ForeignKey(nameof(HotelId))]
        public virtual Hotel Hotel { get; set; }
        public long HotelId { get; set; }
        public string Name { get; set; }
        public string Address { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public string Country { get; set; }
        public string ZIPCode { get; set; }
        public string PhoneNumber { get; set; }
    }
}
