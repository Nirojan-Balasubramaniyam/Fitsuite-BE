using GYMFeeManagement_System_BE.DTOs.Request.RequestEntityDTOs;

namespace GYMFeeManagement_System_BE.DTOs.Response.RequestResponseDTOs
{
    public class NeworChangeMemberRequestResDTO : NeworChangeMemberRequestDTO
    {
        public int RequestId { get; set; }
        public string RequestType { get; set; }
        public string Status { get; set; }
    }
}
