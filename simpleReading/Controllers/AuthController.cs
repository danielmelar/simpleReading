using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using simpleReading.Context;
using simpleReading.Extensions;
using simpleReading.Models;
using simpleReading.Service;

namespace simpleReading.Controllers
{
    public class AuthController(AuthService authService, UserService userService, AppDbContext context) : Controller
    {
        private readonly AuthService _authService = authService;
        private readonly AppDbContext _context = context;
        private readonly UserService _userService = userService;

        [HttpGet("/login")]
        public IActionResult Login()
        {
            return View();
        }

        [HttpPost("/login")]
        public async Task<IActionResult> Login(string email, string password)
        {
            var user = await _userService.GetUserByLoginCredentials(email, password);
            if (user == null) return View("Login");

            var reads = await _context.Read.ToListAsync(new CancellationToken());

            user.Reads = reads.FindAll(x => x.UserId == user.Id);

            HttpContext.Session.SetObject("currentUser", user);

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
            await _authService.Register(model);
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
