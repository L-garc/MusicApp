using Music_Viewer.Models.API_Contact;
using Music_Viewer.Models.DB_Models;
using Music_Viewer.Services.Interfaces;

namespace Music_Viewer.Services {
    public class AlbumService : Service, IAlbumService {
        private readonly IHttpClientFactory _clientFactory;
        private string musicUrl;
        public AlbumService(IHttpClientFactory clientFactory, IConfiguration configuration) : base(clientFactory) {
            _clientFactory = clientFactory;
            musicUrl = configuration.GetValue<string>("ServiceUrls:MusicAPI");
        }
        public Task<T> CreateAsync<T>(Album album) {
            return SendAsync<T>(new APIRequest() {
                ApiType = ContactMethods.ApiType.POST,
                Data = album,
                Url = musicUrl + "/api/Album/"
            });
        }

        public Task<T> DeleteAsync<T>(int id) {
            return SendAsync<T>(new APIRequest() {
                ApiType = ContactMethods.ApiType.POST,
                Url = musicUrl + "/api/Album/" + id
            });
        }

        public Task<T> GetAllAsync<T>() {
            return SendAsync<T>(new APIRequest() {
                ApiType = ContactMethods.ApiType.GET,
                Url = musicUrl + "/api/Album/"
            });
        }

        public Task<T> GetAsync<T>(int id) {
            return SendAsync<T>(new APIRequest() {
                ApiType = ContactMethods.ApiType.GET,
                Url = musicUrl + "/api/Album/" + id
            });
        }

        public Task<T> UpdateAsync<T>(Album album) {
            return SendAsync<T>(new APIRequest() {
                ApiType = ContactMethods.ApiType.PUT,
                Data = album,
                Url = musicUrl + "/api/Album/"
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
