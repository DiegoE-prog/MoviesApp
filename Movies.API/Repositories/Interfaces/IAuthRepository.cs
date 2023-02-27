using Movies.API.Entities;
using Movies.API.Models;

namespace Movies.API.Repositories.Interfaces
{
    public interface IAuthRepository
    {
        Task<int> Register(User user);
        Task<User> Login(string username);
    }
}
