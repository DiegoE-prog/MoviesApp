using Movies.Common.Models.Dtos.Category;
using Movies.API.Models;

namespace Movies.API.Services.Interfaces
{
    public interface ICategoryService
    {
        Task<ServiceResponse<List<GetCategoryDto>>> GetCategoriesAsync();
        Task<ServiceResponse<GetCategoryDto>> GetCategoryByIdAsync(int id);
        Task<ServiceResponse<GetCategoryDto>> AddCategoryAsync(CategoryToAddDto categoryToAddDto);
        Task<ServiceResponse<GetCategoryDto>> UpdateCategoryAsync(CategoryToUpdateDto categoryToUpdateDto);
        Task<ServiceResponse<string>> DeleteCategoryAsync(int id);
    }
}
