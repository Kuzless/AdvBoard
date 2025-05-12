using AdvBoard.MVC.Services;
using Microsoft.AspNetCore.Mvc;

namespace AdvBoard.MVC.Controllers
{
    public class AnnouncementController : Controller
    {
        private readonly AnnouncementHttpService _announcementHttpService;
        public AnnouncementController(AnnouncementHttpService announcementHttpService)
        {
            _announcementHttpService = announcementHttpService;
        }
        public async Task<IActionResult> Index()
        {
            var announcements = await _announcementHttpService.GetAnnouncementsAsync();
            return View(announcements);
        }

        public async Task<IActionResult> UserAnnouncements()
        {
            var announcements = await _announcementHttpService.GetAnnouncementsByUserIdAsync();
            return View(announcements);
        }

        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {
            var announcement = await _announcementHttpService.GetAnnouncementEditByIdAsync(id);
            return View(announcement);
        }
    }
}
