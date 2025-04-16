using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using simpleReading.Context;
using simpleReading.Extensions;
using simpleReading.Interfaces;
using simpleReading.Models;

namespace simpleReading.Controllers
{
    public class AuthController(
        IAuthService authService
        ) : Controller
    {
        private readonly IAuthService _authService = authService;

        [HttpGet("/login")]
        public IActionResult Login()
        {
            return View();
        }

        [HttpPost("/login")]
        public async Task<IActionResult> Login(string email, string password)
        {
            var result = await _authService.Login(email, password);
            if (!result.Sucess)
            {
                TempData["ErrorMessage"] = result.Message;
                return View();
            }

            HttpContext.Session.SetObject("logged_user", result.User);

            return RedirectToAction("Index", "Home");
        }

        [HttpGet("/register")]
        public IActionResult Register()
        {
            return View();
        }

        [HttpPost("/register")]
        public async Task<IActionResult> Register(User model)
        {
            var result = await _authService.Register(model);
            if (!result.Sucess)
            {
                TempData["ErrorMessage"] = result.Message;
                return View();
            }
                
            return RedirectToAction("Index", "Home");
        }

        [HttpGet("/logout")]
        public IActionResult Logout()
        {
            HttpContext.Session.Clear();
            return RedirectToAction("Index", "Login");
        }
    }
}
