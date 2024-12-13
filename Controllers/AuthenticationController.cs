using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using GYMFeeManagement_System_BE.IServices;
using GYMFeeManagement_System_BE.DTOs.Request;

namespace GYMFeeManagement_System_BE.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthenticationController : ControllerBase
    {
        private readonly IAuthenticationService _AuthenticationService;

        public AuthenticationController(IAuthenticationService userService)
        {
            _AuthenticationService = userService;
        }

        [HttpPost("login")]

        public async Task<IActionResult> Login(LoginRequestDTO request)
        {
            try
            {
                var userDetails = await _AuthenticationService.Login(request);
                return Ok(userDetails);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
