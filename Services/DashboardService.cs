using GYMFeeManagement_System_BE.DTOs.Request;
using GYMFeeManagement_System_BE.DTOs.Response;
using GYMFeeManagement_System_BE.IServices;

namespace GYMFeeManagement_System_BE.Services
{
    public class DashboardService : IDashboardService
    {
        private readonly IMemberService _memberService;
        private readonly IPaymentService _paymentService;
        private readonly IAlertService _alertService;

        public DashboardService(IMemberService memberService, IPaymentService paymentService, IAlertService alertService)
        {
            _memberService = memberService;
            _paymentService = paymentService;
            _alertService = alertService;
        }

        public async Task<PaymentSummaryResDTO> GetPaymentSummary(int branchId)
        {
            // Get all active members for the current month
            var members = await _memberService.GetAllMembers(0, 0, true, branchId);
            var totalMonthlyPayment = members.Data.Sum(m => m.MonthlyPayment);

            // Initialize variables to track total paid and overdue payments
            decimal totalPaid = 0;
            decimal overduePayment = 0;

            // Process each member
            foreach (var member in members.Data)
            {
                // Get the last renewal payment for the member
                var lastPayment = await _paymentService.GetLastRenewalPaymentForMember(member.MemberId);

                if (lastPayment != null)
                {
                    // Check if the current date is between the PaidDate and DueDate
                    DateTime paidDate = lastPayment.PaidDate;
                    DateTime? dueDate = lastPayment.DueDate;

                    bool isPaidThisMonth = DateTime.Now >= paidDate && (dueDate == null || DateTime.Now <= dueDate);

                    if (isPaidThisMonth)
                    {
                        // If the payment is valid for this month, add the member's monthly payment to totalPaid
                        totalPaid += member.MonthlyPayment;
                    }
                    else
                    {
                        // If the payment is overdue, add the monthly payment to overduePayment and create an alert
                        overduePayment += member.MonthlyPayment;

                        // Create an alert for overdue payment
                        var alertReq = new AlertReqDTO
                        {
                            AlertType = "Overdue",
                            Amount = member.MonthlyPayment,
                            MemberId = member.MemberId,
                            DueDate = DateTime.Now, // Current date as the due date
                            Status = true,
                            Action = true,
                            AccessedDate = DateTime.Now
                        };

                        await _alertService.AddAlert(alertReq);
                    }
                }
                else
                {
                    // If no payment exists for the member, treat it as overdue
                    overduePayment += member.MonthlyPayment;

                    // Create an alert for overdue payment if no payment is found
                    var alertReq = new AlertReqDTO
                    {
                        AlertType = "Overdue",
                        Amount = member.MonthlyPayment,
                        MemberId = member.MemberId,
                        DueDate = DateTime.Now, // Current date as the due date
                        Status = true,
                        Action = true,
                        AccessedDate = DateTime.Now
                    };

                    await _alertService.AddAlert(alertReq);

                }
            }

            // Calculate the sum and percentages
            var totalAmountToPay = totalMonthlyPayment;
            var paidPercentage = totalAmountToPay > 0 ? (totalPaid / totalAmountToPay) * 100 : 0;
            var overduePercentage = totalAmountToPay > 0 ? (overduePayment / totalAmountToPay) * 100 : 0;

            // Return the payment summary response
            return new PaymentSummaryResDTO
            {
                TotalMonthlyPayment = totalAmountToPay,
                TotalPaid = totalPaid,
                OverduePayment = overduePayment,
                PaidPercentage = paidPercentage,
                OverduePercentage = overduePercentage
            };
        }

    }
}
