using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Movies.API.DataAccess;
using Movies.Common.Models.Dtos.Category;
using Movies.API.Entities;
using Movies.API.Repositories.Interfaces;
using System.Runtime.CompilerServices;

namespace Movies.API.Repositories
{
    [Obsolete("Only for reference, not used any more",true)]
    public class CategoryRepositoryWithSP : ICategoryRepository
    {
        private readonly DataContext _dbContext;
        public CategoryRepositoryWithSP(DataContext dbContext)
        {
            _dbContext= dbContext;
        }

        public async Task<bool> AddCategoryAsync(CategoryToAddDto categoryToAddDto)
        {
            var result = await _dbContext.Database
                .ExecuteSqlInterpolatedAsync($"usp_CreateCategory {categoryToAddDto.Name}");

            return result > 0;
        }

        public async Task<bool> UpdateCategoryAsync(CategoryToUpdateDto categoryToUpdateDto)
        {
            var result = await _dbContext.Database
                    .ExecuteSqlInterpolatedAsync
                    ($"usp_UpdateCategory {categoryToUpdateDto.CategoryId}, {categoryToUpdateDto.Name}");

            return result > 0;
        }

        public async Task<bool> DeleteCategoryAsync(int id)
        {
            var result = await _dbContext.Database
                    .ExecuteSqlInterpolatedAsync($"usp_DeleteCategory {id}");

            return result is 1 ? true   : false;
        }

        public async Task<List<Category>> GetCategoriesAsync()
        {
            var categories = await _dbContext.Categories
                .FromSqlInterpolated($"usp_GetAllCategories").ToListAsync();

            return categories;
        }

        public async Task<Category> GetCategoryByIdAsync(int id)
        {
            var param = new SqlParameter("@id", id);

            var category = await _dbContext.Categories
                    .FromSqlInterpolated($"usp_GetCategoryById {id}").ToListAsync();

            return category.First(c => c.CategoryId == id);
        }

    }
}
