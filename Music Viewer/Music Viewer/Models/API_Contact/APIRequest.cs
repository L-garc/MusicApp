namespace Music_Viewer.Models.API_Contact {
    
    public class APIRequest {
        public ContactMethods.ApiType ApiType { get; set; } = ContactMethods.ApiType.GET;
        public object Data { get; set; }
        public string Url { get; set; }
    }
}
