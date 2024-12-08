using GYMFeeManagement_System_BE.Entities;

namespace GYMFeeManagement_System_BE.DTOs.Response
{
    public class WorkoutPlanResDTO
    {
        public int WorkoutPlanId { get; set; }
        public string Name { get; set; }
        public int RepsCount { get; set; }
        public float Weight { get; set; }
        public int StaffId { get; set; }
    }
}
