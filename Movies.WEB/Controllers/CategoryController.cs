using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Movies.WEB.Models;
using Movies.WEB.Models.Dtos;
using Movies.Common.Models.Dtos.Movie;
using Movies.WEB.Services.IServices;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Net.Http.Headers;
using System.Security.Claims;
using System.Text;
using Movies.Common.Models.Dtos.Category;

namespace Movies.WEB.Controllers
{
    [Authorize(Roles = "Admin")]
    public class CategoryController : Controller
    {
        private static string token = "";
        private readonly ICategoryService _categoryService;

        public CategoryController(ICategoryService categoryService)
        {
            _categoryService= categoryService;
            
        }

        public async Task<IActionResult> Index()
        {
            List<GetCategoryDto> categories = new();

            //token = HttpContext.Session.GetString("access_token")!;

            token = User.FindFirstValue("Token");

            var response = await _categoryService.GetCategoriesAsync<ResponseDto>(token);
            
            if(response is not null && response.Success)
            {
                categories = JsonConvert.DeserializeObject<List<GetCategoryDto>>(Convert.ToString(response.Data)!)!;   
            }

            return View(categories);
        }

        [HttpGet]
        public IActionResult CategoryCreate()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CategoryCreate(CategoryToAddDto categoryDto)
        {
            if (ModelState.IsValid)
            {
                var response = await _categoryService.CreateCategoryAsync<ResponseDto>(categoryDto, token);
                if (response is not null && response.Success)
                {
                    return RedirectToAction("Index");
                }
                else
                {
                    ViewBag.Error = response?.Message;
                }
                
            }
            return View();
        }

        [HttpGet]
        public async Task<IActionResult> CategoryEdit(int categoryId)
        {
            CategoryToUpdateDto category = new();

            var response = await _categoryService.GetCategoryByIdAsync<ResponseDto>(categoryId, token);

            if (response is not null && response.Success)
            {
                category = JsonConvert.DeserializeObject<CategoryToUpdateDto>(Convert.ToString(response.Data)!)!;
            }

            return View(category);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CategoryEdit(CategoryToUpdateDto categoryDto)
        {
            if (ModelState.IsValid)
            {
                var response = await _categoryService.UpdateCategoryAsync<ResponseDto>(categoryDto, token);

                if (response is not null && response.Success)
                {
                    return RedirectToAction("Index");
                }
            }
            return View();
        }

        [HttpGet]
        public async Task<IActionResult> CategoryDelete(int categoryId)
        {
            GetCategoryDto category = new();

            var response = await _categoryService.GetCategoryByIdAsync<ResponseDto>(categoryId, token);

            if (response is not null && response.Success)
            {
                category = JsonConvert.DeserializeObject<GetCategoryDto>(Convert.ToString(response.Data)!)!;
            }

            return View(category);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CategoryDelete(GetCategoryDto categoryDto)
        {
            var response = await _categoryService.DeleteCategoryAsync<ResponseDto>(categoryDto.CategoryId, token);

            if (response is not null && response.Success)
            {
                return RedirectToAction("Index");
            }

            return View();
        }
    }
}
