using GYMFeeManagement_System_BE.Entities;

namespace GYMFeeManagement_System_BE.DTOs.Request
{
    public class BranchReqDTO
    {
        public string BranchName { get; set; }
        public int BranchAdminId { get; set; }
        public AddressReqDTO? Address { get; set; }
    }
}
