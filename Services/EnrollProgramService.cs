using GYMFeeManagement_System_BE.DTOs.Request;
using GYMFeeManagement_System_BE.Entities;
using GYMFeeManagement_System_BE.IRepositories;
using GYMFeeManagement_System_BE.IServices;
using GYMFeeManagement_System_BE.DTOs.Response;
using GYMFeeManagement_System_BE.Repositories;

namespace GYMFeeManagement_System_BE.Services
{
    public class EnrollProgramService : IEnrollProgramService
    {
        private readonly IEnrollProgramRepository _enrollProgramRepository;
        private readonly IProgramTypeRepository _programTypeRepository;


        public EnrollProgramService(IEnrollProgramRepository enrollProgramRepository, IProgramTypeRepository programTypeRepository)
        {
            _enrollProgramRepository = enrollProgramRepository;
            _programTypeRepository = programTypeRepository;
        }

        public async Task<List<EnrollProgramResDTO>> GetAllEnrollPrograms()
        {
            var enrollList = await _enrollProgramRepository.GetAllEnrollPrograms();

            var enrollProgramResDTOList = enrollList.Select(enroll => new EnrollProgramResDTO
            {
                EnrollId = enroll.EnrollId,
                ProgramId = enroll.ProgramId,
                MemberId = enroll.MemberId
            }).ToList();

            return enrollProgramResDTOList;
        }

        public async Task<EnrollProgramResDTO> GetEnrollProgramById(int enrollId)
        {
            var enroll = await _enrollProgramRepository.GetEnrollProgramById(enrollId);

            var enrollProgramResDTO = new EnrollProgramResDTO
            {
                EnrollId = enroll.EnrollId,
                ProgramId = enroll.ProgramId,
                MemberId = enroll.MemberId
            };

            return enrollProgramResDTO;
        }

        public async Task<List<EnrollProgramResDTO>> GetEnrollProgramsByMemberId(int memberId)
        {
            var enrollList = await _enrollProgramRepository.GetEnrollProgramsByMemberId(memberId);

            var enrollProgramResDTOList = enrollList.Select(enroll => new EnrollProgramResDTO
            {
                EnrollId = enroll.EnrollId,
                ProgramId = enroll.ProgramId,
                MemberId = enroll.MemberId
            }).ToList();

            return enrollProgramResDTOList;
        }

        /* public async Task<List<TrainingProgramResDTO>> GetTrainingProgramsByMemberId(int memberId)
         {
             var trainingPrograms = await _enrollProgramRepository.GetTrainingProgramsByMemberId(memberId);

             var trainingProgramResDTOList = trainingPrograms.Select(tp => new TrainingProgramResDTO
             {
                 ProgramId = tp.ProgramId,
                 ProgramName = tp.ProgramName,
                 TypeId = tp.TypeId,
                 Cost = tp.Cost,
                 ImagePath = tp.ImagePath,
                 Description = tp.Description,
                 TypeName = await _programTypeRepository.GetProgramTypeById(tp.TypeId).TypeName

             }).ToList();

             return trainingProgramResDTOList;
         }*/

        public async Task<List<TrainingProgramResDTO>> GetTrainingProgramsByMemberId(int memberId)
        {
            var trainingPrograms = await _enrollProgramRepository.GetTrainingProgramsByMemberId(memberId);

            
            var trainingProgramResDTOList = new List<TrainingProgramResDTO>();
            foreach (var tp in trainingPrograms)
            {
                var programType = await _programTypeRepository.GetProgramTypeById(tp.TypeId);

                trainingProgramResDTOList.Add(new TrainingProgramResDTO
                {
                    ProgramId = tp.ProgramId,
                    ProgramName = tp.ProgramName,
                    TypeId = tp.TypeId,
                    Cost = tp.Cost,
                    ImagePath = tp.ImagePath,
                    Description = tp.Description,
                    TypeName = programType.TypeName
                });
            }

            return trainingProgramResDTOList;
        }


        public async Task<EnrollProgramResDTO> AddEnrollProgram(EnrollProgramReqDTO addEnrollProgramReq)
        {

            var programEnroll = new EnrollProgram
            {
                MemberId = addEnrollProgramReq.MemberId,
                ProgramId = addEnrollProgramReq.ProgramId
            };

            var addedEnrollProgram = await _enrollProgramRepository.AddEnrollProgram(programEnroll);


            var enrollProgramResDTO = new EnrollProgramResDTO
            {
                EnrollId = addedEnrollProgram.EnrollId,
                ProgramId = addedEnrollProgram.ProgramId,
                MemberId = addedEnrollProgram.MemberId
            };

            return enrollProgramResDTO;
        }



        public async Task<EnrollProgramResDTO> UpdateEnrollProgram(int enrollId, EnrollProgramReqDTO updateEnrollProgramReq)
        {
            var existingEnrollProgram = await _enrollProgramRepository.GetEnrollProgramById(enrollId);
            if (existingEnrollProgram == null)
            {
                throw new Exception("EnrollProgram id is invalid");
            }

            existingEnrollProgram.MemberId = updateEnrollProgramReq.MemberId != 0 ? updateEnrollProgramReq.MemberId : existingEnrollProgram.MemberId;
            existingEnrollProgram.ProgramId = updateEnrollProgramReq.ProgramId != 0 ? updateEnrollProgramReq.ProgramId : existingEnrollProgram.ProgramId;


            var updatedEnrollProgram = await _enrollProgramRepository.UpdateEnrollProgram(existingEnrollProgram);

            var enrollProgramResDTO = new EnrollProgramResDTO
            {
                EnrollId = updatedEnrollProgram.EnrollId,
                ProgramId = updatedEnrollProgram.ProgramId,
                MemberId = updatedEnrollProgram.MemberId
            };

            return enrollProgramResDTO;

        }

        public async Task DeleteEnrollProgram(int enrollId)
        {
            await _enrollProgramRepository.DeleteEnrollProgram(enrollId);
        }
    }
}
