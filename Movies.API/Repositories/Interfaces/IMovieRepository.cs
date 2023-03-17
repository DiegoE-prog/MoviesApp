using Movies.Common.Models.Dtos.Movie;
using Movies.DataAccess.Entities;

namespace Movies.API.Repositories.Interfaces
{
    public interface IMovieRepository
    {
        Task<List<Movie>> GetMoviesAsync();
        Task<Movie> GetMovieByIdAsync(int id);
        Task<Movie> AddMovieAsync(MovieToAddDto movieToAddDto);
        Task<Movie> UpdateMovieAsync(MovieToUpdateDto movieToUpdateDto);
        Task<bool> DeleteMovieAsync(int id);
    }
}
