using GYMFeeManagement_System_BE.DTOs.Request;
using GYMFeeManagement_System_BE.IServices;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace GYMFeeManagement_System_BE.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class WorkoutPlanController : ControllerBase
    {
        private readonly IWorkoutPlanService _workoutPlanService;

        public WorkoutPlanController(IWorkoutPlanService workoutPlanService)
        {
            _workoutPlanService = workoutPlanService;
        }


        [HttpPost]
        public async Task<IActionResult> AddWorkoutPlan(WorkoutPlanReqDTO addWorkoutPlanReq)
        {
            try
            {
                var data = await _workoutPlanService.AddWorkoutPlan(addWorkoutPlanReq);

                return Ok(data);

            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }

        }

        [HttpGet("{workoutPlanId}")]
        public async Task<IActionResult> GetWorkoutPlanById(int workoutPlanId)
        {
            try
            {
                var data = await _workoutPlanService.GetWorkoutPlanById(workoutPlanId);

                return Ok(data);

            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }

        }



        [HttpGet]
        public async Task<IActionResult> GetAllWorkoutPlans(int pageNumber, int pageSize)
        {
            try
            {
                var data = await _workoutPlanService.GetAllWorkoutPlans(pageNumber, pageSize);

                return Ok(data);

            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }



        [HttpPut("{workoutPlanId}")]
        public async Task<IActionResult> UpdateWorkoutPlan(int workoutPlanId,  WorkoutPlanReqDTO updateWorkoutPlanReq)
        {
            try
            {
                var data = await _workoutPlanService.UpdateWorkoutPlan(workoutPlanId, updateWorkoutPlanReq);

                return Ok(data);

            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpDelete("{workoutPlanId}")]
        public async Task<IActionResult> DeleteWorkoutPlan(int workoutPlanId)
        {
            try
            {
                await _workoutPlanService.DeleteWorkoutPlan(workoutPlanId);
                return Ok();

            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }

        }
    }
}
