using Movies.WEB.Models.Dtos;
using Movies.WEB.Services.IServices;
using static Movies.WEB.SD;
using System;

namespace Movies.WEB.Services
{
    public class CategoryService: BaseService, ICategoryService
    {
        private readonly IHttpClientFactory _clientFactory;

        public CategoryService(IHttpClientFactory clientFactory) : base(clientFactory)
        {
            _clientFactory = clientFactory;
        }

        public async Task<T> CreateCategoryAsync<T>(CategoryDto categoryDto, string token)
        {
            return await this.SendAsync<T>(new ApiRequest()
            {
                ApiType = SD.ApiType.POST,
                Data = categoryDto,
                Url = SD.APIBase + "api/Category",
                AccessToken= token
            });
        }

        public async Task<T> DeleteCategoryAsync<T>(int id, string token)
        {
            return await this.SendAsync<T>(new ApiRequest()
            {
                ApiType = SD.ApiType.DELETE,
                Url = SD.APIBase + $"api/Category/{id}",
                AccessToken = token
            });
        }

        public async Task<T> GetCategoriesAsync<T>(string token)
        {
            return await this.SendAsync<T>(new ApiRequest()
            {
                ApiType = SD.ApiType.GET,
                Url = SD.APIBase + "api/Category/GetAll",
                AccessToken = token
            });
        }

        public async Task<T> GetCategoryByIdAsync<T>(int id, string token)
        {
            return await this.SendAsync<T>(new ApiRequest()
            {
                ApiType = SD.ApiType.GET,
                Url = SD.APIBase + $"api/Category/{id}",
                AccessToken = token
            });
        }

        public async Task<T> UpdateCategoryAsync<T>(CategoryDto categoryDto, string token)
        {
            return await this.SendAsync<T>(new ApiRequest()
            {
                ApiType = SD.ApiType.PUT,
                Data = categoryDto,
                Url = SD.APIBase + "api/Category",
                AccessToken = token
            });
        }
    }
}
