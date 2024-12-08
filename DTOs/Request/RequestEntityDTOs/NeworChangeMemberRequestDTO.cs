namespace GYMFeeManagement_System_BE.DTOs.Request.RequestEntityDTOs
{
    public class NeworChangeMemberRequestDTO
    {
        public int? MemberId { get; set; }
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        public string? Email { get; set; }
        public string? Phone { get; set; }
        public string? NIC { get; set; }
        public DateTime? DOB { get; set; }
        public string? Gender { get; set; }
        public AddressReqDTO? Address { get; set; }
        public IFormFile? ImageFile { get; set; }
        public string? EmergencyContactName { get; set; }
        public string? EmergencyContactNumber { get; set; }
        public string? Password { get; set; }
    }
}
