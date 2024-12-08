namespace GYMFeeManagement_System_BE.DTOs.Request.RequestEntityDTOs
{
    public class LeaveProgramRequestDTO
    {
        public int RequestId { get; set; }
        public int? MemberId { get; set; }
        public int? ProgramId { get; set; }
    }
}
