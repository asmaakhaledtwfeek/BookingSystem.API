namespace BookingSystem.Application.Dto 
{
    public class ApplicationUserDto
    {
        public string UserName { get; set; }
        public string FullName { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
        public bool HasBookedPreviously { get; set; }
    }
}
