using AutoMapper;
using Movies.API.Dtos.Review;
using Movies.API.Entities;
using Movies.API.Models;
using Movies.API.Repositories.Interfaces;
using Movies.API.Services.Interfaces;

namespace Movies.API.Services
{
    public class ReviewService : IReviewService
    {
        private readonly IReviewRepository _reviewRepository;
        private readonly IMovieRepository _movieRepository;
        private readonly IMapper _mapper;

        public ReviewService(IReviewRepository reviewRepository, IMapper mapper, IMovieRepository movieRepository)
        {
            _reviewRepository = reviewRepository;
            _mapper = mapper;
            _movieRepository = movieRepository;
        }

        public async Task<ServiceResponse<GetReviewDtoWithoutNavigation>> AddReviewAsync(ReviewToAddDto reviewToAddDto)
        {
            var serviceResponse = new ServiceResponse<GetReviewDtoWithoutNavigation>();

            if (reviewToAddDto.MovieId is 0)
            {
                throw new Exception("MovieId needs to be a valid value");
            }

            var movie = await _movieRepository.GetMovieByIdAsync(reviewToAddDto.MovieId);

            if (movie is null)
            {
                throw new Exception("There´s not a Movie with that Id");
            }

            var review = await _reviewRepository.AddReviewAsync(reviewToAddDto);

            serviceResponse.Data = _mapper.Map<GetReviewDtoWithoutNavigation>(review);
            serviceResponse.Message = "Review added successfully";

            return serviceResponse;
        }

        public async Task<ServiceResponse<object>> DeleteReviewAsync(int movieId)
        {
            var serviceResponse = new ServiceResponse<object>();

            if (movieId == 0)
            {
                throw new Exception("MovieId needs to be a valid value");
            }

            var isSuccessful = await _reviewRepository.DeleteReviewAsync(movieId);

            if (!isSuccessful)
            {
                throw new Exception("There is not a review for that movie");
            }

            serviceResponse.Message = "Review deleted successfully";

            return serviceResponse;
        }

        public async Task<ServiceResponse<List<GetReviewDto>>> GetReviewsAsync()
        {
            var serviceResponse = new ServiceResponse<List<GetReviewDto>>();

            var reviews = await _reviewRepository.GetReviewsAsync();

            if (reviews.Count == 0)
                throw new Exception("There is not reviews register yet");

            serviceResponse.Data = _mapper.Map<List<GetReviewDto>>(reviews);


            return serviceResponse;
        }

        public async Task<ServiceResponse<List<GetReviewDto>>> GetReviewsByMovieAsync(int movieId)
        {
            var serviceResponse = new ServiceResponse<List<GetReviewDto>>();

            var reviews = await _reviewRepository.GetReviewsByMovieAsync(movieId);
            
            if (reviews.Count == 0)
                throw new Exception("There is not reviews register for that movie");

            serviceResponse.Data = _mapper.Map<List<GetReviewDto>>(reviews);


            return serviceResponse;
        }

        public async Task<ServiceResponse<List<GetReviewDto>>> GetReviewsByUserAsync(int userId)
        {
            var serviceResponse = new ServiceResponse<List<GetReviewDto>>();

            var reviews = await _reviewRepository.GetReviewsByUserAsync(userId);
            
            if (reviews.Count == 0)
                throw new Exception("There is not reviews register for that user.");

            serviceResponse.Data = _mapper.Map<List<GetReviewDto>>(reviews);

            return serviceResponse;
        }

        public async Task<ServiceResponse<GetReviewDtoWithoutNavigation>> UpdateReviewAsync(ReviewToUpdateDto reviewToUpdateDto)
        {
            var serviceResponse = new ServiceResponse<GetReviewDtoWithoutNavigation>();

            var review = await _reviewRepository.UpdateReviewAsync(reviewToUpdateDto);

            if (review is null)
            {
                throw new Exception("There is not a review assigned to that movie");
            }

            serviceResponse.Data = _mapper.Map<GetReviewDtoWithoutNavigation>(review);

            return serviceResponse;
        }
    }
}
