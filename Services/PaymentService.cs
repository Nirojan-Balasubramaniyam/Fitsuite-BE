using GYMFeeManagement_System_BE.DTOs.Request;
using GYMFeeManagement_System_BE.Entities;
using GYMFeeManagement_System_BE.IRepositories;
using GYMFeeManagement_System_BE.IServices;
using GYMFeeManagement_System_BE.DTOs.Response;

namespace GYMFeeManagement_System_BE.Services
{
    public class PaymentService : IPaymentService
    {
        private readonly IPaymentRepository _paymentRepository;

        public PaymentService(IPaymentRepository paymentRepository)
        {
            _paymentRepository = paymentRepository;
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

        public async Task<PaymentResDTO> GetPaymentById(int trainingPaymentId)
        {
            var payment = await _paymentRepository.GetPaymentById(trainingPaymentId);

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


        public async Task<PaymentResDTO> AddPayment(PaymentReqDTO addPaymentReq)
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
