using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Movies.WEB.Models.Dtos;
using Movies.WEB.Models.Dtos.Review;
using Movies.WEB.Services.IServices;
using Newtonsoft.Json;
using System.Security.Claims;

namespace Movies.WEB.Controllers
{
    [Authorize]
    public class UserController : Controller
    {
        private readonly IReviewService _reviewService;

        public UserController(IReviewService reviewService)
        {
            _reviewService = reviewService;
        }

        public async Task<IActionResult> Index()
        {
            var token = User.FindFirstValue("Token");

            var response = await _reviewService.GetReviewsByUserLoggedAsync<ResponseDto>(token);

            if(response is not null && response.Success)
            {
                var reviews = JsonConvert.DeserializeObject<List<ReviewDto>>(Convert.ToString(response.Data)!);

                return View(reviews);
            }

            return View();
        }
    }
}
