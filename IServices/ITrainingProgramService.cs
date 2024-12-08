using GYMFeeManagement_System_BE.DTOs.Request;
using GYMFeeManagement_System_BE.DTOs.Response;
using GYMFeeManagement_System_BE.Entities;

namespace GYMFeeManagement_System_BE.IServices
{
    public interface ITrainingProgramService
    {
        Task<PaginatedResponse<TrainingProgramResDTO>> GetAllTrainingPrograms(int pageNumber, int pageSize);
        Task<TrainingProgramResDTO> GetTrainingProgramById(int trainingProgramId);
        Task<TrainingProgramResDTO> AddTrainingProgram(TrainingProgramReqDTO addTrainingProgramReq);
        Task<TrainingProgramResDTO> UpdateTrainingProgram(int trainingProgramId, TrainingProgramReqDTO updateTrainingProgramReq);
        Task DeleteTrainingProgram(int trainingProgramId);
        //Task<List<TrainingProgram>> GetProgramsByMemberId(int memberId);
    }
}
