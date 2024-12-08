namespace GYMFeeManagement_System_BE.DTOs.Request
{
    public class WorkoutPlanReqDTO
    {
        public string Name { get; set; }
        public int RepsCount { get; set; }
        public float Weight { get; set; }
        public int StaffId { get; set; }
    }
}
