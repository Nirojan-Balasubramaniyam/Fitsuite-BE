﻿using System.ComponentModel.DataAnnotations;

namespace GYMFeeManagement_System_BE.Entities
{
    public class Request
    {
        [Key]
        public int RequestId { get; set; }
        public string RequestType { get; set; }
        public int? MemberId { get; set; }
        public Member? member { get; set; }
        public string? PaymentType { get; set; }
        public decimal? Amount { get; set; }
        public string? ReceiptNumber { get; set; }
        public DateTime? PaidDate { get; set; }
        public DateTime? DueDate { get; set; }
        public string Status { get; set; }
        public int? ProgramId { get; set; }
        public TrainingProgram? trainingProgram { get; set; }
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        public string? Phone { get; set; }
        public string? NIC { get; set; }
        public string? Email { get; set; }
        public DateTime? DOB { get; set; }
        public string? Gender { get; set; }
        public Address? Address { get; set; }
        public string? EmergencyContactName { get; set; }
        public string? EmergencyContactNumber { get; set; }
        public string? ImagePath { get; set; }
        public string? Password { get; set; }

    }
}