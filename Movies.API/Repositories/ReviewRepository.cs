using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Movies.DataAccess.Context;
using Movies.Common.Models.Dtos.Review;
using Movies.DataAccess.Entities;
using Movies.API.Repositories.Interfaces;
using System.Security.Claims;

namespace Movies.API.Repositories
{
    public class ReviewRepository : IReviewRepository
    {
        private readonly DataContext _dbContext;
        private readonly IMapper _mapper;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public ReviewRepository(DataContext dbContext, IMapper mapper, IHttpContextAccessor httpContextAccessor)
        {
            _dbContext = dbContext;
            _mapper = mapper;
            _httpContextAccessor = httpContextAccessor;
        }

        private int GetUserId() => int.Parse(_httpContextAccessor.HttpContext!.User
                .FindFirstValue(ClaimTypes.NameIdentifier));

        public async Task<Review> AddReviewAsync(ReviewToAddDto reviewToAddDto)
        {
            if(await IsAlreadyReviewed(reviewToAddDto.MovieId))
            {
                throw new Exception("You already have a review register for that movie");
            }

            var reviewToAdd = _mapper.Map<Review>(reviewToAddDto);

            reviewToAdd.IsActive = true;
            reviewToAdd.ReviewDate = DateTime.Now;
            reviewToAdd.UserId = GetUserId();
            _dbContext.Add(reviewToAdd);
            await _dbContext.SaveChangesAsync();

            return reviewToAdd;
        }

        public async Task<List<Review>> GetReviewsAsync()
        {
            var reviews = new List<Review>();

            reviews = await _dbContext.Reviews
                .Include(u => u.User)
                .Include(m => m.Movie)
                .Where(r => r.IsActive)
                .Where(r => r.Movie!.IsActive)
                .ToListAsync();

            return reviews;
        }

        public async Task<List<Review>> GetReviewsByMovieAsync(int movieId)
        {
            var reviews = await _dbContext.Reviews
                .Include(u => u.User)
                .Include(m => m.Movie)
                .Where(r => r.IsActive && r.MovieId == movieId)
                .Where(r => r.Movie!.IsActive)
                .ToListAsync();

            return reviews;
        }

        public async Task<List<Review>> GetReviewsByUserAsync(int userId)
        {
            var reviews = await _dbContext.Reviews
                .Include(u => u.User)
                .Include(m => m.Movie)
                .Where(r => r.IsActive && r.UserId == userId)
                .Where(r => r.Movie!.IsActive)
                .ToListAsync();

            return reviews;
        }

        public async Task<Review> UpdateReviewAsync(ReviewToUpdateDto reviewToUpdateDto)
        {
            var reviewFromDb = await _dbContext.Reviews.FirstOrDefaultAsync
                (c => c.UserId == GetUserId() && c.MovieId == reviewToUpdateDto.MovieId && c.IsActive == true);

            if(reviewFromDb is not null)
            {
                reviewFromDb.ReviewText = reviewToUpdateDto.ReviewText!;
                await _dbContext.SaveChangesAsync();
            }
            return reviewFromDb!;
        }

        public async Task<bool> DeleteReviewAsync(int movieId)
        {
            var reviewFromDb = await _dbContext.Reviews.FirstOrDefaultAsync
                (c => c.UserId == GetUserId() && c.MovieId == movieId && c.IsActive == true);

            if(reviewFromDb is not null)
            {
                reviewFromDb.IsActive = false;
                await _dbContext.SaveChangesAsync();
                return true;
            }

            return false;
        }

        private async Task<bool> IsAlreadyReviewed(int movieId)
        {
            if (await _dbContext.Reviews.AnyAsync
                (c => c.MovieId == movieId && c.UserId == GetUserId() && c.IsActive == true))
            {
                return true;
            }
            return false;
        }
    }
}
