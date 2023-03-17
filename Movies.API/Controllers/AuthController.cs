using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Movies.Common.Models.Dtos.User;
using Movies.Common.Models.Http;
using Movies.API.Services.Interfaces;

namespace Movies.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IAuthService _authService;

        public AuthController(IAuthService authService)
        {
            _authService = authService;
        }

        [HttpPost("Register")]
        public async Task<ActionResult<ServiceResponse<int>>> Register(UserDto userDto)
        {
            var response = await _authService.Register(userDto);

            return Ok(response);
        }

        [HttpPost("Login")]
        public async Task<ActionResult<ServiceResponse<string>>> Login(UserDto userDto)
        {
            var response = await _authService.Login(userDto);

            return Ok(response);
        }
    }
}
