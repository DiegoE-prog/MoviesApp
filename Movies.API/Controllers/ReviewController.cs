using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Movies.API.Dtos.Review;
using Movies.API.Models;
using Movies.API.Services.Interfaces;
using System.Security.Claims;

namespace Movies.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ReviewController : ControllerBase
    {
        private readonly IReviewService _reviewService;

        public ReviewController(IReviewService reviewService)
        {
            _reviewService = reviewService;
        }
        
        [HttpGet("GetAll")]
        public async Task<ActionResult<ServiceResponse<List<GetReviewDto>>>> GetReviews()
        {
            var response = await _reviewService.GetReviewsAsync();
 
            return Ok(response);
        }

        [HttpGet("GetReviewsByUser/{userId}")]
        public async Task<ActionResult<ServiceResponse<List<GetReviewDto>>>> GetReviewsByUser(int userId)
        {
            var response = await _reviewService.GetReviewsByUserAsync(userId);

            return Ok(response);
        }

        [HttpGet("GetReviewsByMovie/{movieId}")]
        public async Task<ActionResult<ServiceResponse<List<GetReviewDto>>>> GetReviewsByMovie(int movieId)
        {
            var response = await _reviewService.GetReviewsByMovieAsync(movieId);

            return Ok(response);
        }

        [Authorize]
        [HttpGet("GetMyReviews")]
        public async Task<ActionResult<ServiceResponse<List<GetReviewDto>>>> GetMyReviews()
        {
            var userId = int.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier));
            var response = await _reviewService.GetReviewsByUserAsync(userId);

            return Ok(response);
        }

        [Authorize]
        [HttpPost]      
        public async Task<ActionResult<ServiceResponse<GetReviewDto>>> AddReview(ReviewToAddDto reviewToAddDto)
        {
            var response = await _reviewService.AddReviewAsync(reviewToAddDto);

            return Ok(response);
        }

        [Authorize]
        [HttpPut]
        public async Task<ActionResult<ServiceResponse<GetReviewDto>>> UpdateReview(ReviewToUpdateDto reviewToUpdateDto)
        {
            var response = await _reviewService.UpdateReviewAsync(reviewToUpdateDto);

            return Ok(response);
        }

        [Authorize]
        [HttpDelete("{movieId}")]
        public async Task<ActionResult<ServiceResponse<object>>> DeleteReview(int movieId)
        { 
            var response = await _reviewService.DeleteReviewAsync(movieId);

            return Ok(response);
        }
    }
}
