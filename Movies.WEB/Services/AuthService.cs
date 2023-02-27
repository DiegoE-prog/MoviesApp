using Movies.WEB.Models.Dtos;
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

        public async Task<T> LoginAsync<T>(LoginDto login)
        {
            return await this.SendAsync<T>(new ApiRequest
            {
                ApiType = SD.ApiType.POST,
                Url = $"{SD.APIBase}api/Auth/Login",
                Data = login
            });
        }

        public async Task<T> RegisterAsync<T>(RegisterDto register)
        {
            return await this.SendAsync<T>(new ApiRequest
            {
                ApiType = SD.ApiType.POST,
                Url = $"{SD.APIBase}api/Auth/Register",
                Data = register
            });
        }
    }
}
