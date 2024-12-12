namespace GYMFeeManagement_System_BE.DTOs.Response
{
    public class ProgramDetailResDTO
    {
        public int ProgramId { get; set; }
        public string ProgramName { get; set; }
        public int TypeId { get; set; }
        public decimal FollowerPercentage { get; set; }
        public int TotalMembers { get; set; }
        public int TotalEnrollingMembers { get; set; }
    }
}
