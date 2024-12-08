using GYMFeeManagement_System_BE.DTOs.Request;
using GYMFeeManagement_System_BE.DTOs.Response;
using GYMFeeManagement_System_BE.Entities;
using GYMFeeManagement_System_BE.IRepositories;
using GYMFeeManagement_System_BE.IServices;

namespace GYMFeeManagement_System_BE.Services
{
    public class ReviewService : IReviewService
    {
        private readonly IReviewRepository _reviewRepository;

        public ReviewService(IReviewRepository reviewRepository)
        {
            _reviewRepository = reviewRepository;
        }

        public async Task<Review> AddReview(ReviewReqDTO reviewRequest)
        {
            var review = new Review
            {
                MemberId = reviewRequest.MemberId,
                ReviewMessage = reviewRequest.ReviewMessage,
                Rating = reviewRequest.Rating,
                CreatedAt = DateTime.Now
            };

            var addedreview =  await _reviewRepository.AddReview(review);
            return addedreview;
        }
        public async Task<List<ReviewResDTO>> GetReviews()
        {
            var reviews = await _reviewRepository.GetAllReviews();

            var reviewDtos = reviews.Select(r => new ReviewResDTO
            {
                FullName = $"{r.Member.FirstName} {r.Member.LastName}",
                ImagePath = r.Member.ImagePath, 
                ReviewMessage = r.ReviewMessage,
                Rating = r.Rating,
                CreatedAt = r.CreatedAt
            }).ToList();

            return reviewDtos;
        }
    }
}
