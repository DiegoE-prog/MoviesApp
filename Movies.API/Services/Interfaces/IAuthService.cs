using Movies.API.Dtos.User;
using Movies.API.Entities;
using Movies.API.Models;

namespace Movies.API.Services.Interfaces
{
    public interface IAuthService
    {
        Task<ServiceResponse<int>> Register(UserDto userDto);
        Task<ServiceResponse<string>> Login(UserDto userDto);
    }
}
