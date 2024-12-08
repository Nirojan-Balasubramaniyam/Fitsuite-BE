using GYMFeeManagement_System_BE.DTOs.Request;
using GYMFeeManagement_System_BE.DTOs.Response;
using GYMFeeManagement_System_BE.Entities;

namespace GYMFeeManagement_System_BE.IServices
{
    public interface IReviewService
    {
        Task<List<ReviewResDTO>> GetReviews();
        Task<Review> AddReview(ReviewReqDTO review);
    }
}
