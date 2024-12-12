using GYMFeeManagement_System_BE.Entities;

namespace GYMFeeManagement_System_BE.DTOs.Response
{
    public class TrainingProgramResDTO
    {
        public int ProgramId { get; set; }
        public int TypeId { get; set; }
        public string ProgramName { get; set; }
        public string TypeName { get; set; }

        public decimal Cost { get; set; }
        public string Description { get; set; }
        public string? ImagePath { get; set; }
    }
}
