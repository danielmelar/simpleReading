using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using simpleReading.Extensions;
using simpleReading.Models;
using simpleReading.ViewModel;

namespace simpleReading.Controllers;

public class HomeController : Controller
{
    public IActionResult Index()
    {
        var user = HttpContext.Session.GetObject<User>("logged_user");
        if (user == null) return RedirectToAction("Login", "Auth");

        var reads = user.Reads;

        var groupedReads = new GroupedReadsViewModel
        {
            ReadsByYearAndMonth = reads
            .GroupBy(r => r.CreatedAt.Year)
            .ToDictionary(
                yearGroup => yearGroup.Key,
                yearGroup => yearGroup
                .GroupBy(r => r.CreatedAt.Month)
                .ToDictionary(
                    monthGroup => monthGroup.Key,
                    monthGroup => monthGroup.ToList()
                )
            )
        };
        return View(groupedReads);
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
