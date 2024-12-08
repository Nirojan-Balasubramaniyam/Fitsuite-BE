using GYMFeeManagement_System_BE.Database;
using GYMFeeManagement_System_BE.Entities;
using GYMFeeManagement_System_BE.IRepositories;
using Microsoft.EntityFrameworkCore;

namespace GYMFeeManagement_System_BE.Repositories
{
    public class ReviewRepository : IReviewRepository
    {
        private readonly GymDbContext _dbContext;

        public ReviewRepository(GymDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<List<Review>> GetAllReviews()
        {
            return await _dbContext.Reviews
                .Include(r => r.Member)
                .ToListAsync();
        }

        public async Task<Review> AddReview(Review review)
        {
            await _dbContext.Reviews.AddAsync(review);
            await _dbContext.SaveChangesAsync();
            return review;
        }

    }
}

