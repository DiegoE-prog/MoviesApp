using AutoMapper;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Movies.API.DataAccess;
using Movies.API.Dtos.Movie;
using Movies.API.Entities;
using Movies.API.Repositories.Interfaces;

namespace Movies.API.Repositories
{
    public class MovieRepository : IMovieRepository
    {
        private readonly DataContext _dbContext;
        private readonly IMapper _mapper;

        public MovieRepository(DataContext dbContext, IMapper mapper)
        {
            _dbContext = dbContext;
            _mapper = mapper;
        }

        public async Task<Movie> AddMovieAsync(MovieToAddDto movieToAddDto)
        {
            if(await MovieExist(movieToAddDto.Title))
            {
                throw new Exception("Movie already exist");
            }

            var movie = _mapper.Map<Movie>(movieToAddDto);
            _dbContext.Movies.Add(movie);
            await _dbContext.SaveChangesAsync();
            return movie;
        }

        public async Task<bool> DeleteMovieAsync(int id)
        {
            var movieFromDb = await _dbContext.Movies
                .FirstOrDefaultAsync(c => c.MovieId == id);

            if(movieFromDb is not null)
            {
                movieFromDb.IsActive = false;
                await _dbContext.SaveChangesAsync();
                return true;
            }

            return false;
        }

        public async Task<Movie> GetMovieByIdAsync(int id)
        {
            var movie = await _dbContext.Movies
                .Where(m => m.IsActive == true)
                .Include(c => c.Category)
                .Where(c => c.Category!.IsActive == true)
                .FirstOrDefaultAsync(c => c.MovieId == id);
                
            return movie!;
        }

        public async Task<List<Movie>> GetMoviesAsync()
        {
            var movies = await _dbContext.Movies
                .Where(m => m.IsActive == true)
                .Include(c => c.Category)
                .Where(c => c.Category!.IsActive == true)
                .ToListAsync();
            return movies;
        }

        public async Task<Movie> UpdateMovieAsync(MovieToUpdateDto movieToUpdateDto)
        {
            var movieFromDb = await _dbContext.Movies.FirstOrDefaultAsync(c => c.MovieId == movieToUpdateDto.MovieId);
            if(movieFromDb is not null)
            {
                movieFromDb.Title = movieToUpdateDto.Title;
                movieFromDb.Description = movieToUpdateDto.Description;
                movieFromDb.ReleaseDate= movieToUpdateDto.ReleaseDate;
                movieFromDb.PosterUrl = movieToUpdateDto.PosterUrl;
                movieFromDb.CategoryId = movieToUpdateDto.CategoryId;

                await _dbContext.SaveChangesAsync();
                return movieFromDb;
            }
            return movieFromDb!;
        }

        private async Task<bool> MovieExist(string title) 
        {
            if(await _dbContext.Movies.AnyAsync(c=> c.Title.ToLower() == title.ToLower()))
            {
                return true;
            }
            return false;
        }
    }
}
