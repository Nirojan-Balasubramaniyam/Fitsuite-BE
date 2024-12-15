using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;

namespace GYMFeeManagement_System_BE.Entities
{
    public class Staff
    {
        [Key]
        public int StaffId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string PasswordHash { get; set; }
        public string NIC { get; set; }
        public string Phone { get; set; }
        public DateTime DoB { get; set; }
        public string Gender { get; set; }
        public string? ImagePath { get; set; }
        public UserRole UserRole { get; set; }
        public int? BranchId { get; set; }
        public Branch? Branch { get; set; }
        public Address? Address { get; set; }
        public bool IsActive { get; set; }


        public ICollection<WorkoutPlan> WorkoutPlans { get; set; } 
        public ICollection<WorkoutEnrollment> WorkoutEnrollments { get; set; }
        public ICollection<Member>? Members { get; set; }


    }
}
