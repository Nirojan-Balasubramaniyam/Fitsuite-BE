﻿using System.ComponentModel.DataAnnotations;

namespace GYMFeeManagement_System_BE.DTOs.Request
{
    public class LoginRequestDTO
    {
        [Required]
        public string Email { get; set; } = string.Empty;
        [Required]
        public string Password { get; set; } = string.Empty;
    }
}
