using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using Movies.Common.Models.Dtos.User;
using Movies.WEB.Models.Http;
using Movies.WEB.Services.IServices;
using Newtonsoft.Json;
using System.Security.Claims;

namespace Movies.WEB.Controllers
{
    public class AuthController : Controller
    {
        private readonly IAuthService _authService;

        public AuthController(IAuthService authService)
        {
            _authService= authService;
        }

        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Login(UserDto loginDto)
        {
            if (ModelState.IsValid)
            {
                var response = await _authService.LoginAsync<ResponseDto>(loginDto);

                if(response is not null && response.Success)
                {
                    var token = Convert.ToString(response.Data);

                    var identity = new ClaimsIdentity(SD.ParseClaimsFromJwt(token!),"jwt");
                    var principal = new ClaimsPrincipal(identity);
                    var props = new AuthenticationProperties();

                    await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, principal, props);
                    
                    //HttpContext.Session.SetString("access_token", token!);

                    return RedirectToAction("Index", "Home");
                }
                else
                {
                    ViewBag.Error = response?.Message;
                }
                   
            }

            return View();
        }

        [HttpGet]
        public IActionResult Register()
        {
            return View();
        }
        
        [HttpPost]
        public async Task<IActionResult> Register(UserDto registerDto)
        {
            if(ModelState.IsValid)
            {
                var response = await _authService.RegisterAsync<ResponseDto>(registerDto);

                if(response is not null && response.Success)
                {
                    return RedirectToAction("Login");
                }
                else
                {
                    ViewBag.Error = response?.Message;
                }
            }
            return View();
        }

        [HttpGet]
        public async Task<IActionResult> Logout()
        {
            await HttpContext.SignOutAsync();

            return RedirectToAction("Index", "Home");
        }

        [HttpGet]
        public IActionResult AccessDenied()
        {
            return View();
        }
    }
}
