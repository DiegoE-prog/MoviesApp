using Movies.Common.Models.Dtos.User;

namespace Movies.WEB.Services.IServices
{
    public interface IAuthService
    {
        Task<T> LoginAsync<T>(UserDto user);
        Task<T> RegisterAsync<T>(UserDto user);
    }
}
