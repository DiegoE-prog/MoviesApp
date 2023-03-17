using Microsoft.AspNetCore.Mvc;
using Movies.API.Dtos.Category;
using Movies.API.Entities;

namespace Movies.API.Repositories.Interfaces
{
    public interface ICategoryRepository
    {
        Task<List<Category>> GetCategoriesAsync();
        Task<Category> GetCategoryByIdAsync(int id);
        Task<bool> AddCategoryAsync(CategoryToAddDto categoryToAddDto);
        Task<bool> UpdateCategoryAsync(CategoryToUpdateDto categoryToUpdateDto);
        Task<bool> DeleteCategoryAsync(int id);
    }
}
