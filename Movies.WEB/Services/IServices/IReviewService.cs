using Movies.Common.Models.Dtos.Review;

namespace Movies.WEB.Services.IServices
{
    public interface IReviewService
    {
        Task<T> GetReviewsAsync<T>(string token);
        Task<T> GetReviewsByUserAsync<T>(int userId, string token);
        Task<T> GetReviewsByMovieAsync<T>(int movieId, string token);
        Task<T> GetReviewsByUserLoggedAsync<T>(string token);
        Task<T> AddReviewAsync<T>(ReviewToAddDto reviewToAdd, string token);
        Task<T> UpdateReviewAsync<T>(ReviewToUpdateDto reviewToUpdate, string token);
        Task<T> DeleteReviewAsync<T>(int movieId, string token);
    }
}