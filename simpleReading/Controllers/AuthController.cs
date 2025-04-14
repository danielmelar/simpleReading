using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using simpleReading.Context;
using simpleReading.Extensions;
using simpleReading.Models;
using simpleReading.Service;

namespace simpleReading.Controllers
{
    public class AuthController(AuthService authService, AppDbContext context) : Controller
    {
        private readonly AuthService _authService = authService;
        private readonly AppDbContext _context = context;

        [HttpGet("/login")]
        public IActionResult Login()
        {
            return View();
        }

        [HttpPost("/login")]
        public async Task<IActionResult> Login(string email, string password)
        {
            var user = await _authService.Login(email, password);
            if (user is not null)
            {
                //var reads = _context.Read.AllAsync(predicate: x => x.UserId == user.Id);
                var reads = await _context.Read.ToListAsync(new CancellationToken());

                user.Reads = reads.FindAll(x => x.UserId == user.Id);

                HttpContext.Session.SetObject("currentUser", user);
                

                return RedirectToAction("Index", "Home");
            }
            
            return View("Login");
        }

        [HttpGet("/register")]
        public IActionResult Register()
        {
            return View();
        }

        [HttpPost("/register")]
        public async Task<IActionResult> Register(User model)
        {
            if (ModelState.IsValid)
            {
                await _authService.Register(model);
                return RedirectToAction("Index", "Home");
            }
            return View(model);
        }

        [HttpGet("/logout")]
        public IActionResult Logout()
        {
            HttpContext.Session.Clear();
            return RedirectToAction("Index", "Login");
        }
    }
}
