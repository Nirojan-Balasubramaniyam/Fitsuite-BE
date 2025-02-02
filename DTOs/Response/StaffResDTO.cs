using GYMFeeManagement_System_BE.Entities;

namespace GYMFeeManagement_System_BE.DTOs.Response
{
    public class StaffResDTO
    {
        public int StaffId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string NIC { get; set; }
        public string Phone { get; set; }
        public DateTime DoB { get; set; }
        public string Gender { get; set; }
        public string? ImagePath { get; set; }
        public UserRole UserRole { get; set; }
        public int BranchId { get; set; }
        public AddressResDTO? Address { get; set; }


    }
}
