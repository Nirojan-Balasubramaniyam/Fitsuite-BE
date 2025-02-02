using System.ComponentModel.DataAnnotations;

namespace GYMFeeManagement_System_BE.Entities
{
    public class PaymentDiscount
    {
        [Key]
        public int DiscountId { get; set; }
        public string Name { get; set; }
        public float Discount { get; set; }
    }
}
