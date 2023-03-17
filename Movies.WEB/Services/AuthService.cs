using Movies.Common.Models.Dtos.User;
using Movies.WEB.Models.Http;
using Movies.WEB.Services.IServices;

namespace Movies.WEB.Services
{
    public class AuthService : BaseService, IAuthService
    {
        private readonly IHttpClientFactory _httppClientFactory;

        public AuthService(IHttpClientFactory httppClientFactory) : base(httppClientFactory)
        {
            _httppClientFactory = httppClientFactory;
        }

        public async Task<T> LoginAsync<T>(UserDto user)
        {
            return await this.SendAsync<T>(new ApiRequest
            {
                ApiType = SD.ApiType.POST,
                Url = $"{SD.APIBase}api/Auth/Login",
                Data = user
            });
        }

        public async Task<T> RegisterAsync<T>(UserDto user)
        {
            return await this.SendAsync<T>(new ApiRequest
            {
                ApiType = SD.ApiType.POST,
                Url = $"{SD.APIBase}api/Auth/Register",
                Data = user
            });
        }
    }
}
