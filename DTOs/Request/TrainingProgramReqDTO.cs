using GYMFeeManagement_System_BE.Entities;

namespace GYMFeeManagement_System_BE.DTOs.Request
{
    public class TrainingProgramReqDTO
    {
        public int? TypeId { get; set; }
        public string ProgramName { get; set; }
        public decimal? Cost { get; set; }
        public IFormFile? ImageFile { get; set; }
    }
}
