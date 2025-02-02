using GYMFeeManagement_System_BE.DTOs.Request;
using GYMFeeManagement_System_BE.Entities;
using GYMFeeManagement_System_BE.IRepositories;
using GYMFeeManagement_System_BE.IServices;
using GYMFeeManagement_System_BE.DTOs.Response;

namespace GYMFeeManagement_System_BE.Services
{
    public class WorkoutPlanService : IWorkoutPlanService
    {
        private readonly IWorkoutPlanRepository _workoutPlanRepository;

        public WorkoutPlanService(IWorkoutPlanRepository workoutPlanRepository)
        {
            _workoutPlanRepository = workoutPlanRepository;
        }

        public async Task<PaginatedResponse<WorkoutPlanResDTO>> GetAllWorkoutPlans(int pageNumber, int pageSize)
        {
            var workoutPlanList = await _workoutPlanRepository.GetAllWorkoutPlans(pageNumber, pageSize);

            var workoutPlanResDTOList = workoutPlanList.Data.Select(workoutPlan => new WorkoutPlanResDTO
            {
                WorkoutPlanId = workoutPlan.WorkoutPlanId,
                Name = workoutPlan.Name,
                RepsCount = workoutPlan.RepsCount,
                Weight = workoutPlan.Weight,
                StaffId = workoutPlan.StaffId,
                memberId = workoutPlan.memberId,
                StartTime = workoutPlan.StartTime,
                EndTime = workoutPlan.StartTime,
                Date=workoutPlan.Date

            }).ToList();

            // Return the paginated response with DTOs
            return new PaginatedResponse<WorkoutPlanResDTO>
            {
                TotalRecords = workoutPlanList.TotalRecords,
                PageNumber = workoutPlanList.PageNumber,
                PageSize = workoutPlanList.PageSize,
                Data = workoutPlanResDTOList
            };
        }

        public async Task<WorkoutPlanResDTO> GetWorkoutPlanById(int trainingWorkoutPlanId)
        {
            var workoutPlan = await _workoutPlanRepository.GetWorkoutPlanById(trainingWorkoutPlanId);

            // Return the workout plan as a DTO
            return new WorkoutPlanResDTO
            {
                WorkoutPlanId = workoutPlan.WorkoutPlanId,
                Name = workoutPlan.Name,
                RepsCount = workoutPlan.RepsCount,
                Weight = workoutPlan.Weight,
                StaffId = workoutPlan.StaffId,
                memberId = workoutPlan.memberId,
                StartTime = workoutPlan.StartTime,
                EndTime= workoutPlan.StartTime,
                Date = workoutPlan.Date,

            };
        }


        public async Task<WorkoutPlanResDTO> AddWorkoutPlan(WorkoutPlanReqDTO addWorkoutPlanReq)
        {

            var workoutPlan = new WorkoutPlan
            {
                Name = addWorkoutPlanReq.Name,
                RepsCount = addWorkoutPlanReq.RepsCount,
                Weight = addWorkoutPlanReq.Weight,
                StaffId = addWorkoutPlanReq.StaffId,
                memberId = addWorkoutPlanReq.MemberId,

            };

            var addedWorkoutPlan = await _workoutPlanRepository.AddWorkoutPlan(workoutPlan);

            return new WorkoutPlanResDTO
            {
                WorkoutPlanId = addedWorkoutPlan.WorkoutPlanId,
                Name = addedWorkoutPlan.Name,
                RepsCount = addedWorkoutPlan.RepsCount,
                Weight = addedWorkoutPlan.Weight,
                StaffId = addedWorkoutPlan.StaffId,
                memberId = workoutPlan.memberId

            };
        }



        public async Task<WorkoutPlanResDTO> UpdateWorkoutPlan(int trainingWorkoutPlanId, WorkoutPlanReqDTO updateWorkoutPlanReq)
        {
            var existingWorkoutPlan = await _workoutPlanRepository.GetWorkoutPlanById(trainingWorkoutPlanId);
            if (existingWorkoutPlan == null)
            {
                throw new Exception("WorkoutPlan id is invalid");
            }

            if (updateWorkoutPlanReq.Name != null)
            {
                await ValidateWorkoutPlanName(updateWorkoutPlanReq.Name, trainingWorkoutPlanId);
            }


            existingWorkoutPlan.Name = updateWorkoutPlanReq.Name ?? existingWorkoutPlan.Name;
            existingWorkoutPlan.RepsCount = updateWorkoutPlanReq.RepsCount != 0 ? updateWorkoutPlanReq.RepsCount : existingWorkoutPlan.RepsCount;
            existingWorkoutPlan.Weight = updateWorkoutPlanReq.Weight != 0 ? updateWorkoutPlanReq.Weight : existingWorkoutPlan.Weight;
            existingWorkoutPlan.StaffId = updateWorkoutPlanReq.StaffId != 0 ? updateWorkoutPlanReq.StaffId : existingWorkoutPlan.StaffId;
            existingWorkoutPlan.memberId = updateWorkoutPlanReq.MemberId != 0 ? updateWorkoutPlanReq.MemberId : existingWorkoutPlan.memberId;
         


            var updatedWorkoutPlan = await _workoutPlanRepository.UpdateWorkoutPlan(existingWorkoutPlan);

            return new WorkoutPlanResDTO
            {
                WorkoutPlanId = updatedWorkoutPlan.WorkoutPlanId,
                Name = updatedWorkoutPlan.Name,
                RepsCount = updatedWorkoutPlan.RepsCount,
                Weight = updatedWorkoutPlan.Weight,
                StaffId = updatedWorkoutPlan.StaffId,
                memberId = updatedWorkoutPlan.memberId,
                StartTime = updatedWorkoutPlan.StartTime,
                EndTime = updatedWorkoutPlan.EndTime,
                Date = updatedWorkoutPlan.Date,
            };

        }

        public async Task DeleteWorkoutPlan(int trainingWorkoutPlanId)
        {
            await _workoutPlanRepository.DeleteWorkoutPlan(trainingWorkoutPlanId);
        }

        private async Task ValidateWorkoutPlanName(string workoutName, int? workoutId = null)
        {
            var existingWorkoutPlan = await _workoutPlanRepository.GetWorkoutPlanByName(workoutName);

            if (existingWorkoutPlan != null)
            {
                if (workoutId.HasValue && existingWorkoutPlan.WorkoutPlanId == workoutId.Value)
                {
                    return;
                }

                throw new ArgumentException($"{workoutName} has already registered.");
            }
        }
        public async Task<string> UpdateplanTime(int workoutPlanId, string Starting, string Ending , string date)
        {
            var data = await _workoutPlanRepository.GetWorkoutPlanById(workoutPlanId);
            if (data != null)
            {
                data.StartTime= Starting;
                data.EndTime= Ending;
                data.Date= date;
                var responseData = await _workoutPlanRepository.UpdateWorkTime(data);
                return responseData;

            }
            else
            {
                return "Workout Not Found.";
            }
        }
    }
}
