using GYMFeeManagement_System_BE.Entities;

namespace GYMFeeManagement_System_BE.DTOs.Response
{
    public class MemberResDTO
    {
        public int MemberId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string NIC { get; set; }
        public string Phone { get; set; }
        public DateTime DoB { get; set; }
        public string Gender { get; set; }
        public string EmergencyContactName { get; set; }
        public string EmergencyContactNumber { get; set; }
        public decimal MonthlyPayment { get; set; }
        public int? Bmi {  get; set; }
        public string? ImagePath { get; set; }
        public int? TrainerId { get; set; }
        public int BranchId { get; set; }
        public bool IsActive { get; set; }
        public AddressResDTO? Address { get; set; }
    }
}
