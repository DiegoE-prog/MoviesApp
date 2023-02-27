using Microsoft.EntityFrameworkCore;
using Movies.API.DataAccess;
using Movies.API.Entities;
using Movies.API.Repositories.Interfaces;

namespace Movies.API.Repositories
{
    public class AuthRespository : IAuthRepository
    {
        private readonly DataContext _dbContext;

        public AuthRespository(DataContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<User> Login(string username)
        {
            var userFromDb = await _dbContext.Users
                .FirstOrDefaultAsync(u => u.Username.ToLower().Equals(username.ToLower()));

            return userFromDb!;
        }

        public async Task<int> Register(User user)
        {
            if(await UserExist(user.Username))
            {
                throw new Exception("User already exist");
            }

            _dbContext.Users.Add(user);  
            await _dbContext.SaveChangesAsync();
            return user.UserId;
        }

        private async Task<bool> UserExist(string username)
        {
            if(await _dbContext.Users.AnyAsync(u => u.Username.ToLower() == username.ToLower()))
            {
                return true;
            }
            return false;
        }
    }
}
