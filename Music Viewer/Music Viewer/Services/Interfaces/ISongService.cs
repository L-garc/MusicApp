using Music_Viewer.Models.API_Contact;
using Music_Viewer.Models.DB_Models;

namespace Music_Viewer.Services.Interfaces {
    public interface ISongService {
        Task<T> GetAllAsync<T>();
        Task<T> GetAsync<T>(int id);
        Task<T> CreateAsync<T>(Song song);
        Task<T> UpdateAsync<T>(Song song);
        Task<T> DeleteAsync<T>(int id);
        Task<T> SendAsync<T>(APIRequest apiRequest);
        string GetMusicUrl();

    }
}
