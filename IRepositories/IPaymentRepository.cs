﻿using GYMFeeManagement_System_BE.DTOs.Response;
using GYMFeeManagement_System_BE.Entities;

namespace GYMFeeManagement_System_BE.IRepositories
{
    public interface IPaymentRepository
    {
        Task<Payment> AddPayment(Payment payment);
        Task<PaginatedResponse<Payment>> GetAllPayments(string? paymentType, int? pageNumber, int? pageSize);
        Task<Payment> GetPaymentById(int paymentId);
        Task<Payment> UpdatePayment(Payment updatePayment);
        Task DeletePayment(int paymentId);
    }
}