using AdvBoard.MVC.Models.ViewModels;

namespace AdvBoard.MVC.Services
{
    public class AnnouncementHttpService
    {
        private readonly HttpClient _httpClient;
        private readonly string _advApi = "api/Announcement/";
        public AnnouncementHttpService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<List<AnnouncementViewModel>> GetAnnouncementsAsync()
        {
            var response = await _httpClient.GetAsync(_advApi);
            if (response.IsSuccessStatusCode)
            {
                var advList = await response.Content.ReadFromJsonAsync<List<AnnouncementViewModel>>();
                return advList!;
            }
            throw new Exception($"Error fetching data: {response.ReasonPhrase}");
        }

        public async Task<AnnouncementEditViewModel> GetAnnouncementEditByIdAsync(int id)
        {
            var response = await _httpClient.GetAsync(_advApi + id);
            if (response.IsSuccessStatusCode)
            {
                var adv = await response.Content.ReadFromJsonAsync<AnnouncementEditViewModel>();
                return adv!;
            }
            throw new Exception($"Error fetching data: {response.ReasonPhrase}");
        }

        public async Task<List<AnnouncementViewModel>> GetAnnouncementsByUserIdAsync()
        {
            var response = await _httpClient.GetAsync(_advApi + "user/announcements/");
            if (response.IsSuccessStatusCode)
            {
                var adv = await response.Content.ReadFromJsonAsync<List<AnnouncementViewModel>>();
                return adv!;
            }
            throw new Exception($"Error fetching data: {response.ReasonPhrase}");
        }
    }
}
