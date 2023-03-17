using Movies.Common.Models.Dtos.Movie;
using Movies.API.Models;

namespace Movies.API.Services.Interfaces
{
    public interface IMovieService
    {
        Task<ServiceResponse<List<GetMovieDto>>> GetMoviesAsync();
        Task<ServiceResponse<GetMovieDto>> GetMovieByIdAsync(int id);
        Task<ServiceResponse<GetMovieWithoutCategoryDto>> AddMovieAsync(MovieToAddDto movieToAddDto);
        Task<ServiceResponse<GetMovieWithoutCategoryDto>> UpdateMovieAsync(MovieToUpdateDto movieToUpdateDto);
        Task<ServiceResponse<string>> DeleteMovieAsync(int id);
    }
}
