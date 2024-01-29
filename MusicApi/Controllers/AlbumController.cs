using Microsoft.AspNetCore.Mvc;
using MusicApi.Data;
using MusicApi.Models;
using MusicApi.Models.DB_Models;
using MusicApi.Repository;

namespace MusicApi.Controllers {
    [Route("api/Album")]
    [ApiController]
    public class AlbumController : ControllerBase {
        private readonly IRepo<Album> _dbAlbum;
        protected APIResponse _response;
        public AlbumController(IRepo<Album> dbAlbum) {
            _dbAlbum = dbAlbum;
            _response = new APIResponse();
        }

        [HttpGet] //Get All Albums
        public async Task<ActionResult<APIResponse>> GetAlbums() {
            try {
                IEnumerable<Album> albumList = await _dbAlbum.GetAllAsync();

                //Setup Response object
                _response.StatusCode = System.Net.HttpStatusCode.OK;
                _response.Result = albumList;
                _response.Success = true;

                return Ok(_response);
            }
            catch (Exception ex) {
                _response.ErrorMessages = new List<string> { ex.Message };
                _response.Success = false;

                return BadRequest(_response);
            }
        }

        [HttpGet("{id:int}", Name = "GetAlbum")] //Get a single album by its ID
        public async Task<ActionResult<APIResponse>> GetAlbum(int id) {
            try {
                if (id == 0) {
                    _response.ErrorMessages = new List<string>() { "Album either not specified, or invalid" };
                    _response.StatusCode = System.Net.HttpStatusCode.BadRequest;
                    _response.Success = false;

                    return BadRequest(_response);
                }

                var album = await _dbAlbum.GetAsync(u => u.albumID == id);
                if (album == null) {
                    _response.ErrorMessages = new List<string>() { "Album not found" };
                    _response.Success = true;
                    _response.StatusCode = System.Net.HttpStatusCode.NotFound;
                    return NotFound(_response);
                }

                _response.Success = true;
                _response.Result = album;
                _response.StatusCode = System.Net.HttpStatusCode.OK;
                return Ok(_response);
            }
            catch (Exception ex) {
                _response.Success = false;
                _response.ErrorMessages = new List<string>() { ex.Message };
                return _response;
            }
        }

    }
}
