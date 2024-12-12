using GYMFeeManagement_System_BE.DTOs.Response;
using GYMFeeManagement_System_BE.Entities;

namespace GYMFeeManagement_System_BE.IRepositories
{
    public interface ITrainingProgramRepository
    {
        Task<TrainingProgram> AddProgram(TrainingProgram trainingProgram);
        Task<ICollection<TrainingProgram>> GetAllPrograms();
        Task<PaginatedResponse<TrainingProgram>> GetAllPrograms(int pageNumber, int pageSize);
        Task<TrainingProgram> GetProgramById(int trainingProgramId);
        //Task<List<TrainingProgram>> GetProgramsByMemberId(int memberId);
        Task<TrainingProgram> UpdateProgram(TrainingProgram updateTrainingProgram);
        Task<TrainingProgram> GetProgramByName(string programName);
        Task DeleteProgram(int trainingProgramId);

    }
}
