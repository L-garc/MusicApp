using Music_Viewer.Models.DB_Models;
using Music_Viewer.Services.Interfaces;
using Music_Viewer.Models.API_Contact;
using Microsoft.AspNetCore.Components;

namespace Music_Viewer.Services {
    public class SongService : Service, ISongService {
        private readonly IHttpClientFactory _clientFactory;
        private string musicUrl;
        public SongService(IHttpClientFactory clientFactory, IConfiguration configuration) : base(clientFactory) {
            _clientFactory = clientFactory;
            musicUrl = configuration.GetValue<string>("ServiceUrls:MusicAPI");
        }
        public Task<T> CreateAsync<T>(Song song) {
            return SendAsync<T>(new APIRequest() {
                ApiType = ContactMethods.ApiType.POST,
                Data = song,
                Url = musicUrl + "/api/Song/"
            }) ;
        }

        public Task<T> DeleteAsync<T>(int id) {
            return SendAsync<T>(new APIRequest() {
                ApiType = ContactMethods.ApiType.POST,
                Url = musicUrl + "/api/Song/" + id
            });
        }

        public Task<T> GetAllAsync<T>() {
            return SendAsync<T>(new APIRequest() {
                ApiType = ContactMethods.ApiType.GET,
                Url = musicUrl + "/api/Song/"
            });
        }

        public Task<T> GetAsync<T>(int id) {
            return SendAsync<T>(new APIRequest() {
                ApiType = ContactMethods.ApiType.GET,
                Url = musicUrl + "/api/Song/" + id
            });
        }

        public Task<T> UpdateAsync<T>(Song song) {
            return SendAsync<T>(new APIRequest() {
                ApiType = ContactMethods.ApiType.PUT,
                Data = song,
                Url = musicUrl + "/api/Song/"
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
