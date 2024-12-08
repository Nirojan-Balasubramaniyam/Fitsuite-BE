using GYMFeeManagement_System_BE.Entities;

namespace GYMFeeManagement_System_BE.DTOs.Request
{
    public class EnrollProgramReqDTO
    {
        public int ProgramId { get; set; }
        public int MemberId { get; set; }
    }
}
