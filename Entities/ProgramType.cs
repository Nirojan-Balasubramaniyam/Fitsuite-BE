using System.ComponentModel.DataAnnotations;

namespace GYMFeeManagement_System_BE.Entities
{
    public class ProgramType
    {
        [Key]
        public int TypeId { get; set; }
        public string TypeName { get; set; }
        public ICollection<TrainingProgram>? TrainingPrograms { get; set; }
    }
}
