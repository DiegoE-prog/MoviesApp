using Movies.Common.Models.Dtos.Category;

namespace Movies.WEB.Services.IServices
{
    public interface ICategoryService
    {
        Task<T> GetCategoriesAsync<T>(string token);
        Task<T> GetCategoryByIdAsync<T>(int id, string token);
        Task<T> CreateCategoryAsync<T>(CategoryToAddDto categoryDto, string token);
        Task<T> UpdateCategoryAsync<T>(CategoryToUpdateDto categoryDto, string token);
        Task<T> DeleteCategoryAsync<T>(int id, string token);
    }
}
