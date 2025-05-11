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
    }
}
