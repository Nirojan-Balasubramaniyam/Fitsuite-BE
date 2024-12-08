using GYMFeeManagement_System_BE.Entities;

namespace GYMFeeManagement_System_BE.DTOs.Response
{
    public class EnrollProgramResDTO
    {
        public int EnrollId { get; set; }
        public int ProgramId { get; set; }
        public int MemberId { get; set; }
    }
}
