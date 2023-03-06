using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Movies.API.DataAccess;
using Movies.API.Dtos.Category;
using Movies.API.Entities;
using Movies.API.Repositories.Interfaces;
using System.Runtime.CompilerServices;

namespace Movies.API.Repositories
{
    public class CategoryRepositoryWithSP : ICategoryRepository
    {
        private readonly DataContext _dbContext;
        public CategoryRepositoryWithSP(DataContext dbContext)
        {
            _dbContext= dbContext;
        }

        public async Task<Category> AddCategoryAsync(CategoryToAddDto categoryToAddDto)
        {
            var result = await _dbContext.Database
                .ExecuteSqlInterpolatedAsync($"sp_CreateCategory {categoryToAddDto.Name}");

            return new Category() { CategoryId = result, Name= categoryToAddDto.Name, IsActive = true };
        }

        public async Task<Category> UpdateCategoryAsync(CategoryToUpdateDto categoryToUpdateDto)
        {
            var result = await _dbContext.Database
                    .ExecuteSqlInterpolatedAsync
                    ($"sp_UpdateCategory {categoryToUpdateDto.CategoryId}, {categoryToUpdateDto.Name}");

            return new Category() { CategoryId = categoryToUpdateDto.CategoryId, IsActive= true, Name = categoryToUpdateDto.Name};
        }

        public async Task<bool> DeleteCategoryAsync(int id)
        {
            var result = await _dbContext.Database
                    .ExecuteSqlInterpolatedAsync($"sp_DeleteCategory {id}");

            return result is 1 ? true   : false;
        }

        public async Task<List<Category>> GetCategoriesAsync()
        {
            var categories = await _dbContext.Categories
                .FromSqlInterpolated($"sp_GetAllCategories").ToListAsync();

            return categories;
        }

        public async Task<Category> GetCategoryByIdAsync(int id)
        {
            var param = new SqlParameter("@id", id);

            var category = await _dbContext.Categories
                    .FromSqlInterpolated($"sp_GetCategoryById {id}").ToListAsync();

            return category.First(c => c.CategoryId == id);
        }

    }
}
