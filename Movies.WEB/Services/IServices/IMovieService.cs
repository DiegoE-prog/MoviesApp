using Movies.WEB.Models.Dtos;

namespace Movies.WEB.Services.IServices
{
    public interface IMovieService
    {
        Task<T> GetMoviesAsync<T>(string token);
        Task<T> GetMovieByIdAsync<T>(int id, string token);
        Task<T> CreateMovieAsync<T>(MovieToCreateDto movieDto, string token);
        Task<T> UpdateMovieAsync<T>(MovieToUpdateDto movieDto, string token);
        Task<T> DeleteMovieAsync<T>(int id, string token);
    }
}
