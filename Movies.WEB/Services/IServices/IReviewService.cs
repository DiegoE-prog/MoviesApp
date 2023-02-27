using Movies.WEB.Models.Dtos.Review;
using Newtonsoft.Json.Linq;

namespace Movies.WEB.Services.IServices
{
    public interface IReviewService
    {
        Task<T> GetReviewsAsync<T>(string token);
        Task<T> GetReviewsByUserAsync<T>(int userId, string token);
        Task<T> GetReviewsByMovieAsync<T>(int movieId, string token);
        Task<T> GetReviewsByUserLoggedAsync<T>(string token);
        Task<T> AddReviewAsync<T>(ReviewToCreate reviewToAdd, string token);
        Task<T> UpdateReviewAsync<T>(ReviewToUpdate reviewToUpdate, string token);
        Task<T> DeleteReviewAsync<T>(int movieId, string token);
    }
}