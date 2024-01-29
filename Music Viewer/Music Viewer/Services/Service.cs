using Music_Viewer.Models.API_Contact;
using Music_Viewer.Services.Interfaces;
using Newtonsoft.Json;
using System.Text;

namespace Music_Viewer.Services
{
    public class Service : IService {
        public APIResponse response { get; set; }
        public IHttpClientFactory httpClient;
        
        public Service(IHttpClientFactory httpClient) {
            this.response = new APIResponse();
            this.httpClient = httpClient;
        }

        public async Task<T> SendAsync<T>(APIRequest apiRequest) {
            try {
                var client = httpClient.CreateClient("Music_Viewer");
                HttpRequestMessage message = new HttpRequestMessage();
                message.Headers.Add("Accept", "application/json");
                message.RequestUri = new Uri(apiRequest.Url);

                if (apiRequest.Data != null) {
                    message.Content = new StringContent(JsonConvert.SerializeObject(apiRequest.Data), Encoding.UTF8, "application/json");
                }

                switch (apiRequest.ApiType) {
                    case ContactMethods.ApiType.POST: {
                        message.Method = HttpMethod.Post;
                        break;
                    }
                    case ContactMethods.ApiType.PUT: {
                        message.Method = HttpMethod.Put;
                        break;
                    }
                    case ContactMethods.ApiType.DELETE: {
                        message.Method = HttpMethod.Delete;
                        break;
                    }
                    default: {
                        message.Method = HttpMethod.Get;
                        break;
                    }
                }

                HttpResponseMessage apiResponse = null;
                apiResponse = await client.SendAsync(message);

                string apiContent = await apiResponse.Content.ReadAsStringAsync();
                try {
                    APIResponse ApiResponse = JsonConvert.DeserializeObject<APIResponse>(apiContent) ?? new() { StatusCode = System.Net.HttpStatusCode.InternalServerError, Success = false, ErrorMessages = new List<string>() { "Fatal Error!"} };
                    if (ApiResponse != null && (apiResponse.StatusCode == System.Net.HttpStatusCode.BadRequest || apiResponse.StatusCode == System.Net.HttpStatusCode.NotFound)) {
                        ApiResponse.StatusCode = System.Net.HttpStatusCode.BadRequest;
                        ApiResponse.Success = false;

                        var res = JsonConvert.SerializeObject(ApiResponse);
                        var returnObj = JsonConvert.DeserializeObject<T>(res);

                        return returnObj;
                    }
                }
                catch (Exception ex) {
                    var exceptionResponse = JsonConvert.DeserializeObject<T>(apiContent);
                    return exceptionResponse;
                }

                var APIResponse = JsonConvert.DeserializeObject<T>(apiContent);
                return APIResponse;
            }
            catch (Exception ex) {
                var dto = new APIResponse {
                    ErrorMessages = new List<string>() { Convert.ToString(ex.Message) },
                    Success = false
                };

                var res = JsonConvert.SerializeObject(dto);
                var APIResponse = JsonConvert.DeserializeObject<T>(res);
                return APIResponse;
            }
        }
    }
}
