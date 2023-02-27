using Movies.WEB.Models.Dtos;

namespace Movies.WEB.Services.IServices
{
    public interface ICategoryService
    {
        Task<T> GetCategoriesAsync<T>(string token);
        Task<T> GetCategoryByIdAsync<T>(int id, string token);
        Task<T> CreateCategoryAsync<T>(CategoryDto categoryDto, string token);
        Task<T> UpdateCategoryAsync<T>(CategoryDto categoryDto, string token);
        Task<T> DeleteCategoryAsync<T>(int id, string token);
    }
}
