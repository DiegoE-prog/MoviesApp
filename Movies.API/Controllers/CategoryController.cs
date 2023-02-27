using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Movies.API.Dtos.Category;
using Movies.API.Entities;
using Movies.API.Models;
using Movies.API.Repositories.Interfaces;
using Movies.API.Services.Interfaces;

namespace Movies.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(Roles ="Admin")]
    public class CategoryController : ControllerBase
    {
        private readonly ICategoryService _categoryService;

        public CategoryController(ICategoryService categoryService)
        {
            _categoryService = categoryService;
        }

        [HttpGet("GetAll")]
        public async Task<ActionResult<ServiceResponse<List<GetCategoryDto>>>> GetCategories()
        {
            var response = await _categoryService.GetCategoriesAsync();

            if (response.Success is false)
            {
                return BadRequest(response);
            }
            return Ok(response);

        }

        [HttpGet("{id}")]
        public async Task<ActionResult<ServiceResponse<GetCategoryDto>>> GetCategory(int id)
        {
            var response = await _categoryService.GetCategoryByIdAsync(id);
            if (response.Success is false)
            {
                return NotFound(response);
            }
            return Ok(response);
        }

        [HttpPost]
        public async Task<ActionResult<ServiceResponse<GetCategoryDto>>> AddCategory(CategoryToAddDto categoryToAddDto)
        {
            var response = await _categoryService.AddCategoryAsync(categoryToAddDto);
            if (response.Success is false)
            {
                return BadRequest(response);
            }
            return Ok(response);
        }

        [HttpPut]
        public async Task<ActionResult<ServiceResponse<GetCategoryDto>>> UpdateCategory(CategoryToUpdateDto categoryToUpdateDto)
        {
            var response = await _categoryService.UpdateCategoryAsync(categoryToUpdateDto);
            if (response.Success is false)
            {
                return BadRequest(response);
            }
            return Ok(response);
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<ServiceResponse<GetCategoryDto>>> DeleteCategory(int id)
        {
            var response = await _categoryService.DeleteCategoryAsync(id);
            if(response.Success is false)
            {
                return BadRequest(response);
            }
            return Ok(response);
        }
    }
}
