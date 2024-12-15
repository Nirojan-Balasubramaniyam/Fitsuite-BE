using GYMFeeManagement_System_BE.Entities;
using GYMFeeManagement_System_BE.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace GYMFeeManagement_System_BE.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SendMailController(sendmailService _sendmailService) : ControllerBase
    {

        [HttpPost("Send-Mail")]
        public async Task<IActionResult> Sendmail(SendMailRequest sendMailRequest)
        {
            if (sendMailRequest == null)
            {
                return BadRequest("Request body cannot be null.");
            }

            
            var res = await _sendmailService.Sendmail(sendMailRequest).ConfigureAwait(false);
            return Ok(res);
        }

    }
}
