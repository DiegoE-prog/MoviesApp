using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Movies.API.DataAccess;
using Movies.API.Dtos.Category;
using Movies.API.Entities;
using Movies.API.Repositories.Interfaces;
using System.ComponentModel;
using System.Diagnostics;

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

        public async Task<Category> AddCategoryAsync(CategoryToAddDto categoryToAddDto)
        {
            if(await CategoryExist(categoryToAddDto.Name))
            {
                throw new Exception("Category already exist");
            }
            var category = _mapper.Map<Category>(categoryToAddDto);
            _dbContext.Categories.Add(category);
            await _dbContext.SaveChangesAsync();
            return category;
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

        public async Task<Category> UpdateCategoryAsync(CategoryToUpdateDto categoryToUpdateDto)
        {
            var categoryFromDb = await _dbContext.Categories.FirstOrDefaultAsync(c => c.CategoryId == categoryToUpdateDto.CategoryId);
            if (categoryFromDb is not null)
            {
                categoryFromDb.Name= categoryToUpdateDto.Name;
                await _dbContext.SaveChangesAsync();
                return categoryFromDb;
            }
            return categoryFromDb!;
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
