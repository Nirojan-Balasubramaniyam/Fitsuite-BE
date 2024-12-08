using System.ComponentModel.DataAnnotations;

namespace GYMFeeManagement_System_BE.Entities
{
    public class Branch
    {
        [Key]
        public int BranchId {  get; set; }
        public string BranchName { get; set; }
        public Address? Address { get; set; }
        public bool IsActive { get; set; }
        public ICollection<Member>? Members { get; set; }
        public ICollection<Staff>? Staffs { get; set; }
    }
}
