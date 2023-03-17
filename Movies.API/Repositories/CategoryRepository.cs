using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Movies.DataAccess.Context;
using Movies.Common.Models.Dtos.Category;
using Movies.DataAccess.Entities;
using Movies.API.Repositories.Interfaces;

namespace Movies.API.Repositories
{
    public class CategoryRepository : ICategoryRepository
    {
        private readonly DataContext _dbContext;
        private readonly IMapper _mapper;

        public CategoryRepository(DataContext dbContext, IMapper mapper)
        {
            _dbContext = dbContext;
            _mapper = mapper;
        }

        public async Task<bool> AddCategoryAsync(CategoryToAddDto categoryToAddDto)
        {
            if(await CategoryExist(categoryToAddDto.Name))
            {
                throw new Exception("Category already exist");
            }

            var category = _mapper.Map<Category>(categoryToAddDto);
            _dbContext.Categories.Add(category);
            var result = await _dbContext.SaveChangesAsync();

            return result > 0;
        }

        public async Task<bool> DeleteCategoryAsync(int id)
        {
            var category = await _dbContext.Categories.FirstOrDefaultAsync(c => c.CategoryId == id);

            if (category is not null) 
            { 
                category.IsActive = false;
                await _dbContext.SaveChangesAsync();
                return true;
            }
            return false;
        }

        public async Task<List<Category>> GetCategoriesAsync()
        {
            var categories = await _dbContext.Categories
                .Where(c => c.IsActive == true)
                .ToListAsync();
            return categories;
        }

        public async Task<Category> GetCategoryByIdAsync(int id)
        {
            var category = await _dbContext.Categories
                .Where(c => c.IsActive == true)
                .FirstOrDefaultAsync(c => c.CategoryId == id);
            return category!;
        }

        public async Task<bool> UpdateCategoryAsync(CategoryToUpdateDto categoryToUpdateDto)
        {
            var isSuccessfull = 0;
            var categoryFromDb = await _dbContext.Categories.FirstOrDefaultAsync(c => c.CategoryId == categoryToUpdateDto.CategoryId);
            
            if (categoryFromDb is not null)
            {
                categoryFromDb.Name = categoryToUpdateDto.Name!;

                isSuccessfull = await _dbContext.SaveChangesAsync();
            }

            return isSuccessfull > 0;
        }

        private async Task<bool> CategoryExist(string name)
        {
            if(await _dbContext.Categories.AnyAsync(c => c.Name.ToLower() == name.ToLower()))
            {
                return true;
            }
            return false;
        }
    }
}
