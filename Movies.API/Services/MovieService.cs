using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Movies.API.Dtos.Movie;
using Movies.API.Models;
using Movies.API.Repositories.Interfaces;
using Movies.API.Services.Interfaces;

namespace Movies.API.Services
{
    public class MovieService : IMovieService
    {
        private readonly IMovieRepository _movieRepository;
        private readonly IMapper _mapper;

        public MovieService(IMovieRepository movieRepository, IMapper mapper)
        {
            _movieRepository = movieRepository;
            _mapper = mapper;
        }

        public async Task<ServiceResponse<GetMovieWithoutCategoryDto>> AddMovieAsync(MovieToAddDto movieToAddDto)
        {
            var serviceResponse = new ServiceResponse<GetMovieWithoutCategoryDto>();
            try
            {
                var movie = await _movieRepository.AddMovieAsync(movieToAddDto);
                serviceResponse.Data = _mapper.Map<GetMovieWithoutCategoryDto>(movie);
                serviceResponse.Message = "Movie added successfully";
            }
            catch(Exception ex)
            {
                serviceResponse.Success = false;
                serviceResponse.Message = ex.Message;
            }
            return serviceResponse;
        }

        
        public async Task<ServiceResponse<string>> DeleteMovieAsync(int id)
        {
            var serviceResponse = new ServiceResponse<string>();
            try
            {
                if(id is 0) { throw new Exception("The Id needs to be a valid ID"); }

                var isSuccesfull = await _movieRepository.DeleteMovieAsync(id);
                if (isSuccesfull is false)
                {
                    throw new Exception("There is not a movie with that ID");
                }
                serviceResponse.Data = "Movie deleted succesfully";
            }
            catch(Exception ex)
            {
                serviceResponse.Success = false;
                serviceResponse.Message = ex.Message;
            }

            return serviceResponse;
        }

        public async Task<ServiceResponse<GetMovieDto>> GetMovieByIdAsync(int id)
        {
            var serviceResponse = new ServiceResponse<GetMovieDto>();
            try
            {
                if (id is 0) { throw new Exception("The Id needs to be a valid ID"); }
                var movie = await _movieRepository.GetMovieByIdAsync(id);
                if(movie is null)
                {
                    throw new Exception("There is not a movie with that ID");
                }
                serviceResponse.Data = _mapper.Map<GetMovieDto>(movie);
            }
            catch(Exception ex)
            {
                serviceResponse.Success = false;
                serviceResponse.Message = ex.Message;
            }
            return serviceResponse;
        }

        public async Task<ServiceResponse<List<GetMovieDto>>> GetMoviesAsync()
        {
            var serviceResponse = new ServiceResponse<List<GetMovieDto>>();
            try
            {
                var movies = await _movieRepository.GetMoviesAsync();
                if (movies is null  || movies.Count == 0)
                {
                    throw new Exception("There are not movies");
                }
                serviceResponse.Data = _mapper.Map<List<GetMovieDto>>(movies);
            }
            catch (Exception ex)
            {
                serviceResponse.Success = false;
                serviceResponse.Message = ex.Message;
            }
            return serviceResponse;
        }

        
        public async Task<ServiceResponse<GetMovieWithoutCategoryDto>> UpdateMovieAsync(MovieToUpdateDto movieToUpdateDto)
        {
            var serviceResponse = new ServiceResponse<GetMovieWithoutCategoryDto>();
            try
            {
                if (movieToUpdateDto.CategoryId is 0) { throw new Exception("The Id needs to be a valid ID"); }
                var movie = await _movieRepository.UpdateMovieAsync(movieToUpdateDto);
                if (movie is null)
                {
                    throw new Exception("There is not a movie with that value");
                }
                serviceResponse.Data = _mapper.Map<GetMovieWithoutCategoryDto>(movie);
                serviceResponse.Message = "Movie updated succesfully";
            }
            catch (Exception ex)
            {
                serviceResponse.Success = false;
                serviceResponse.Message = ex.Message;
            }
            return serviceResponse;
        }
    }
}
