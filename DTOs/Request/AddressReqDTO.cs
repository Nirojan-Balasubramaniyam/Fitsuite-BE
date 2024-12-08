namespace GYMFeeManagement_System_BE.DTOs.Request
{
    public class AddressReqDTO
    {
        public string Street { get; set; }
        public string City { get; set; }
        public string? District { get; set; }
        public string? Province { get; set; }
        public string Country { get; set; }
    }
}
