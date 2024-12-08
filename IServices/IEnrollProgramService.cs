using GYMFeeManagement_System_BE.DTOs.Request;
using GYMFeeManagement_System_BE.DTOs.Response;
using GYMFeeManagement_System_BE.Entities;

namespace GYMFeeManagement_System_BE.IServices
{
    public interface IEnrollProgramService
    {
        Task<List<EnrollProgramResDTO>> GetAllEnrollPrograms();
        Task<EnrollProgramResDTO> GetEnrollProgramById(int enrollId);
        Task<List<EnrollProgramResDTO>> GetEnrollProgramsByMemberId(int memberId);
        Task<List<TrainingProgramResDTO>> GetTrainingProgramsByMemberId(int memberId);
        Task<EnrollProgramResDTO> AddEnrollProgram(EnrollProgramReqDTO addEnrollProgramReq);
        Task<EnrollProgramResDTO> UpdateEnrollProgram(int enrollId, EnrollProgramReqDTO updateEnrollProgramReq);
        Task DeleteEnrollProgram(int enrollId);
    }
}
