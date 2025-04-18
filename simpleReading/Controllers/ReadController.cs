using Microsoft.AspNetCore.Mvc;
using simpleReading.Extensions;
using simpleReading.Interfaces;
using simpleReading.Models;
using simpleReading.ViewModel;

namespace simpleReading.Controllers
{
    public class ReadController(
        IReadService readService
        ) : Controller
    {
        private readonly IReadService _readService = readService;
        private const string logged = "logged_user";

        public async Task<bool> UpdateLocalSession(User currentUser)
        {
            var user = HttpContext.Session.GetObject<User>(logged);

            user = await _readService.UpdateCurrentUserReads(currentUser);

            HttpContext.Session.SetObject(logged, user);

            return true;
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(Read read)
        {
            var currentUser = HttpContext.Session.GetObject<User>(logged);

            await _readService.Create(read, currentUser.Id);

            await UpdateLocalSession(currentUser);

            return View(read);
        }

        public async Task<IActionResult> Delete(string id)
        {
            await _readService.Delete(id);

            await UpdateLocalSession(HttpContext.Session.GetObject<User>(logged));

            return RedirectToAction("Update");
        }

        public IActionResult Update()
        {
            var user = HttpContext.Session.GetObject<User>(logged);
            if (user == null)
                return RedirectToAction("Login", "Auth");

            var reads = _readService.MountViewModel(user.Reads);

            return View(new UpdateViewModel(null, reads));
        }

        [HttpPost]
        public async Task<IActionResult> Update(UpdateViewModel updateRead)
        {
            var user = HttpContext.Session.GetObject<User>(logged);
            if (user == null)
                return RedirectToAction("Login", "Auth");

            var read = await _readService.Update(updateRead.Read);

            user = await _readService.UpdateCurrentUserReads(user);

            var reads = _readService.MountViewModel(user.Reads);

            await UpdateLocalSession(user);

            return View(new UpdateViewModel(read.Read, reads));
        }

        [HttpGet("leituras/{username}")]
        public async Task<IActionResult> GetReadsByUsername(string username)
        {
            var reads = await _readService.GetReadsByUsername(username);

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
    }
}
