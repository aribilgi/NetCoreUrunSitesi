using Entities;

namespace NetCoreUrunSitesi.Services
{
    public class AppUsersApiService
    {
        private readonly HttpClient _httpClient; // Api işlemleri httpclient nesneleriyle yapılır

        public AppUsersApiService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<List<AppUser>> GetAllAppUsers()
        {
            return await _httpClient.GetFromJsonAsync<List<AppUser>>("AppUsers"); // Api deki app users controller a get isteği yaptık, oradan dönen json datayı List<AppUser> ile appuser listesine çevirdik ve bu metodun çağrıldığı yere gönderdik
        }

        public async Task<AppUser> AddAsync(AppUser appUser)
        {
            var response = await _httpClient.PostAsJsonAsync("AppUsers", appUser); // parametreden gelen appuser nesnesini apiye post yöntemiyle gönderip oradan dönen cevabı response değişkenine atadık
            if (!response.IsSuccessStatusCode) return null; // eğer dönen cevap başarılı değilse geriye null dön

            var responseBody = await response.Content.ReadFromJsonAsync<AppUser>(); // eğer işlem başarılıysa response içinden json türündeki appuser nesnesini oku ve geri döndür
            return responseBody;
        }

        public async Task<AppUser> GetByIdAsync(int id)
        {
            return await _httpClient.GetFromJsonAsync<AppUser>($"AppUsers/{id}");
        }

    }
}
