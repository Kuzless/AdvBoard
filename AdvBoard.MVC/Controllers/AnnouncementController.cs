using AdvBoard.MVC.Models.Requests;
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
            var announcement = await _announcementHttpService.GetAnnouncementEditStructureById(id);
            return View(announcement);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(EditAnnouncementRequest request)
        {
            var result = await _announcementHttpService.EditAnnouncementAsync(request);
            return RedirectToAction("UserAnnouncements");
        }
        [HttpGet]
        public async Task<IActionResult> Add()
        {
            var structure = await _announcementHttpService.GetAnnouncementStructure();
            return View(structure);
        }
        [HttpPost]
        public async Task<IActionResult> Add(AnnouncementAddRequest request)
        {
            var result = await _announcementHttpService.AddAnnouncementAsync(request);
            return RedirectToAction("UserAnnouncements");
        }
        [HttpPost]
        public async Task<IActionResult> Delete(int id)
        {
            var result = await _announcementHttpService.DeleteAnnouncementAsync(id);
            return RedirectToAction("UserAnnouncements");
        }
    }
}
