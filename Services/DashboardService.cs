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

        public async Task<PaymentSummaryResDTO> GetPaymentSummary(int? branchId)
        {
            // Get all active members for the current month
            var members = await _memberService.GetAllMembers(0, 0, true, branchId??0);
            var totalMonthlyPayment = members.Data.Sum(m => m.MonthlyPayment);

            // Initialize variables to track total paid and overdue payments
            decimal totalPaid = 0;
            decimal overduePayment = 0;
            decimal remainAmountToPay = 0;

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

                    bool isOverdue = DateTime.Now > dueDate;

                    if (isPaidThisMonth)
                    {
                        // If the payment is valid for this month, add the member's monthly payment to totalPaid
                        totalPaid += member.MonthlyPayment;
                    }
                    else if(isOverdue)
                    {
                        overduePayment += member.MonthlyPayment;
                        bool hasOverdueAlert = false;
                        // Check if an overdue alert already exists for this member
                        var existingAlerts = await _alertService.GetAlertsByAlertType("Overdue");
                        if(existingAlerts != null)
                        {
                            hasOverdueAlert = existingAlerts.Any(alert => alert.MemberId == member.MemberId);

                        }

                        if (!hasOverdueAlert)
                        {
                            // If no overdue alert exists, add the monthly payment to overduePayment and create an alert
                            

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
                        remainAmountToPay += member.MonthlyPayment;
                    }
                   
                }
               
            }

            // Calculate the sum and percentages
            var totalAmountToPay = totalMonthlyPayment;
            remainAmountToPay = remainAmountToPay==0 ? overduePayment : remainAmountToPay;
            var paidPercentage = totalAmountToPay > 0 ? (totalPaid / totalAmountToPay) * 100 : 0;
            var overduePercentage = totalAmountToPay > 0 ? (overduePayment / totalAmountToPay) * 100 : 0;
            var remainAmountToPayPercentage = totalAmountToPay > 0 ? (remainAmountToPay / totalAmountToPay) * 100 : 0;

            // Return the payment summary response
            return new PaymentSummaryResDTO
            {
                TotalMonthlyPayment = totalAmountToPay,
                TotalPaid = totalPaid,
                RemainAmountToPay = remainAmountToPay,
                RemainAmounToPayPercentage = remainAmountToPayPercentage,
                OverduePayment = overduePayment,
                PaidPercentage = paidPercentage,
                OverduePercentage = overduePercentage
            };
        }

    }
}
