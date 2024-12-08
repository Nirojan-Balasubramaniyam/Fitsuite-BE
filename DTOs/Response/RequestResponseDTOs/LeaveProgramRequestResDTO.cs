using GYMFeeManagement_System_BE.DTOs.Request.RequestEntityDTOs;

namespace GYMFeeManagement_System_BE.DTOs.Response.RequestResponseDTOs
{
    public class LeaveProgramRequestResDTO : LeaveProgramRequestDTO
    {
        public int RequestId { get; set; }
        public string RequestType { get; set; }
        public string Status { get; set; }

    }
}
