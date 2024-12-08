using GYMFeeManagement_System_BE.DTOs.Request;
using GYMFeeManagement_System_BE.DTOs.Response;
using GYMFeeManagement_System_BE.Entities;

namespace GYMFeeManagement_System_BE.IServices
{
    public interface IWorkoutEnrollService
    {
        Task<PaginatedResponse<WorkoutEnrollmentResDTO>> GetAllWorkoutEnrollments(int pageNumber, int pageSize);
        Task<WorkoutEnrollmentResDTO> GetWorkoutEnrollmentById(int workoutEnrollId);
        Task<WorkoutEnrollmentResDTO> AddWorkoutEnrollment(WorkoutEnrollReqDTO addWorkoutEnrollmentReq);
        Task<List<WorkoutPlanResDTO>> GetWorkoutplansByMemberId(int memberId);
        Task<List<WorkoutEnrollmentResDTO>> GetWorkoutEnrollmentsByMemberId(int memberId);
        Task<WorkoutEnrollmentResDTO> UpdateWorkoutEnrollment(int workoutEnrollId, WorkoutEnrollReqDTO updateWorkoutEnrollmentReq);
        Task DeleteWorkoutEnrollment(int workoutEnrollId);
    }
}
