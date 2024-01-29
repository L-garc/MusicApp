using Microsoft.AspNetCore.Mvc;
using Music_Viewer.Models.API_Contact;
using Music_Viewer.Models.DB_Models;
using Music_Viewer.Services.Interfaces;
using Newtonsoft.Json;
using System.Linq;

namespace Music_Viewer.Controllers {
    public class SongController : Controller {
        private readonly ISongService _songService;

        public SongController(ISongService songService) {
            _songService = songService;
        }
        public async Task<IActionResult> IndexSong() {
            List<Song> songList = new List<Song>();

            APIResponse response = await _songService.GetAllAsync<APIResponse>();

            if (response != null && response.Success) {
                songList = JsonConvert.DeserializeObject<List<Song>>(Convert.ToString(response.Result));
            }

            songList = await JoinData(songList);

            return View(songList);
        }

        public async Task<List<Song>> JoinData(List<Song> songList) {
            List<Album> albumList = new List<Album>();
            List<Artist> artistList = new List<Artist>();
            
            APIResponse albumRes = await _songService.SendAsync<APIResponse>(new APIRequest() {
                ApiType = ContactMethods.ApiType.GET,
                Url = _songService.GetMusicUrl() + "/api/Album/"
            });

            APIResponse artistRes = await _songService.SendAsync<APIResponse>(new APIRequest() {
                ApiType = ContactMethods.ApiType.GET,
                Url = _songService.GetMusicUrl() + "/api/Artist/"
            });

            if (albumRes != null && albumRes.Success) {
                albumList = JsonConvert.DeserializeObject<List<Album>>(Convert.ToString(albumRes.Result));
            }

            if (artistRes != null && artistRes.Success) {
                artistList = JsonConvert.DeserializeObject<List<Artist>>(Convert.ToString(artistRes.Result));
            }

            foreach(Song song in songList) {
                song.Artist = artistList.Find(u => u.artistID == song.artistID);
                song.Album = albumList.Find(u => u.albumID == song.albumID);
            }

            return songList;
        }
    }
}
