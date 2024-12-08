using GYMFeeManagement_System_BE.DTOs.Request;
using GYMFeeManagement_System_BE.Entities;
using GYMFeeManagement_System_BE.IServices;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace GYMFeeManagement_System_BE.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ReviewController : ControllerBase
    {
        private readonly IReviewService _reviewService;

        public ReviewController(IReviewService reviewService)
        {
            _reviewService = reviewService;
        }

      
        [HttpPost]
        public async Task<IActionResult> AddReview( ReviewReqDTO review)
        {
            try
            {
                var data = await _reviewService.AddReview(review);
                return Ok(data);

            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);  
            }          
           
        }

      
        [HttpGet]
        public async Task<IActionResult> GetReviews()
        {
            try
            {
                var data = await _reviewService.GetReviews();
                return Ok(data);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }

        }
    }
}
