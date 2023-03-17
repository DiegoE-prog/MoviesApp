using Movies.Common.Models.Dtos.Review;
using Movies.API.Entities;

namespace Movies.API.Repositories.Interfaces
{
    public interface IReviewRepository
    {
        Task<List<Review>> GetReviewsAsync();
        Task<List<Review>> GetReviewsByUserAsync(int userId);
        Task<List<Review>> GetReviewsByMovieAsync(int movieId);
        Task<Review> AddReviewAsync(ReviewToAddDto reviewToAddDto);
        Task<Review> UpdateReviewAsync(ReviewToUpdateDto reviewToUpdateDto);
        Task<bool> DeleteReviewAsync(int movieId);
    }
}
