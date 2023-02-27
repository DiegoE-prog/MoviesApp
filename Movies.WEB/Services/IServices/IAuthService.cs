using Movies.WEB.Models.Dtos;

namespace Movies.WEB.Services.IServices
{
    public interface IAuthService
    {
        Task<T> LoginAsync<T>(LoginDto login);
        Task<T> RegisterAsync<T>(RegisterDto register);
    }
}
