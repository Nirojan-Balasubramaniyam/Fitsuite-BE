using GYMFeeManagement_System_BE.DTOs.Request;
using GYMFeeManagement_System_BE.Entities;
using GYMFeeManagement_System_BE.IRepositories;
using GYMFeeManagement_System_BE.IServices;
using GYMFeeManagement_System_BE.Repositories;
using GYMFeeManagement_System_BE.DTOs.Response;

namespace GYMFeeManagement_System_BE.Services
{
    public class WorkoutEnrollService : IWorkoutEnrollService
    {
        private readonly IWorkoutEnrollRepository _workoutEnrollRepository;

        public WorkoutEnrollService(IWorkoutEnrollRepository workoutEnrollRepository)
        {
            _workoutEnrollRepository = workoutEnrollRepository;
        }

        public async Task<PaginatedResponse<WorkoutEnrollmentResDTO>> GetAllWorkoutEnrollments(int pageNumber, int pageSize)
        {
            var workoutEnrollList = await _workoutEnrollRepository.GetAllWorkoutEnrollments(pageNumber, pageSize);

            var workoutEnrollmentResDTOList = workoutEnrollList.Data.Select(workoutEnroll => new WorkoutEnrollmentResDTO
            {
                WorkoutEnrollId = workoutEnroll.WorkoutEnrollId,
                MemberId = workoutEnroll.MemberId,
                EnrollmentDate = workoutEnroll.EnrollmentDate,
                WorkoutPlanId = workoutEnroll.WorkoutPlanId
            }).ToList();

            // Return the paginated response with DTOs
            return new PaginatedResponse<WorkoutEnrollmentResDTO>
            {
                TotalRecords = workoutEnrollList.TotalRecords,
                PageNumber = workoutEnrollList.PageNumber,
                PageSize = workoutEnrollList.PageSize,
                Data = workoutEnrollmentResDTOList
            };
        }

        public async Task<WorkoutEnrollmentResDTO> GetWorkoutEnrollmentById(int workoutEnrollId)
        {
            var workoutEnroll = await _workoutEnrollRepository.GetWorkoutEnrollmentById(workoutEnrollId);

            var workoutEnrollmentResDTO = new WorkoutEnrollmentResDTO
            {
                WorkoutEnrollId = workoutEnroll.WorkoutEnrollId,
                MemberId = workoutEnroll.MemberId,
                EnrollmentDate = workoutEnroll.EnrollmentDate,
                WorkoutPlanId = workoutEnroll.WorkoutPlanId
            };

            return workoutEnrollmentResDTO;
        }

        public async Task<List<WorkoutEnrollmentResDTO>> GetWorkoutEnrollmentsByMemberId(int memberId)
        {
            var workoutenrollments = await _workoutEnrollRepository.GetWorkoutEnrollmentsByMemberId(memberId);

            var workoutEnrollmentResDTOList = workoutenrollments.Select(workoutEnroll => new WorkoutEnrollmentResDTO
            {
                WorkoutEnrollId = workoutEnroll.WorkoutEnrollId,
                MemberId = workoutEnroll.MemberId,
                EnrollmentDate = workoutEnroll.EnrollmentDate,
                WorkoutPlanId = workoutEnroll.WorkoutPlanId
            }).ToList();

            return workoutEnrollmentResDTOList;
        }

        public async Task<List<WorkoutPlanResDTO>> GetWorkoutplansByMemberId(int memberId)
        {
            var workoutPlans = await _workoutEnrollRepository.GetWorkoutPlansByMemberId(memberId);

            var workoutPlanResDTOList = workoutPlans.Select(workoutPlan => new WorkoutPlanResDTO
            {
                WorkoutPlanId = workoutPlan.WorkoutPlanId,
                Name = workoutPlan.Name,
                RepsCount = workoutPlan.RepsCount,
                Weight = workoutPlan.Weight,
                StaffId = workoutPlan.StaffId
            }).ToList();

            return workoutPlanResDTOList;
        }

        public async Task<WorkoutEnrollmentResDTO> AddWorkoutEnrollment(WorkoutEnrollReqDTO addWorkoutEnrollmentReq)
        {

            var workoutEnroll = new WorkoutEnrollment
            {
                MemberId = addWorkoutEnrollmentReq.MemberId,
                EnrollmentDate = addWorkoutEnrollmentReq.EnrollmentDate,
                WorkoutPlanId = addWorkoutEnrollmentReq.WorkoutPlanId
            };

            var addedWorkoutEnrollment = await _workoutEnrollRepository.AddWorkoutEnrollment(workoutEnroll);

            var workoutEnrollmentResDTO = new WorkoutEnrollmentResDTO
            {
                WorkoutEnrollId = addedWorkoutEnrollment.WorkoutEnrollId,
                MemberId = addedWorkoutEnrollment.MemberId,
                EnrollmentDate = addedWorkoutEnrollment.EnrollmentDate,
                WorkoutPlanId = addedWorkoutEnrollment.WorkoutPlanId
            };

            return workoutEnrollmentResDTO;
        }



        public async Task<WorkoutEnrollmentResDTO> UpdateWorkoutEnrollment(int workoutEnrollId, WorkoutEnrollReqDTO updateWorkoutEnrollmentReq)
        {
            var existingWorkoutEnrollment = await _workoutEnrollRepository.GetWorkoutEnrollmentById(workoutEnrollId);
            if (existingWorkoutEnrollment == null)
            {
                throw new Exception("WorkoutEnrollment id is invalid");
            }
            
            existingWorkoutEnrollment.MemberId = updateWorkoutEnrollmentReq.MemberId != 0 ? updateWorkoutEnrollmentReq.MemberId : existingWorkoutEnrollment.MemberId;
            existingWorkoutEnrollment.EnrollmentDate = updateWorkoutEnrollmentReq.EnrollmentDate != default ? updateWorkoutEnrollmentReq.EnrollmentDate : existingWorkoutEnrollment.EnrollmentDate;
            existingWorkoutEnrollment.WorkoutPlanId = updateWorkoutEnrollmentReq.WorkoutPlanId != 0 ? updateWorkoutEnrollmentReq.MemberId : existingWorkoutEnrollment.WorkoutPlanId;


            var updatedWorkoutEnrollment = await _workoutEnrollRepository.UpdateWorkoutEnrollment(existingWorkoutEnrollment);

            var workoutEnrollmentResDTO = new WorkoutEnrollmentResDTO
            {
                WorkoutEnrollId = updatedWorkoutEnrollment.WorkoutEnrollId,
                MemberId = updatedWorkoutEnrollment.MemberId,
                EnrollmentDate = updatedWorkoutEnrollment.EnrollmentDate,
                WorkoutPlanId = updatedWorkoutEnrollment.WorkoutPlanId
            };

            return workoutEnrollmentResDTO;

        }

        public async Task DeleteWorkoutEnrollment(int workoutEnrollId)
        {
            await _workoutEnrollRepository.DeleteWorkoutEnrollment(workoutEnrollId);
        }
    }
}
