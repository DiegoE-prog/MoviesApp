using Movies.API.Dtos.Movie;
using Movies.API.Entities;

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
