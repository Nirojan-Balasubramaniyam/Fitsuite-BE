using GYMFeeManagement_System_BE.Entities;

namespace GYMFeeManagement_System_BE.DTOs.Request
{
    public class MemberReqDTO
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string? Password { get; set; }
        public string NIC { get; set; }
        public string Phone { get; set; }
        public DateTime DoB { get; set; }
        public string Gender { get; set; }
        public string EmergencyContactName { get; set; }
        public string EmergencyContactNumber { get; set; }
        public int ? Bmi { get; set; }
        public IFormFile? ImageFile { get; set; }
        public int? StaffId { get; set; }
        public int BranchId { get; set; }
        public AddressReqDTO Address { get; set; }
        public bool IsActive { get; set; }

    }
}
