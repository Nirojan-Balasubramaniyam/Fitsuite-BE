﻿using GYMFeeManagement_System_BE.DTOs.Request;
using GYMFeeManagement_System_BE.Entities;
using GYMFeeManagement_System_BE.IServices;
using GYMFeeManagement_System_BE.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace GYMFeeManagement_System_BE.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MemberController : ControllerBase
    {
        private readonly IMemberService _memberService;

        public MemberController(IMemberService memberService)
        {
            _memberService = memberService;
        }


        [HttpPost]
        public async Task<IActionResult> AddMember([FromForm] MemberReqDTO addMemberReq)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(addMemberReq.StaffId?.ToString()))
                {
                    addMemberReq.StaffId = null;
                }

                var data = await _memberService.AddMember(addMemberReq);

                return Ok(data);

            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }

        }


        [HttpGet("{memberId}")]
        public async Task<IActionResult> GetMemberById(int memberId)
        {
            try
            {
                var data = await _memberService.GetMemberById(memberId);

                return Ok(data);

            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }

        }

        [HttpGet("login")]
        public async Task<IActionResult> Login(string email, string password)
        {
            try
            {
                var result = await _memberService.Login(email, password);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet]
        public async Task<IActionResult> GetAllMembers(int pageNumber, int pageSize)
        {
            try
            {
                var data = await _memberService.GetAllMembers(pageNumber, pageSize);

                return Ok(data);

            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }



        [HttpPut("{memberId}")]
        public async Task<IActionResult> UpdateMember(int memberId, [FromForm] MemberReqDTO updateMemberReq)
        {
            try
            {
                var data = await _memberService.UpdateMember(memberId, updateMemberReq);

                return Ok(data);

            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpDelete("{memberId}")]
        public async Task<IActionResult> DeleteMember(int memberId)
        {
            try
            {
                await _memberService.DeleteMember(memberId);
                return Ok();

            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }

        }
    }
}