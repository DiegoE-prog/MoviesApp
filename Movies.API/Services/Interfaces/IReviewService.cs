using Movies.Common.Models.Dtos.Review;
using Movies.Common.Models.Http;

namespace Movies.API.Services.Interfaces
{
    public interface IReviewService
    {
        Task<ServiceResponse<List<GetReviewDto>>> GetReviewsAsync();
        Task<ServiceResponse<List<GetReviewDto>>> GetReviewsByUserAsync(int userId);
        Task<ServiceResponse<List<GetReviewDto>>> GetReviewsByMovieAsync(int movieId);
        Task<ServiceResponse<GetReviewDtoWithoutNavigation>> AddReviewAsync(ReviewToAddDto reviewToAddDto);
        Task<ServiceResponse<GetReviewDtoWithoutNavigation>> UpdateReviewAsync(ReviewToUpdateDto reviewToUpdateDto);
        Task<ServiceResponse<object>> DeleteReviewAsync(int movieId);
    }
}
