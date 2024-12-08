using GYMFeeManagement_System_BE.DTOs.Request;
using GYMFeeManagement_System_BE.Entities;
using GYMFeeManagement_System_BE.IServices;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace GYMFeeManagement_System_BE.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ContactUsMessageController : ControllerBase
    {
        private readonly IContactUsMessageService _messageService;

        public ContactUsMessageController(IContactUsMessageService messageService)
        {
            _messageService = messageService;
        }

        // POST: api/contactus
        [HttpPost]
        public async Task<IActionResult> AddMessage(ContactUsMessageReqDTO message)
        {
            try
            {
                if (message == null)
                    return BadRequest("Invalid message data");

                var data = await _messageService.AddMessage(message);
                return Ok(data);

            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }           
        }

     
        [HttpGet]
        public async Task<IActionResult> GetMessages(int pageNumber, int pageSize)
        {
            try
            {
                var data = await _messageService.GetMessages(pageNumber, pageSize);
                return Ok(data);

            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
 
        }

        [HttpPut]
        public async Task<IActionResult> SoftDeleteMessage(int messageId)
        {
            try
            {
                var data = await _messageService.SoftDeleteMessage(messageId);
                return Ok(data);

            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }

        }
    }
}
