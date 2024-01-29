using Microsoft.AspNetCore.Mvc;
using Music_Viewer.Models.API_Contact;
using Music_Viewer.Models.DB_Models;
using Music_Viewer.Services;
using Music_Viewer.Services.Interfaces;
using Newtonsoft.Json;

namespace Music_Viewer.Controllers {
    public class ArtistController : Controller {
        private readonly IArtistService _artistService;
        public ArtistController(IArtistService artistService) {
            _artistService = artistService;
        }
        public async Task<IActionResult> IndexArtist() {
            List<Artist> artistList = new List<Artist>();

            APIResponse response = await _artistService.GetAllAsync<APIResponse>();

            if (response != null && response.Success) {
                artistList = JsonConvert.DeserializeObject<List<Artist>>(Convert.ToString(response.Result));
            }

            return View(artistList);
        }
    }
}

/*
 * Artist model does not need to have data joined
 * The Artist Page need only list the artist names
 */