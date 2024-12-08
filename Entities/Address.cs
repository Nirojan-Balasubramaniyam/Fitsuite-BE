using System.ComponentModel.DataAnnotations;

namespace GYMFeeManagement_System_BE.Entities
{
    public class Address
    {
        [Key]
        public int AddressId { get; set; }
        public string Street { get; set; }
        public string City { get; set; }
        public string? District { get; set; }
        public string? Province { get; set; }
        public string Country { get; set; }

        public int? BranchId { get; set; }
        public int? MemberId { get; set; }
        public int? StaffId { get; set; }
        public int? RequestId { get; set; }

        // Navigation properties for each related entity
        public Branch? Branch { get; set; }
        public Member? Member { get; set; }
        public Staff? Staff { get; set; }
        public Request? Request { get; set; }

    }
}
