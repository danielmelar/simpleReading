﻿using Microsoft.AspNetCore.Mvc;
using simpleReading.Context;
using simpleReading.Models;
using simpleReading.Service;

namespace simpleReading.Controllers
{
    public class AuthController(AuthService authService) : Controller
    {
        private readonly AuthService _authService = authService;

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
                HttpContext.Session.SetString("logged_username", user.Username);
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
    }
}
