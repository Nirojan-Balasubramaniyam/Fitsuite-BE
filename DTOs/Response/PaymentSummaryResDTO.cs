namespace GYMFeeManagement_System_BE.DTOs.Response
{
    public class PaymentSummaryResDTO
    {
        public decimal TotalMonthlyPayment { get; set; }
        public decimal TotalPaid { get; set; }
        public decimal OverduePayment { get; set; }
        public decimal RemainAmountToPay { get; set; }
        public decimal RemainAmounToPayPercentage { get; set; }
        public decimal PaidPercentage { get; set; }
        public decimal OverduePercentage { get; set; }
    }
}
