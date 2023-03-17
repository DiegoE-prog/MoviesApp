using Movies.Common.Models.Dtos.User;
using Movies.Common.Models.Http;

namespace Movies.API.Services.Interfaces
{
    public interface IAuthService
    {
        Task<ServiceResponse<int>> Register(UserDto userDto);
        Task<ServiceResponse<string>> Login(UserDto userDto);
    }
}
