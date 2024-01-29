using Music_Viewer.Models.API_Contact;
using Music_Viewer.Models.DB_Models;

namespace Music_Viewer.Services.Interfaces {
    public interface IArtistService {
        Task<T> GetAllAsync<T>();
        Task<T> GetAsync<T>(int id);
        Task<T> CreateAsync<T>(Artist artist);
        Task<T> UpdateAsync<T>(Artist artist);
        Task<T> DeleteAsync<T>(int id);
        Task<T> SendAsync<T>(APIRequest apiRequest);
        string GetMusicUrl();
    }
}
