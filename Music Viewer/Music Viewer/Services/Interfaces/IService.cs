using Music_Viewer.Models.API_Contact;

namespace Music_Viewer.Services.Interfaces
{
    public interface IService
    {
        APIResponse response { get; set; }
        Task<T> SendAsync<T>(APIRequest apiRequest);
    }
}
