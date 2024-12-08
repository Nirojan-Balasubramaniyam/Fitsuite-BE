using GYMFeeManagement_System_BE.DTOs.Request;
using GYMFeeManagement_System_BE.DTOs.Response;
using GYMFeeManagement_System_BE.Entities;

namespace GYMFeeManagement_System_BE.IServices
{
    public interface IWorkoutPlanService
    {
        Task<PaginatedResponse<WorkoutPlanResDTO>> GetAllWorkoutPlans(int pageNumber, int pageSize);
        Task<WorkoutPlanResDTO> GetWorkoutPlanById(int trainingWorkoutPlanId);
        Task<WorkoutPlanResDTO> AddWorkoutPlan(WorkoutPlanReqDTO addWorkoutPlanReq);
        Task<WorkoutPlanResDTO> UpdateWorkoutPlan(int trainingWorkoutPlanId, WorkoutPlanReqDTO updateWorkoutPlanReq);
        Task DeleteWorkoutPlan(int trainingWorkoutPlanId);
    }
}
