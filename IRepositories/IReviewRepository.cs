using GYMFeeManagement_System_BE.Entities;

namespace GYMFeeManagement_System_BE.IRepositories
{
    public interface IReviewRepository
    {
        Task<List<Review>> GetAllReviews(); 
        Task<Review> AddReview(Review review);
    }
}
