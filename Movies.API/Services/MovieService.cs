using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Movies.Common.Models.Dtos.Movie;
using Movies.API.Exceptions;
using Movies.Common.Models.Http;
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

            var movie = await _movieRepository.AddMovieAsync(movieToAddDto);
            
            serviceResponse.Data = _mapper.Map<GetMovieWithoutCategoryDto>(movie);
            serviceResponse.Message = "Movie added successfully";

            return serviceResponse;
        }


        public async Task<ServiceResponse<string>> DeleteMovieAsync(int id)
        {
            var serviceResponse = new ServiceResponse<string>();

            if (id is 0) 
            { 
                throw new Exception("The Id needs to be a valid ID");
            }

            var isSuccesfull = await _movieRepository.DeleteMovieAsync(id);
            
            if (isSuccesfull is false)
            {
                throw new Exception("There is not a movie with that ID");
            }

            serviceResponse.Data = "Movie deleted succesfully";

            return serviceResponse;
        }

        public async Task<ServiceResponse<GetMovieDto>> GetMovieByIdAsync(int id)
        {
            var serviceResponse = new ServiceResponse<GetMovieDto>();

            if (id is 0) { throw new Exception("The Id needs to be a valid ID"); }
            
            var movie = await _movieRepository.GetMovieByIdAsync(id);
            
            if (movie is null)
            {
                throw new NotFoundException("There is not a movie with that ID");
            }
            
            serviceResponse.Data = _mapper.Map<GetMovieDto>(movie);

            return serviceResponse;
        }

        public async Task<ServiceResponse<List<GetMovieDto>>> GetMoviesAsync()
        {
            var serviceResponse = new ServiceResponse<List<GetMovieDto>>();

            var movies = await _movieRepository.GetMoviesAsync();
            
            if (movies is null || movies.Count == 0)
            {
                throw new NotFoundException("There are not movies");
            }
            
            serviceResponse.Data = _mapper.Map<List<GetMovieDto>>(movies);

            return serviceResponse;
        }


        public async Task<ServiceResponse<GetMovieWithoutCategoryDto>> UpdateMovieAsync(MovieToUpdateDto movieToUpdateDto)
        {
            var serviceResponse = new ServiceResponse<GetMovieWithoutCategoryDto>();

            if (movieToUpdateDto.CategoryId is 0) { throw new Exception("The Id needs to be a valid ID"); }
            
            var movie = await _movieRepository.UpdateMovieAsync(movieToUpdateDto);
            
            if (movie is null)
            {
                throw new NotFoundException("There is not a movie with that value");
            }
            
            serviceResponse.Data = _mapper.Map<GetMovieWithoutCategoryDto>(movie);
            serviceResponse.Message = "Movie updated succesfully";


            return serviceResponse;
        }
    }
}
