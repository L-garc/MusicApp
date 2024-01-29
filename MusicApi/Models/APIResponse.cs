using System.Net;

namespace MusicApi.Models {
    public class APIResponse {
        public APIResponse() {
            ErrorMessages = new List<string>();
        }

        public HttpStatusCode StatusCode { get; set; }
        public bool Success { get; set; }
        public List<string> ErrorMessages { get; set; }
        public object Result { get; set; }
    }
}
