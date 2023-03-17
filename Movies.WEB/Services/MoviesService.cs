using Movies.WEB.Models.Dtos;
using Movies.Common.Models.Dtos.Movie;
using Movies.WEB.Services.IServices;

namespace Movies.WEB.Services
{
    public class MoviesService : BaseService, IMovieService
    {
        private readonly IHttpClientFactory _httpClientFactory;

        public MoviesService(IHttpClientFactory httpClientFactory) : base(httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }

        public async Task<T> CreateMovieAsync<T>(MovieToAddDto movieDto, string token)
        {
            return await this.SendAsync<T>(new ApiRequest()
            {
                ApiType = SD.ApiType.POST,
                Url = SD.APIBase + "api/Movie",
                Data = movieDto,
                AccessToken = token
            });
        }
        public async Task<T> UpdateMovieAsync<T>(MovieToUpdateDto movieDto, string token)
        {
            return await this.SendAsync<T>(new ApiRequest()
            {
                ApiType = SD.ApiType.PUT,
                Url = SD.APIBase + "api/Movie",
                Data = movieDto,
                AccessToken = token
            });
        }

        public async Task<T> DeleteMovieAsync<T>(int id, string token)
        {
            return await this.SendAsync<T>(new ApiRequest()
            {
                ApiType = SD.ApiType.DELETE,
                Url = SD.APIBase + $"api/Movie/{id}",
                AccessToken = token
            });
        }

        public async Task<T> GetMoviesAsync<T>(string token)
        {
            return await this.SendAsync<T>(new ApiRequest()
            {
                ApiType = SD.ApiType.GET,
                Url = SD.APIBase + "api/Movie/GetAll",
                AccessToken = token
            }); 
        }

        public async Task<T> GetMovieByIdAsync<T>(int id, string token)
        {
            return await this.SendAsync<T>(new ApiRequest()
            {
                ApiType = SD.ApiType.GET,
                Url = SD.APIBase + $"api/Movie/{id}",
                AccessToken = token
            });
        }
    }
}
