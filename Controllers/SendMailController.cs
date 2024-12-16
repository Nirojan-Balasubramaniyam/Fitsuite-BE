using GYMFeeManagement_System_BE.DTOs.Request.RequestEntityDTOs;
using GYMFeeManagement_System_BE.Entities;
using GYMFeeManagement_System_BE.IServices;
using GYMFeeManagement_System_BE.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace GYMFeeManagement_System_BE.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SendMailController : ControllerBase
    {
        private readonly sendmailService _sendmailService;

        public SendMailController(sendmailService sendmailService)
        {
            _sendmailService = sendmailService;
        }

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


        [HttpPost("Send-Response")]
        public async Task<IActionResult> ResponseMail(SendResponseMailRequest sendMailRequest)
        {
            try
            {
                if (sendMailRequest == null)
                {
                    return BadRequest("Request body cannot be null.");
                }

                var res = await _sendmailService.ResponseMail(sendMailRequest).ConfigureAwait(false);
                return Ok(res);
            }
            catch (Exception ex)
            {
               return BadRequest(ex.Message);
            }
        }

    }
}
