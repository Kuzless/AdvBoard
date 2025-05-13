using System.Net.Http.Headers;
using AdvBoard.MVC.Models.Requests;
using AdvBoard.MVC.Models.ViewModels;

namespace AdvBoard.MVC.Services
{
    public class AnnouncementHttpService
    {
        private readonly HttpClient _httpClient;
        private readonly string _advApi = "api/Announcement/";

        public AnnouncementHttpService(HttpClient httpClient, IHttpContextAccessor httpContextAccessor)
        {
            _httpClient = httpClient;
            string token = httpContextAccessor.HttpContext.Request.Cookies["AccessToken"];
            _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
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
        public async Task<List<AnnouncementViewModel>> GetAnnouncementsByUserIdAsync()
        {
            var response = await _httpClient.GetAsync(_advApi + "user/");
            if (response.IsSuccessStatusCode)
            {
                var adv = await response.Content.ReadFromJsonAsync<List<AnnouncementViewModel>>();
                return adv!;
            }
            throw new Exception($"Error fetching data: {response.ReasonPhrase}");
        }
        public async Task<AnnouncementEditViewModel> GetAnnouncementEditStructureById(int id)
        {
            var response = await _httpClient.GetAsync(_advApi + $"user/{id}");
            if (response.IsSuccessStatusCode)
            {
                var adv = await response.Content.ReadFromJsonAsync<AnnouncementEditViewModel>();
                return adv!;
            }
            throw new Exception($"Error fetching data: {response.ReasonPhrase}");
        }

        public async Task<bool> EditAnnouncementAsync(EditAnnouncementRequest request)
        {
            var response = await _httpClient.PutAsJsonAsync(_advApi, request);
            if (response.IsSuccessStatusCode)
            {
                return true;
            }
            throw new Exception($"Error fetching data: {response.ReasonPhrase}");
        }

        public async Task<bool> DeleteAnnouncementAsync(int id)
        {
            var response = await _httpClient.DeleteAsync(_advApi + id);
            if (response.IsSuccessStatusCode)
            {
                return true;
            }
            throw new Exception($"Error fetching data: {response.ReasonPhrase}");
        }
        public async Task<AnnouncementAddViewModel> GetAnnouncementStructure()
        {
            var response = await _httpClient.GetAsync(_advApi + "user/add");
            if (response.IsSuccessStatusCode)
            {
                var adv = await response.Content.ReadFromJsonAsync<AnnouncementAddViewModel>();
                return adv!;
            }
            throw new Exception($"Error fetching data: {response.ReasonPhrase}");
        }

        public async Task<bool> AddAnnouncementAsync(AnnouncementAddRequest request)
        {
            var response = await _httpClient.PostAsJsonAsync(_advApi, request);
            if (response.IsSuccessStatusCode)
            {
                return true;
            }
            throw new Exception($"Error fetching data: {response.ReasonPhrase}");
        }
    }
}
