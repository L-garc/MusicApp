using System.Net;

namespace Music_Viewer.Models.API_Contact {
    public class APIResponse {
        public HttpStatusCode StatusCode { get; set; }
        public bool Success { get; set; }
        public List<string> ErrorMessages { get; set; }
        public object Result { get; set; }
    }
}
