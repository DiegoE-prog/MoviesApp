using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Movies.WEB.Models.Dtos;
using Movies.WEB.Services.IServices;
using Newtonsoft.Json;
using System.Data;
using System.Security.Claims;

namespace Movies.WEB.Controllers
{
    [Authorize(Roles = "Admin")]
    public class MovieController : Controller
    {
        private static string token = "";
        private readonly IMovieService _movieService;
        private readonly ICategoryService _categoryService;
        
        public MovieController(IMovieService movieService, ICategoryService categoryService)
        {
            _movieService= movieService;
            _categoryService = categoryService;
        }

        public async Task<IActionResult> Index()
        {
            List<MovieDto> movies = new();

            //token = HttpContext.Session.GetString("access_token")!;

            token = User.FindFirstValue("Token");

            var response = await _movieService.GetMoviesAsync<ResponseDto>(token);

            if(response is not null && response.Success)
            {
                movies = JsonConvert.DeserializeObject<List<MovieDto>>(Convert.ToString(response.Data)!)!;
            }

            return View(movies);       
        }

        [HttpGet]
        public async Task<IActionResult> MovieCreate()
        {
            ViewBag.Categories = await GetCategories();
            
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> MovieCreate(MovieToCreateDto movie)
        {
            if(ModelState.IsValid)
            {
                var response = await _movieService.CreateMovieAsync<ResponseDto>(movie, token);

                if(response is not null && response.Success)
                {
                    return RedirectToAction("Index");
                }
                else
                {
                    ViewBag.Error = response?.Message;
                }
            }
            ViewBag.Categories = await GetCategories();
            return View();
        }

        [HttpGet]
        public async Task<IActionResult> MovieEdit(int movieId)
        {
            var response = await _movieService.GetMovieByIdAsync<ResponseDto>(movieId, token);

            if(response is not null && response.Success)
            {
                var movie = JsonConvert.DeserializeObject<MovieToUpdateDto>(Convert.ToString(response.Data)!);
                ViewBag.Categories = await GetCategories();
                return View(movie);
            }

            return View();
        }

        [HttpPost]
        public async Task<IActionResult> MovieEdit(MovieToUpdateDto movie)
        {
            if(ModelState.IsValid)
            {
                var response = await _movieService.UpdateMovieAsync<ResponseDto>(movie, token);

                if (response is not null && response.Success)
                {
                    return RedirectToAction("Index");
                }
            }

            ViewBag.Categories = await GetCategories();
            return View();
        }

        [HttpGet]
        public async Task<IActionResult> MovieDelete(int movieId)
        {
            var response = await _movieService.GetMovieByIdAsync<ResponseDto>(movieId, token);

            if (response.Success)
            {
                var movie = JsonConvert.DeserializeObject<MovieDto>(Convert.ToString(response.Data)!);
                ViewBag.Categories = await GetCategories();
                return View(movie);
            }

            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> MovieDelete(MovieDto movie)
        {
            var response = await _movieService.DeleteMovieAsync<ResponseDto>(movie.MovieId, token);

            if(response is not null && response.Success)
            {
                return RedirectToAction("Index");
            }

            return View();
        }

        private async Task<List<SelectListItem>> GetCategories()
        {
            var response = await _categoryService.GetCategoriesAsync<ResponseDto>(token);
            List<SelectListItem> selectList = new();

            if (response is not null && response.Success)
            {
                var categories = JsonConvert.DeserializeObject<List<CategoryDto>>(Convert.ToString(response.Data)!)!;

                foreach (var category in categories)
                {
                    selectList.Add(new SelectListItem()
                    {
                        Text = category.Name,
                        Value = Convert.ToString(category.CategoryId)
                    });
                }
            }

            return selectList;
        }
    }
}