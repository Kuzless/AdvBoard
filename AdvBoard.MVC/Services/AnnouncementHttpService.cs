using AdvBoard.MVC.Models;

namespace AdvBoard.MVC.Services
{
    public class AnnouncementHttpService
    {
        private readonly HttpClient _httpClient;
        public AnnouncementHttpService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<List<AnnouncementModel>> GetAnnouncementsAsync()
        {
            var response = await _httpClient.GetAsync("api/Announcement");
            if (response.IsSuccessStatusCode)
            {
                var advList = await response.Content.ReadFromJsonAsync<List<AnnouncementModel>>();
                return advList;
            }
            throw new Exception($"Error fetching data: {response.ReasonPhrase}");
        }
    }
}
