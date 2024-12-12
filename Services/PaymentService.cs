using GYMFeeManagement_System_BE.DTOs.Request;
using GYMFeeManagement_System_BE.Entities;
using GYMFeeManagement_System_BE.IRepositories;
using GYMFeeManagement_System_BE.IServices;
using GYMFeeManagement_System_BE.DTOs.Response;
using GYMFeeManagement_System_BE.Repositories;
using Microsoft.EntityFrameworkCore;

namespace GYMFeeManagement_System_BE.Services
{
    public class PaymentService : IPaymentService
    {
        private readonly IPaymentRepository _paymentRepository;
        private readonly IAlertRepository _alertRepository;


        public PaymentService(IPaymentRepository paymentRepository, IAlertRepository alertRepository)
        {
            _paymentRepository = paymentRepository;
            _alertRepository = alertRepository;

        }

        public async Task<List<PaymentResDTO>> GetAllPayments()
        {
            var payments = await _paymentRepository.GetAllPayments(); // Fetch all payments from the repository

            var paymentResDTOList = payments.Select(payment => new PaymentResDTO
            {
                PaymentId = payment.PaymentId,
                MemberId = payment.MemberId,
                PaymentType = payment.PaymentType,
                Amount = payment.Amount,
                PaymentMethod = payment.PaymentMethod,
                PaidDate = payment.PaidDate,
                DueDate = payment.DueDate
            }).ToList();

            return paymentResDTOList;
        }

        public async Task<PaymentResDTO> GetLastRenewalPaymentForMember(int memberId)
        {
            var payments = await _paymentRepository.GetPaymentsByMemberId(memberId); 

            var renewalPayments = payments.Where(payment =>
                payment.PaymentType.ToLower() != "initial" && payment.PaymentType.ToLower() != "programaddon").ToList();

            var lastRenewalPayment = renewalPayments.OrderByDescending(payment => payment.PaidDate).FirstOrDefault();

            if (lastRenewalPayment == null)
            {
                return null; // No renewal payments found
            }

            return new PaymentResDTO
            {
                PaymentId = lastRenewalPayment.PaymentId,
                MemberId = lastRenewalPayment.MemberId,
                PaymentType = lastRenewalPayment.PaymentType,
                Amount = lastRenewalPayment.Amount,
                PaymentMethod = lastRenewalPayment.PaymentMethod,
                PaidDate = lastRenewalPayment.PaidDate,
                DueDate = lastRenewalPayment.DueDate
            };
        }

        public async Task<PaginatedResponse<PaymentResDTO>> GetAllPayments(string? paymentType, int? pageNumber, int? pageSize)
        {
            var paymentList = await _paymentRepository.GetAllPayments(paymentType, pageNumber, pageSize);

            var paymentResDTOList = paymentList.Data.Select(payment => new PaymentResDTO
            {
                PaymentId = payment.PaymentId,
                MemberId = payment.MemberId,
                PaymentType = payment.PaymentType,
                Amount = payment.Amount,
                PaymentMethod = payment.PaymentMethod,
                PaidDate = payment.PaidDate,
                DueDate = payment.DueDate
            }).ToList();

            // Return the paginated response with DTOs
            return new PaginatedResponse<PaymentResDTO>
            {
                TotalRecords = paymentList.TotalRecords,
                PageNumber = paymentList.PageNumber,
                PageSize = paymentList.PageSize,
                Data = paymentResDTOList
            };
        }

        public async Task<PaymentResDTO> GetPaymentById(int paymentId)
        {
            var payment = await _paymentRepository.GetPaymentById(paymentId);

            var paymentResDTO = new PaymentResDTO
            {
                PaymentId = payment.PaymentId,
                MemberId = payment.MemberId,
                PaymentType = payment.PaymentType,
                Amount = payment.Amount,
                PaymentMethod = payment.PaymentMethod,
                PaidDate = payment.PaidDate,
                DueDate = payment.DueDate
            };

            // Return the DTO
            return paymentResDTO;
        }


        /* public async Task<PaymentResDTO> AddPayment(PaymentReqDTO addPaymentReq)
         {

             var payment = new Payment
             {
                 MemberId = addPaymentReq.MemberId,
                 PaymentType = addPaymentReq.PaymentType,
                 PaymentMethod = addPaymentReq.PaymentMethod,
                 Amount = addPaymentReq.Amount,
                 PaidDate = addPaymentReq.PaidDate,
                 DueDate = addPaymentReq.PaidDate.AddDays(30)
             };

             var addedPayment = await _paymentRepository.AddPayment(payment);

             var paymentResDTO = new PaymentResDTO
             {
                 PaymentId = addedPayment.PaymentId,
                 MemberId = addedPayment.MemberId,
                 PaymentType = addedPayment.PaymentType,
                 Amount = addedPayment.Amount,
                 PaymentMethod = addedPayment.PaymentMethod,
                 PaidDate = addedPayment.PaidDate,
                 DueDate = addedPayment.DueDate
             };


             return paymentResDTO;
         }
 */

        public async Task<PaymentResDTO> AddPayment(PaymentReqDTO addPaymentReq)
        {
            // Determine due date based on payment type
            DateTime? dueDate = null;
            string paymentType = addPaymentReq.PaymentType?.Replace(" ", "").Trim().ToLower(); // Normalize the payment type

            if (paymentType == "programaddon")
            {
                dueDate = null; // No due date for program addon
            }
            else if (paymentType == "monthly")
            {
                dueDate = addPaymentReq.PaidDate.AddDays(30);
            }
            else if (paymentType == "quarterly")
            {
                dueDate = addPaymentReq.PaidDate.AddDays(90);
            }
            else if (paymentType == "semi-annual")
            {
                dueDate = addPaymentReq.PaidDate.AddDays(180);
            }
            else if (paymentType == "annual")
            {
                dueDate = addPaymentReq.PaidDate.AddDays(360);
            }
            else
            {
                dueDate= null;
            }

            // Create a new payment record
            var payment = new Payment
            {
                MemberId = addPaymentReq.MemberId,
                PaymentType = addPaymentReq.PaymentType,
                PaymentMethod = addPaymentReq.PaymentMethod,
                Amount = addPaymentReq.Amount,
                PaidDate = addPaymentReq.PaidDate,
                DueDate = dueDate
            };

            var addedPayment = await _paymentRepository.AddPayment(payment);

            // Handle overdue and renewal alerts for the member
            var overdueAlerts = await _alertRepository.GetAlertsByAlertType("overdue");
            var renewalAlerts = await _alertRepository.GetAlertsByAlertType("renewal");

            // Filter and get the last alert of each type for the selected member using AlertId
            var memberOverdueAlert = overdueAlerts
                .Where(a => a.MemberId == addPaymentReq.MemberId)
                .OrderByDescending(a => a.AlertId) // Use AlertId for ordering
                .FirstOrDefault();
            var memberRenewalAlert = renewalAlerts
                .Where(a => a.MemberId == addPaymentReq.MemberId)
                .OrderByDescending(a => a.AlertId) // Use AlertId for ordering
                .FirstOrDefault();

            // Update alert statuses to false
            if (memberOverdueAlert != null)
            {
                memberOverdueAlert.Status = false;
                await _alertRepository.UpdateAlert(memberOverdueAlert);
            }

            if (memberRenewalAlert != null)
            {
                memberRenewalAlert.Status = false;
                await _alertRepository.UpdateAlert(memberRenewalAlert);
            }

            // Prepare response DTO
            var paymentResDTO = new PaymentResDTO
            {
                PaymentId = addedPayment.PaymentId,
                MemberId = addedPayment.MemberId,
                PaymentType = addedPayment.PaymentType,
                Amount = addedPayment.Amount,
                PaymentMethod = addedPayment.PaymentMethod,
                PaidDate = addedPayment.PaidDate,
                DueDate = addedPayment.DueDate
            };

            return paymentResDTO;
        }

        public async Task<ICollection<PaymentResDTO>> GetAllPaymentsByBranchId(int? branchId)
        {
            var payments = await _paymentRepository.GetAllPaymentsByBranchId(branchId);

            var paymentList = payments.Select(p => new PaymentResDTO
            {
                PaymentId = p.PaymentId,
                PaymentType = p.PaymentType,
                PaidDate = p.PaidDate,
                DueDate = p.DueDate,
                MemberId = p.MemberId,
                Amount = p.Amount,
                PaymentMethod = p.PaymentMethod
            }).ToList();

            return paymentList;
        }



        public async Task<PaymentResDTO> UpdatePayment(int trainingPaymentId, PaymentReqDTO updatePaymentReq)
        {
            var existingPayment = await _paymentRepository.GetPaymentById(trainingPaymentId);
            if (existingPayment == null)
            {
                throw new Exception("Payment id is invalid");
            }

            existingPayment.PaymentType = updatePaymentReq.PaymentType ?? existingPayment.PaymentType;
            existingPayment.PaymentMethod = updatePaymentReq.PaymentMethod ?? existingPayment.PaymentMethod;
            existingPayment.MemberId = updatePaymentReq.MemberId != 0 ? updatePaymentReq.MemberId : existingPayment.MemberId;
            existingPayment.Amount = updatePaymentReq.Amount != 0 ? updatePaymentReq.Amount : existingPayment.Amount;
            existingPayment.PaidDate = updatePaymentReq.PaidDate != default ? updatePaymentReq.PaidDate : existingPayment.PaidDate;
            existingPayment.DueDate = updatePaymentReq.DueDate != default ? updatePaymentReq.DueDate : existingPayment.DueDate;


            var updatedPayment = await _paymentRepository.UpdatePayment(existingPayment);

            var paymentResDTO = new PaymentResDTO
            {
                PaymentId = updatedPayment.PaymentId,
                MemberId = updatedPayment.MemberId,
                PaymentType = updatedPayment.PaymentType,
                Amount = updatedPayment.Amount,
                PaymentMethod = updatedPayment.PaymentMethod,
                PaidDate = updatedPayment.PaidDate,
                DueDate = updatedPayment.DueDate
            };


            return paymentResDTO;

        }



        public async Task DeletePayment(int trainingPaymentId)
        {
            await _paymentRepository.DeletePayment(trainingPaymentId);
        }
    }
}
