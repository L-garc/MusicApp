using Microsoft.AspNetCore.Mvc;
using MusicApi.Models;
using MusicApi.Models.DB_Models;
using MusicApi.Repository;

namespace MusicApi.Controllers {
    [Route("api/Song")]
    [ApiController]
    public class SongController : ControllerBase { //ControllerBase has no support for MVC Views, not needed for APIs
        private readonly IRepo<Song> _dbSong;
        protected APIResponse _response;
        public SongController(IRepo<Song> dbSong) {
            _dbSong = dbSong;
            _response = new APIResponse();
        }
        
        [HttpGet]
        public async Task<ActionResult<APIResponse>> GetSongs() {
            try {
                IEnumerable<Song> songList = await _dbSong.GetAllAsync();
                
                _response.Result = songList;
                _response.StatusCode = System.Net.HttpStatusCode.OK;
                _response.Success = true;
                return Ok(_response);
            }
            catch (Exception ex) {
                _response.Success = false;
                _response.ErrorMessages.Add(ex.Message);
                return BadRequest(_response);
            }
        }

        [HttpGet("{id:int}", Name = "GetSong")] //Get a single album by its ID
        public async Task<ActionResult> GetSong(int id) {
            try {
                if (id == 0) {
                    _response.Success = true;
                    _response.StatusCode = System.Net.HttpStatusCode.BadRequest;
                    _response.ErrorMessages = new List<string>() { "Song either not specified, or invalid" };
                    return BadRequest(_response);
                }

                var song = await _dbSong.GetAsync(u => u.songID == id);
                if (song == null) {
                    _response.Success = true;
                    _response.StatusCode = System.Net.HttpStatusCode.NotFound;
                    _response.ErrorMessages = new List<string>() { "Song not found" };
                    return NotFound(_response);
                }

                _response.Result = song;
                _response.StatusCode = System.Net.HttpStatusCode.OK;
                _response.Success = true;
                return Ok(_response);
            }
            catch (Exception ex) {
                _response.Success = false;
                _response.StatusCode = System.Net.HttpStatusCode.BadRequest;
                _response.ErrorMessages = new List<string>() { ex.Message };
                return BadRequest(_response);
            }
        }
    }
}
