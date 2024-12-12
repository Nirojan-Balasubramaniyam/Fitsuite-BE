using GYMFeeManagement_System_BE.DTOs.Request;
using GYMFeeManagement_System_BE.IServices;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace GYMFeeManagement_System_BE.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EnrollProgramController : ControllerBase
    {
        private readonly IEnrollProgramService _enrollProgramService;

        public EnrollProgramController(IEnrollProgramService enrollProgramService)
        {
            _enrollProgramService = enrollProgramService;
        }

        [HttpPost]
        public async Task<IActionResult> AddEnrollProgram(EnrollProgramReqDTO enrollProgramRequest)
        {
            try
            {
                var data = await _enrollProgramService.AddEnrollProgram(enrollProgramRequest);
                return Ok(data);

            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }

        }

        [HttpGet("enrollprogram-id/{enrollProgramId}")]
        public async Task<IActionResult> GetEnrollProgramById(int enrollProgramId)
        {
            try
            {
                var data = await _enrollProgramService.GetEnrollProgramById(enrollProgramId);
                return Ok(data);

            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }

        }

        [HttpGet("/training-programs/{memberId}")]
        public async Task<IActionResult> GetTrainingProgramsByMemberId(int memberId)
        {
            try
            {
                var data = await _enrollProgramService.GetTrainingProgramsByMemberId(memberId);

                return Ok(data);

            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }

        }

        [HttpGet("{memberId}")]
        public async Task<IActionResult> GetEnrollProgramsByMemberId(int memberId)
        {
            try
            {
                var data = await _enrollProgramService.GetEnrollProgramsByMemberId(memberId);

                return Ok(data);

            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }

        }

        [HttpGet]
        public async Task<IActionResult> GetAllEnrollPrograms()
        {
            try
            {
                var data = await _enrollProgramService.GetAllEnrollPrograms();
                return Ok(data);

            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }

        }


        [HttpPut]
        public async Task<IActionResult> UpdateEnrollProgram(int enrollProgramId, EnrollProgramReqDTO enrollProgramRequest)
        {
            try
            {
                var data = await _enrollProgramService.UpdateEnrollProgram(enrollProgramId, enrollProgramRequest);
                return Ok(data);

            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }

        }

        [HttpDelete("{enrollProgramId}")]
        public async Task<IActionResult> DeleteEnrollProgramment(int enrollProgramId)
        {
            try
            {
                await _enrollProgramService.DeleteEnrollProgram(enrollProgramId);
                return Ok();

            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }

        }
    }
}
