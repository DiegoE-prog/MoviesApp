using Movies.WEB.Models.Dtos;
using Movies.WEB.Models.Dtos.Review;
using Movies.WEB.Services.IServices;

namespace Movies.WEB.Services
{
    public class ReviewService : BaseService, IReviewService
    {
        private readonly IHttpClientFactory _httpClientFactory;

        public ReviewService(IHttpClientFactory httpClientFactory) : base(httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }

        public Task<T> AddReviewAsync<T>(ReviewToCreate reviewToAdd, string token)
        {
            return this.SendAsync<T>(new ApiRequest()
            {
                ApiType = SD.ApiType.POST,
                Data = reviewToAdd,
                Url = $"{SD.APIBase}api/Review",
                AccessToken = token
            });
        }

        public Task<T> DeleteReviewAsync<T>(int movieId, string token)
        {
            return this.SendAsync<T>(new ApiRequest()
            {
                ApiType = SD.ApiType.DELETE,
                Url = $"{SD.APIBase}api/Review/{movieId}",
                AccessToken = token
            });
        }

        public Task<T> GetReviewsAsync<T>(string token)
        {
            return this.SendAsync<T>(new ApiRequest()
            {
                ApiType = SD.ApiType.GET,
                Url = $"{SD.APIBase}api/Review/GetAll",
                AccessToken = token
            });
        }

        public Task<T> GetReviewsByMovieAsync<T>(int movieId, string token)
        {
            return this.SendAsync<T>(new ApiRequest()
            {
                ApiType = SD.ApiType.GET,
                Url = $"{SD.APIBase}api/Review/GetReviewsByMovie/{movieId}",
                AccessToken = token
            });
        }

        public Task<T> GetReviewsByUserAsync<T>(int userId, string token)
        {
            return this.SendAsync<T>(new ApiRequest()
            {
                ApiType = SD.ApiType.GET,
                Url = $"{SD.APIBase}api/Review/GetReviewsByUser/{userId}",
                AccessToken = token
            });
        }

        public Task<T> GetReviewsByUserLoggedAsync<T>(string token)
        {
            return this.SendAsync<T>(new ApiRequest()
            {
                ApiType = SD.ApiType.GET,
                Url = $"{SD.APIBase}api/Review/GetMyReviews",
                AccessToken = token
            });
        }

        public Task<T> UpdateReviewAsync<T>(ReviewToUpdate reviewToUpdate, string token)
        {
            return this.SendAsync<T>(new ApiRequest()
            {
                ApiType = SD.ApiType.PUT,
                Data = reviewToUpdate,
                Url = $"{SD.APIBase}api/Review",
                AccessToken = token
            });
        }
    }
}
