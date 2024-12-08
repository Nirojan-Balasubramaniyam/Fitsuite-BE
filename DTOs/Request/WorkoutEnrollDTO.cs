using GYMFeeManagement_System_BE.Entities;

namespace GYMFeeManagement_System_BE.DTOs.Request
{
    public class WorkoutEnrollDTO
    {
        public int MemberId { get; set; }
        public DateTime EnrollmentDate { get; set; }
        public int WorkoutPlanId { get; set; }
    }
}
