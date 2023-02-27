using Microsoft.AspNetCore.Mvc;
using Movies.WEB.Models.Dtos;
using Movies.WEB.Models.Dtos.Review;
using Movies.WEB.Services.IServices;
using Newtonsoft.Json;
using System.Security.Claims;

namespace Movies.WEB.Controllers
{
    public class ReviewController : Controller
    {
        private readonly IReviewService _reviewService;
        private static string token = "";

        public ReviewController(IReviewService reviewService)
        {
            _reviewService = reviewService;
        }

        [HttpGet]
        public IActionResult ReviewCreate(int movieId)
        {
            return View(new ReviewToCreate()
            {
                MovieId = movieId
            });
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> ReviewCreate(ReviewToCreate reviewToCreate)
        {
            token = User.FindFirstValue("Token");

            if (ModelState.IsValid)
            {
                var response = await _reviewService.AddReviewAsync<ResponseDto>(reviewToCreate, token);

                if (response is not null && response.Success)
                {
                    return RedirectToAction("Details", "Home", new { movieId = reviewToCreate.MovieId });
                }
            }

            return View(reviewToCreate);
        }

        [HttpGet]
        public async Task<IActionResult> ReviewEdit(int movieId)
        {
            token = User.FindFirstValue("Token");

            var response = await _reviewService.GetReviewsByMovieAsync<ResponseDto>(movieId, token);

            if (response is not null && response.Success)
            {
                var reviews = JsonConvert.DeserializeObject<List<ReviewDto>>(Convert.ToString(response.Data)!)!;

                var review = reviews.Find(c => c.User.Username == User.Identity.Name);

                return View(new ReviewToUpdate() { MovieId = movieId, ReviewText = review.ReviewText });
            }

            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> ReviewEdit(ReviewToUpdate reviewToUpdate)
        {
            var response = await _reviewService.UpdateReviewAsync<ResponseDto>(reviewToUpdate, token);

            if (response is not null && response.Success)
            {
                return RedirectToAction("Details", "Home", new { movieId = reviewToUpdate.MovieId });
            }

            return View(reviewToUpdate);
        }

        [HttpGet]
        public async Task<IActionResult> ReviewDelete(int movieId)
        {
            token = User.FindFirstValue("Token");

            var response = await _reviewService.GetReviewsByMovieAsync<ResponseDto>(movieId, token);

            if (response is not null && response.Success)
            {
                var reviews = JsonConvert.DeserializeObject<List<ReviewDto>>(Convert.ToString(response.Data)!)!;

                var review = reviews.Find(c => c.User.Username == User.Identity.Name);

                return View(new ReviewToCreate() { MovieId = movieId, ReviewText = review.ReviewText });
            }

            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> ReviewDelete(ReviewToCreate review)
        {
            var response = await _reviewService.DeleteReviewAsync<ResponseDto>(review.MovieId, token);

            if(response is not null && response.Success)
            {
                return RedirectToAction("Details", "Home", new { movieId = review.MovieId });
            }

            return View();
        }
    }
}
