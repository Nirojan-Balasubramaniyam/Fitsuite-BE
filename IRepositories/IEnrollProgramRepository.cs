using GYMFeeManagement_System_BE.DTOs.Response;
using GYMFeeManagement_System_BE.Entities;

namespace GYMFeeManagement_System_BE.IRepositories
{
    public interface IEnrollProgramRepository
    {
        Task<EnrollProgram> AddEnrollProgram(EnrollProgram enrollProgram);
        Task<ICollection<EnrollProgram>> GetAllEnrollPrograms();
        //Task<PaginatedResponse<EnrollProgram>> GetAllEnrollPrograms(int pageNumber, int pageSize);
        Task<EnrollProgram> GetEnrollProgramById(int enrollId);
        Task<List<TrainingProgram>> GetTrainingProgramsByMemberId(int memberId);
        Task<List<EnrollProgram>> GetEnrollProgramsByMemberId(int memberId);
        Task<EnrollProgram> UpdateEnrollProgram(EnrollProgram updateEnrollProgram);
        Task DeleteEnrollProgram(int enrollProgramId);
    }
}
