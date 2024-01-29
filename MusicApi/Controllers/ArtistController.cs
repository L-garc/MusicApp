using Microsoft.AspNetCore.Mvc;
using MusicApi.Models;
using MusicApi.Models.DB_Models;
using MusicApi.Repository;

namespace MusicApi.Controllers {

    namespace MusicApi.Controllers {
        [Route("api/Artist")]
        [ApiController]
        public class ArtistController : ControllerBase { //ControllerBase has no support for MVC Views, not needed for APIs
            private readonly IRepo<Artist> _dbArtist;
            protected APIResponse _response;

            public ArtistController(IRepo<Artist> dbArtist) {
                _dbArtist = dbArtist;
                _response = new APIResponse();
            }

            [HttpGet]
            public async Task<ActionResult<APIResponse>> GetArtists() {
                try {
                    IEnumerable<Artist> artistList = await _dbArtist.GetAllAsync();

                    _response.Result = artistList;
                    _response.Success = true;
                    _response.StatusCode = System.Net.HttpStatusCode.OK;

                    return Ok(_response);
                }
                catch (Exception ex) {

                    _response.ErrorMessages = new List<string>() { ex.Message };
                    _response.Success = false;
                    _response.StatusCode = System.Net.HttpStatusCode.BadRequest;
                    return BadRequest(_response);
                }
            }

            [HttpGet("{id:int}", Name = "GetArtist")] //Get a single album by its ID
            public async Task<ActionResult> GetArtist(int id) {
                try {
                    if (id == 0) {
                        _response.StatusCode = System.Net.HttpStatusCode.BadRequest;
                        _response.Success = true;
                        _response.ErrorMessages = new List<string>() { "Artist either not specified, or invalid" };
                        return BadRequest(_response);
                    }

                    var artist = await _dbArtist.GetAsync(u => u.artistID == id);
                    if (artist == null) {
                        _response.StatusCode = System.Net.HttpStatusCode.NotFound;
                        _response.Success = true;
                        _response.ErrorMessages = new List<string>() { "Artist not Found" };
                        return NotFound(_response);
                    }

                    _response.Success = true;
                    _response.Result = artist;
                    _response.StatusCode = System.Net.HttpStatusCode.OK;
                    return Ok(_response);
                }
                catch (Exception ex) {
                    _response.ErrorMessages = new List<string>() { ex.Message };
                    _response.Success = false;
                    _response.StatusCode = System.Net.HttpStatusCode.BadRequest;
                    return BadRequest(_response);
                }
            }
        }
    }
}
