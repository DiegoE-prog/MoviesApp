﻿using AutoMapper;
using Movies.API.Dtos.Category;
using Movies.API.Models;
using Movies.API.Repositories.Interfaces;
using Movies.API.Services.Interfaces;

namespace Movies.API.Services
{
    public class CategoryService : ICategoryService
    {
        private readonly ICategoryRepository _categoryRepository;
        private readonly IMapper _mapper;

        public CategoryService(ICategoryRepository categoryRepository, IMapper mapper)
        {
            _categoryRepository = categoryRepository;
            _mapper = mapper;
        }

        public async Task<ServiceResponse<GetCategoryDto>> AddCategoryAsync(CategoryToAddDto categoryToAddDto)
        {
            var serviceResponse = new ServiceResponse<GetCategoryDto>();
            var category = await _categoryRepository.AddCategoryAsync(categoryToAddDto);

            serviceResponse.Data = _mapper.Map<GetCategoryDto>(category);
            serviceResponse.Message = "Category Added Successfully";

            return serviceResponse;
        }

        public async Task<ServiceResponse<string>> DeleteCategoryAsync(int id)
        {
            var serviceResponse = new ServiceResponse<string>();

            var isSucessfull = await _categoryRepository.DeleteCategoryAsync(id);
            
            if (isSucessfull is false)
            {
                throw new Exception("There is not a category with that ID");
            }
            
            serviceResponse.Data = "Category deleted successfully";
            return serviceResponse;
        }

        public async Task<ServiceResponse<List<GetCategoryDto>>> GetCategoriesAsync()
        {
            var serviceResponse = new ServiceResponse<List<GetCategoryDto>>();

            var categories = await _categoryRepository.GetCategoriesAsync();
          
            if (categories is null || categories.Count is 0)
            {
                throw new Exception("There are not categories available");
            }

            serviceResponse.Data = _mapper.Map<List<GetCategoryDto>>(categories);

            return serviceResponse;
        }

        public async Task<ServiceResponse<GetCategoryDto>> GetCategoryByIdAsync(int id)
        {
            var serviceResponse = new ServiceResponse<GetCategoryDto>();

            var category = await _categoryRepository.GetCategoryByIdAsync(id);
            
            if (category is null)
            {
                throw new Exception("There is not a category with that ID");
            
            }
            serviceResponse.Data = _mapper.Map<GetCategoryDto>(category);

            return serviceResponse;
        }

        public async Task<ServiceResponse<GetCategoryDto>> UpdateCategoryAsync(CategoryToUpdateDto categoryToUpdateDto)
        {
            var serviceResponse = new ServiceResponse<GetCategoryDto>();

            var category = await _categoryRepository.UpdateCategoryAsync(categoryToUpdateDto);
            
            if (category is null)
            {
                throw new Exception("There is not a category with that ID");
            }
            serviceResponse.Data = _mapper.Map<GetCategoryDto>(category);

            return serviceResponse;
        }
    }
}
