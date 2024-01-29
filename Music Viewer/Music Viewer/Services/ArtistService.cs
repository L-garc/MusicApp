using Music_Viewer.Models.API_Contact;
using Music_Viewer.Models.DB_Models;
using Music_Viewer.Services.Interfaces;

namespace Music_Viewer.Services {
    public class ArtistService : Service, IArtistService{
        private readonly IHttpClientFactory _clientFactory;
        private string musicUrl;
        public ArtistService(IHttpClientFactory clientFactory, IConfiguration configuration) : base(clientFactory) {
            _clientFactory = clientFactory;
            musicUrl = configuration.GetValue<string>("ServiceUrls:MusicAPI");
        }
        public Task<T> CreateAsync<T>(Artist artist) {
            return SendAsync<T>(new APIRequest() {
                ApiType = ContactMethods.ApiType.POST,
                Data = artist,
                Url = musicUrl + "/api/Artist/"
            });
        }

        public Task<T> DeleteAsync<T>(int id) {
            return SendAsync<T>(new APIRequest() {
                ApiType = ContactMethods.ApiType.POST,
                Url = musicUrl + "/api/Artist/" + id
            });
        }

        public Task<T> GetAllAsync<T>() {
            return SendAsync<T>(new APIRequest() {
                ApiType = ContactMethods.ApiType.GET,
                Url = musicUrl + "/api/Artist/"
            });
        }

        public Task<T> GetAsync<T>(int id) {
            return SendAsync<T>(new APIRequest() {
                ApiType = ContactMethods.ApiType.GET,
                Url = musicUrl + "/api/Artist/" + id
            });
        }

        public Task<T> UpdateAsync<T>(Artist artist) {
            return SendAsync<T>(new APIRequest() {
                ApiType = ContactMethods.ApiType.PUT,
                Data = artist,
                Url = musicUrl + "/api/Artist/"
            });
        }

        public async Task<T> SendAsync<T>(APIRequest apiRequest) {
            return await base.SendAsync<T>(apiRequest);
        }

        public string GetMusicUrl() {
            return musicUrl;
        }
    }
}
