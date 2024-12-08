using System.ComponentModel.DataAnnotations;

namespace GYMFeeManagement_System_BE.Entities
{
    public class Member
    {
        [Key]
        public int MemberId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string PasswordHash { get; set; }
        public string NIC { get; set; }
        public string Phone { get; set; }
        public DateTime DoB { get; set; }
        public string Gender { get; set; }
        public string EmergencyContactName { get; set; }
        public string EmergencyContactNumber { get; set; }
        public string? ImagePath { get; set; }
        public int? TrainerId { get; set; }
        public Staff? Staff { get; set; }

        public int BranchId { get; set; }
        public Branch Branch { get; set; }
        public Address? Address { get; set; }
        public bool IsActive { get; set; }


        public ICollection<EnrollProgram> EnrollPrograms { get; set; }
        public ICollection<Payment> Payments { get; set; }
        public ICollection<Alert> Alerts { get; set; }
        public ICollection<Request> Requests { get; set; }
        public ICollection<WorkoutEnrollment> WorkoutEnrollments { get; set; }
        public ICollection<Review> Reviews { get; set; }



    }
}
