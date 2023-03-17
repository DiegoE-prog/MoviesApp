using Movies.DataAccess.Entities;

namespace Movies.API.Repositories.Interfaces
{
    public interface IAuthRepository
    {
        Task<int> Register(User user);
        Task<User> Login(string username);
    }
}
