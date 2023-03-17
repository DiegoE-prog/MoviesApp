using Microsoft.IdentityModel.Tokens;
using Movies.Common.Models.Dtos.User;
using Movies.DataAccess.Entities;
using Movies.API.Exceptions;
using Movies.Common.Models.Http;
using Movies.API.Repositories.Interfaces;
using Movies.API.Services.Interfaces;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;

namespace Movies.API.Services
{
    public class AuthService : IAuthService
    {
        private readonly IAuthRepository _authRepository;
        private readonly IConfiguration _configuration;

        public AuthService(IAuthRepository authRepository, IConfiguration configuration)
        {
            _authRepository = authRepository;
            _configuration = configuration;
        }

        public async Task<ServiceResponse<string>> Login(UserDto userDto)
        {
            var serviceResponse = new ServiceResponse<string>();

            var user = await _authRepository.Login(userDto.Username!);

            if (user is null)
            {
                throw new NotFoundException("User not found");
            }

            if (!VerifyPasswordHash(userDto.Password!, user.PasswordHash, user.PasswordSalt))
            {
                throw new Exception("Wrong password");
            }

            var token = CreateToken(user);

            serviceResponse.Data = token;
            serviceResponse.Message = "User logged successfully";

            return serviceResponse;
        }

        public async Task<ServiceResponse<int>> Register(UserDto userDto)
        {
            var serviceResponse = new ServiceResponse<int>();

            CreatePasswordHash(userDto.Password!, out byte[] passwordHash, out byte[] passwordSalt);

            var user = new User();

            user.Username = userDto.Username!;
            user.PasswordHash = passwordHash;
            user.PasswordSalt = passwordSalt;

            var userId = await _authRepository.Register(user);

            serviceResponse.Data = userId;
            serviceResponse.Message = "User created successfully";

            return serviceResponse;
        }

        private void CreatePasswordHash(string password, out byte[] passwordHash, out byte[] passwordSalt)
        {
            using (var hmac = new HMACSHA512())
            {
                passwordSalt = hmac.Key;
                passwordHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));
            }
        }

        private bool VerifyPasswordHash(string password, byte[] passwordHash, byte[] passwordSalt)
        {
            using (var hmac = new HMACSHA512(passwordSalt))
            {
                var computedHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));
                return (computedHash.SequenceEqual(passwordHash));
            }
        }

        private string CreateToken(User user)
        {
            List<Claim> claims = new List<Claim>
            {
                new Claim(ClaimTypes.NameIdentifier, user.UserId.ToString()),
                new Claim(ClaimTypes.Name, user.Username),
                new Claim(ClaimTypes.Role, user.Role)
            };

            var secretToken = _configuration.GetSection("AppSettings:Token").Value;

            var key = new SymmetricSecurityKey(System.Text.Encoding.UTF8
                .GetBytes(secretToken!));

            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha512Signature);

            var token = new JwtSecurityToken(
                claims: claims,
                expires: DateTime.Now.AddDays(1),
                signingCredentials: creds);

            var jwt = new JwtSecurityTokenHandler().WriteToken(token);

            return jwt;
        }


    }
}
