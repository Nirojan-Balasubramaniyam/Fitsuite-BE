namespace GYMFeeManagement_System_BE.DTOs.Response
{
    public class AddressResDTO
    {
        public int AddressId { get; set; }
        public string Street { get; set; }
        public string City { get; set; }
        public string? District { get; set; }
        public string? Province { get; set; }
        public string Country { get; set; }
    }
}
