using GYMFeeManagement_System_BE.DTOs.Request;
using GYMFeeManagement_System_BE.DTOs.Response;
using GYMFeeManagement_System_BE.Entities;
using GYMFeeManagement_System_BE.IRepositories;
using GYMFeeManagement_System_BE.IServices;

namespace GYMFeeManagement_System_BE.Services
{
    public class TrainingProgramService : ITrainingProgramService
    {
        private readonly ITrainingProgramRepository _trainingProgramRepository;
        private readonly IWebHostEnvironment _webHostEnvironment;
        private readonly IConfiguration _configuration;

        public TrainingProgramService(ITrainingProgramRepository trainingProgramRepository, IWebHostEnvironment webHostEnvironment, IConfiguration configuration)
        {
            _trainingProgramRepository = trainingProgramRepository;
            _webHostEnvironment = webHostEnvironment;
            _configuration = configuration;
        }

        public async Task<PaginatedResponse<TrainingProgramResDTO>> GetAllTrainingPrograms(int pageNumber, int pageSize)
        {
            var trainingProgramList = await _trainingProgramRepository.GetAllPrograms( pageNumber, pageSize);

            var trainingProgramResDTOList = trainingProgramList.Data.Select(trainingProgram => new TrainingProgramResDTO
            {
                ProgramId = trainingProgram.ProgramId,
                TypeId = trainingProgram.TypeId,
                ProgramName = trainingProgram.ProgramName,
                Cost = trainingProgram.Cost,
                Description = trainingProgram.Description,
                ImagePath = trainingProgram.ImagePath
            }).ToList();

            
            return new PaginatedResponse<TrainingProgramResDTO>
            {
                TotalRecords = trainingProgramList.TotalRecords,
                PageNumber = trainingProgramList.PageNumber,
                PageSize = trainingProgramList.PageSize,
                Data = trainingProgramResDTOList
            };
        }

        public async Task<TrainingProgramResDTO> GetTrainingProgramById(int trainingProgramId)
        {
            var trainingProgram = await _trainingProgramRepository.GetProgramById(trainingProgramId);

            var trainingProgramResDTO = new TrainingProgramResDTO
            {
                ProgramId = trainingProgram.ProgramId,
                TypeId = trainingProgram.TypeId,
                ProgramName = trainingProgram.ProgramName,
                Cost = trainingProgram.Cost,
                Description = trainingProgram.Description,
                ImagePath = trainingProgram.ImagePath
            };

            return trainingProgramResDTO;
        }



        public async Task<TrainingProgramResDTO> AddTrainingProgram(TrainingProgramReqDTO addTrainingProgramReq)
        {

            // Validate the name uniqueness
            await ValidateProgramName(addTrainingProgramReq.ProgramName);
            if (!addTrainingProgramReq.Cost.HasValue)
            {
                throw new Exception("Please Enter Program Cost");
            }

            if (!addTrainingProgramReq.TypeId.HasValue)
            {
                throw new Exception("Please Enter Program TypeID");
            }

            var trainingProgram = new TrainingProgram
            {
                TypeId = (int)addTrainingProgramReq.TypeId,
                ProgramName = addTrainingProgramReq.ProgramName,
                Cost = (decimal)addTrainingProgramReq.Cost
            };

            if (addTrainingProgramReq.ImageFile != null)
            {
                trainingProgram.ImagePath = await SaveImageFileAsync(addTrainingProgramReq.ImageFile);
            }

            var addedTrainingProgram = await _trainingProgramRepository.AddProgram(trainingProgram);


            var trainingProgramResDTO = new TrainingProgramResDTO
            {
                ProgramId = addedTrainingProgram.ProgramId,
                TypeId = addedTrainingProgram.TypeId,
                ProgramName = addedTrainingProgram.ProgramName,
                Cost = addedTrainingProgram.Cost,
                Description = addedTrainingProgram.Description,
                ImagePath = addedTrainingProgram.ImagePath
            };

            return trainingProgramResDTO;
        }



    public async Task<TrainingProgramResDTO> UpdateTrainingProgram(int trainingProgramId, TrainingProgramReqDTO updateTrainingProgramReq)
    {
        var existingTrainingProgram = await _trainingProgramRepository.GetProgramById(trainingProgramId);
        if (existingTrainingProgram == null)
        {
            throw new Exception("TrainingProgram id is invalid");
        }

        if (updateTrainingProgramReq.ProgramName != null)
        {
            await ValidateProgramName(updateTrainingProgramReq.ProgramName, trainingProgramId);
        }


        existingTrainingProgram.ProgramName = updateTrainingProgramReq.ProgramName ?? existingTrainingProgram.ProgramName;

        if (updateTrainingProgramReq.TypeId.HasValue)
        {
            existingTrainingProgram.TypeId = updateTrainingProgramReq.TypeId.Value;
        }

        if (updateTrainingProgramReq.Cost.HasValue)
        {
            existingTrainingProgram.Cost = updateTrainingProgramReq.Cost.Value;
        }


        if (updateTrainingProgramReq.ImageFile != null)
        {
            existingTrainingProgram.ImagePath = await SaveImageFileAsync(updateTrainingProgramReq.ImageFile);
        }

        var updatedTrainingProgram = await _trainingProgramRepository.UpdateProgram(existingTrainingProgram);

            var trainingProgramResDTO = new TrainingProgramResDTO
            {
                ProgramId = updatedTrainingProgram.ProgramId,
                TypeId = updatedTrainingProgram.TypeId,
                ProgramName = updatedTrainingProgram.ProgramName,
                Cost = updatedTrainingProgram.Cost,
                Description = updatedTrainingProgram.Description,
                ImagePath = updatedTrainingProgram.ImagePath
            };

            return trainingProgramResDTO;

        }

    public async Task DeleteTrainingProgram(int trainingProgramId)
    {
        await _trainingProgramRepository.DeleteProgram(trainingProgramId);
    }

    private async Task<string> SaveImageFileAsync(IFormFile imageFile)
    {
        var profileImagesPath = Path.Combine(_webHostEnvironment.WebRootPath, "profileimages");

        if (!Directory.Exists(profileImagesPath))
        {
            Directory.CreateDirectory(profileImagesPath);
        }

        var fileName = Guid.NewGuid().ToString() + Path.GetExtension(imageFile.FileName);
        var filePath = Path.Combine(profileImagesPath, fileName);

        using (var stream = new FileStream(filePath, FileMode.Create))
        {
            await imageFile.CopyToAsync(stream);
        }

        return $"/profileimages/{fileName}";
    }

        private async Task ValidateProgramName(string programName, int? programId = null)
        {
            
            var existingTrainingProgram = await _trainingProgramRepository.GetProgramByName(programName);

            if (existingTrainingProgram != null)
            {
                // If the programId is provided and matches the found program's ID, it means no name conflict
                if (programId.HasValue && existingTrainingProgram.ProgramId == programId.Value)
                {
                    return; 
                }

                // Otherwise, the name is already taken by another program
                throw new ArgumentException($"{programName} is already registered.");
            }
        }

    }
}
