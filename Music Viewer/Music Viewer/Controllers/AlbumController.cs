using Microsoft.AspNetCore.Mvc;
using Music_Viewer.Models.API_Contact;
using Music_Viewer.Models.DB_Models;
using Music_Viewer.Services;
using Music_Viewer.Services.Interfaces;
using Newtonsoft.Json;

namespace Music_Viewer.Controllers {
    public class AlbumController : Controller {
        private readonly IAlbumService _albumService;
        public AlbumController(IAlbumService albumService) {
            _albumService = albumService;
        }
        public async Task<IActionResult> IndexAlbum() {
            List<Album> albumList = new List<Album>();

            APIResponse response = await _albumService.GetAllAsync<APIResponse>();

            if (response != null && response.Success) {
                albumList = JsonConvert.DeserializeObject<List<Album>>(Convert.ToString(response.Result));
            }

            albumList = await JoinData(albumList);

            return View(albumList);
        }

        public async Task<List<Album>> JoinData(List<Album> albumList) {
            List<Song> songList = new List<Song>();
            List<Artist> artistList = new List<Artist>();

            APIResponse songRes = await _albumService.SendAsync<APIResponse>(new APIRequest() {
                ApiType = ContactMethods.ApiType.GET,
                Url = _albumService.GetMusicUrl() + "/api/Song/"
            });

            APIResponse artistRes = await _albumService.SendAsync<APIResponse>(new APIRequest() {
                ApiType = ContactMethods.ApiType.GET,
                Url = _albumService.GetMusicUrl() + "/api/Artist/"
            });

            if (songRes != null && songRes.Success) {
                songList = JsonConvert.DeserializeObject<List<Song>>(Convert.ToString(songRes.Result));
            }

            if (artistRes != null && artistRes.Success) {
                artistList = JsonConvert.DeserializeObject<List<Artist>>(Convert.ToString(artistRes.Result));
            }

            foreach (Album album in albumList) {
                album.Artist = artistList.Find(u => u.artistID == album.artistID);
                album.Songs = songList.FindAll(u => u.albumID == album.albumID);
            }

            return albumList;
        }
    }
}
