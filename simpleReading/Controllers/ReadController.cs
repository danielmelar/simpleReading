using Microsoft.AspNetCore.Mvc;
using simpleReading.Context;
using simpleReading.Extensions;
using simpleReading.Models;
using System.Threading.Tasks;

namespace simpleReading.Controllers
{
    public class ReadController(AppDbContext context) : Controller
    {
        private readonly AppDbContext _context = context;

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(Read read)
        {
            var currentUser = HttpContext.Session.GetObject<User>("currentUser");
            read.UserId = currentUser.Id;

            await _context.AddAsync(read);
            await _context.SaveChangesAsync();

            return View(read);
        }

        public IActionResult GetReads()
        {
            return View();
        }
    }
}
