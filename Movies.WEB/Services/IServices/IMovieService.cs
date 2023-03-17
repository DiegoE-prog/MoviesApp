using Movies.Common.Models.Dtos.Movie;

namespace Movies.WEB.Services.IServices
{
    public interface IMovieService
    {
        Task<T> GetMoviesAsync<T>(string token);
        Task<T> GetMovieByIdAsync<T>(int id, string token);
        Task<T> CreateMovieAsync<T>(MovieToAddDto movieDto, string token);
        Task<T> UpdateMovieAsync<T>(MovieToUpdateDto movieDto, string token);
        Task<T> DeleteMovieAsync<T>(int id, string token);
    }
}
