using System.ComponentModel.DataAnnotations;

namespace GYMFeeManagement_System_BE.Entities
{
    public class TrainingProgram
    {
        [Key]
        public int ProgramId { get; set; }
        public int TypeId { get; set; }
        public ProgramType ProgramType { get; set; }
        public string ProgramName { get; set; }
        public decimal Cost { get; set; }
        public string Description { get; set; }
        public string? ImagePath { get; set; }


        public ICollection<Alert> Alerts { get; set; }
        public ICollection<Request> Requests { get; set; }
        public ICollection<EnrollProgram> EnrollPrograms { get; set; }



    }
}
