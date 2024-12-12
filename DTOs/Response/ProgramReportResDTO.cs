namespace GYMFeeManagement_System_BE.DTOs.Response
{
    public class ProgramReportResDTO
    {
        public int TypeId { get; set; }
        public string TypeName { get; set; }
        public int TotalMembers { get; set; }
        public int TotalEnrollingMembers { get; set; }
        public decimal FollowersPercentage { get; set; }
        public List<ProgramDetailResDTO> Programs { get; set; }
    }
}
