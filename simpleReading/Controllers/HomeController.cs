using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using simpleReading.Extensions;
using simpleReading.Models;

namespace simpleReading.Controllers;

public class HomeController : Controller
{
    public IActionResult Index()
    {
        var user = HttpContext.Session.GetObject<User>("logged_user");
        if (user is null)
        {
            return RedirectToAction("Login", "Auth");
        }
        return View();
    }

    public IActionResult Privacy()
    {
        return View();
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}
