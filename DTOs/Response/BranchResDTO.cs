using GYMFeeManagement_System_BE.Entities;

namespace GYMFeeManagement_System_BE.DTOs.Response
{
    public class BranchResDTO
    {
        public int BranchId { get; set; }
        public string BranchName { get; set; }
        public int BranchAdminId { get; set; }
        public string AdminName { get; set; }
        public int ActiveMembers { get; set; }
        public int LeavedMembers { get; set; }
        public bool IsActive { get; set; }
        public AddressResDTO? Address { get; set; }
    }
}
