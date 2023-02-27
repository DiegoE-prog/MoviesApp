using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Movies.WEB.Models;
using Movies.WEB.Models.Dtos;
using Movies.WEB.Models.Dtos.Review;
using Movies.WEB.Services.IServices;
using Newtonsoft.Json;
using System.Diagnostics;
using System.Net.Http.Headers;
using System.Security.Claims;

namespace Movies.WEB.Controllers
{
    public class HomeController : Controller
    {
        private string token = "";
        
        private readonly IMovieService _movieService;
        private readonly IReviewService _reviewService;

        public HomeController(IMovieService movieService, IReviewService reviewService)
        {
            _movieService = movieService;
            _reviewService = reviewService;
        }

        public async Task<IActionResult> Index()
        {
            List<MovieDto> movies = new();

            var response = await _movieService.GetMoviesAsync<ResponseDto>(token);

            if(response is not null && response.Success)
            {
                movies = JsonConvert.DeserializeObject<List<MovieDto>>(Convert.ToString(response.Data)!)!;
            }

            return View(movies);
        }

        [Authorize]
        [HttpGet]
        public async Task<IActionResult> Details(int movieId)
        {
            token = User.FindFirstValue("Token");

            var responseForMovie = await _movieService.GetMovieByIdAsync<ResponseDto>(movieId, token);
            
            var responseForReview = await _reviewService.GetReviewsByMovieAsync<ResponseDto>(movieId, token);

            if(responseForMovie is not null && responseForMovie.Success)
            {
                var movie = JsonConvert.DeserializeObject<MovieDto>(Convert.ToString(responseForMovie.Data)!);

                var viewModel = new DetailsViewModel()
                {
                    Movie = movie!
                };

                if(responseForReview is not null && responseForReview.Success)
                {
                    var reviews = JsonConvert.DeserializeObject<List<ReviewDto>>(Convert.ToString(responseForReview.Data)!);

                    if (reviews!.Any(c => c.User!.Username == User.Identity!.Name))
                    {
                        viewModel.IsAlreadyReviewed = true;
                    }

                    viewModel.Reviews = reviews!;
                }



                return View(viewModel);
            }

            return View();
        }
    }
}