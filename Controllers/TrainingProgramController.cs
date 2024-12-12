using GYMFeeManagement_System_BE.DTOs.Request;
using GYMFeeManagement_System_BE.IServices;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace GYMFeeManagement_System_BE.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TrainingProgramController : ControllerBase
    {
        private readonly ITrainingProgramService _trainingProgramService;

        public TrainingProgramController(ITrainingProgramService trainingProgram)
        {
            _trainingProgramService = trainingProgram;
        }


        [HttpPost]
        public async Task<IActionResult> AddTrainingProgram([FromForm] TrainingProgramReqDTO addTrainingProgramReq)
        {
            try
            {
                var data = await _trainingProgramService.AddTrainingProgram(addTrainingProgramReq);

                return Ok(data);

            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }

        }


        [HttpGet("{trainingProgramId}")]
        public async Task<IActionResult> GetTrainingProgramById(int trainingProgramId)
        {
            try
            {
                var data = await _trainingProgramService.GetTrainingProgramById(trainingProgramId);

                return Ok(data);

            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }

        }

/*        [HttpGet("{memberId}")]
        public async Task<IActionResult> GetProgramsByMemberId(int memberId)
        {
            try
            {

                var data = await _trainingProgramService.GetProgramsByMemberId(memberId);

                return Ok(data);

            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }

        }*/

        [HttpGet("pagination")]
        public async Task<IActionResult> GetAllTrainingPrograms(int pageNumber, int pageSize)
        {
            try
            {
                var data = await _trainingProgramService.GetAllTrainingPrograms(pageNumber, pageSize);

                return Ok(data);

            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet]
        public async Task<IActionResult> GetAllTrainingPrograms()
        {
            try
            {
                var data = await _trainingProgramService.GetAllTrainingPrograms();

                return Ok(data);

            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }



        [HttpPut("{trainingProgramId}")]
        public async Task<IActionResult> UpdateTrainingProgram(int trainingProgramId, [FromForm] TrainingProgramReqDTO updateTrainingProgramReq)
        {
            try
            {
                var data = await _trainingProgramService.UpdateTrainingProgram(trainingProgramId, updateTrainingProgramReq);

                return Ok(data);

            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpDelete("{trainingProgramId}")]
        public async Task<IActionResult> DeleteTrainingProgram(int trainingProgramId)
        {
            try
            {
                await _trainingProgramService.DeleteTrainingProgram(trainingProgramId);
                return Ok();

            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }

        }
    }
}
