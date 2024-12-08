using GYMFeeManagement_System_BE.DTOs.Request;
using GYMFeeManagement_System_BE.IServices;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace GYMFeeManagement_System_BE.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class WorkoutEnrollmentController : ControllerBase
    {
        private readonly IWorkoutEnrollService _workoutEnrollService;

        public WorkoutEnrollmentController(IWorkoutEnrollService workoutEnrollService)
        {
            _workoutEnrollService = workoutEnrollService;
        }

        [HttpPost]
        public async Task<IActionResult> AddWorkoutEnrollment(WorkoutEnrollReqDTO workoutEnrollRequest)
        {
            try
            {
                var data = await _workoutEnrollService.AddWorkoutEnrollment(workoutEnrollRequest);
                return Ok(data);

            }
            catch (Exception ex)
            {
                return BadRequest(new
                {
                    Message = ex.Message,
                    InnerException = ex.InnerException?.Message
                });
            }

        }

        [HttpGet("workoutenroll/{workoutEnrollId}")]
        public async Task<IActionResult> GetWorkoutEnrollmentById(int workoutEnrollId)
        {
            try
            {
                var data = await _workoutEnrollService.GetWorkoutEnrollmentById(workoutEnrollId);
                return Ok(data);

            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }

        }

        [HttpGet("/workout-plans{memberId}")]
        public async Task<IActionResult> GetWorkoutplansByMemberId(int memberId)
        {
            try
            {
                var data = await _workoutEnrollService.GetWorkoutplansByMemberId(memberId);

                return Ok(data);

            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }

        }

        [HttpGet("{memberId}")]
        public async Task<IActionResult> GetWorkoutEnrollmentsByMemberId(int memberId)
        {
            try
            {
                var data = await _workoutEnrollService.GetWorkoutEnrollmentsByMemberId(memberId);

                return Ok(data);

            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }

        }

        [HttpGet]
        public async Task<IActionResult> GetAllWorkoutEnrollments(int pageNumber, int pageSize)
        {
            try
            {
                var data = await _workoutEnrollService.GetAllWorkoutEnrollments(pageNumber, pageSize);
                return Ok(data);

            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }

        }


        [HttpPut]
        public async Task<IActionResult> UpdateWorkoutEnrollment(int workoutEnrollId, WorkoutEnrollReqDTO workoutEnrollRequest)
        {
            try
            {
                var data = await _workoutEnrollService.UpdateWorkoutEnrollment(workoutEnrollId, workoutEnrollRequest);
                return Ok(data);

            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }

        }

        [HttpDelete("{workoutEnrollId}")]
        public async Task<IActionResult> DeleteWorkoutEnrollment(int workoutEnrollId)
        {
            try
            {
                await _workoutEnrollService.DeleteWorkoutEnrollment(workoutEnrollId);
                return Ok();

            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }

        }
    }
}
