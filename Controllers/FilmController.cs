using FilmProject.DTO;
using Microsoft.AspNetCore.Mvc;
using FilmProject.Services.Concrete;

namespace FilmProject.Controllers
{
    [ApiController]
    [Route("api/films")]
    public class FilmController : ControllerBase
    {
        private readonly FilmService _filmService; //IFilmService  BURASI UNUTULMUŞ IFILMSERVICE OLACAK

        public FilmController(FilmService filmService)
        {
            _filmService = filmService;
        }

        [HttpGet]
        public ActionResult<List<DtoFilteredFilms>> GetAllFilms()
        {
            return Ok(_filmService.GetAllFilms());
        }

        [HttpGet("filtered")]
        public ActionResult<List<DtoFilteredFilms>> GetFilteredFilms([FromQuery] DtoFilmsQuery filmDto)
        {
            var films = _filmService.GetFilteredFilms(filmDto);
            return Ok(films);
        }

        [HttpGet("details")]
        public async Task<IActionResult> GetDetailsFiltered(int FilmId)
        {
            var filmDetails = _filmService.GetDetailsFiltered(FilmId);

            if (filmDetails == null)
            {
                return NotFound("Film not found");
            }

            return Ok(filmDetails);
        }

        [HttpPost]
        public IActionResult AddFilm(DtoAddFilm filmdto)
        {
            _filmService.AddFilm(filmdto);
            return Ok("Film added successfully");
        }

        [HttpPut("{id}")]
        public IActionResult Update(int id, [FromBody] DtoUpdateFilm filmdto)
        {
            _filmService.UpdateFilm(id, filmdto);
            return Ok("Film updated.");
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            _filmService.DeleteFilm(id);
            return Ok("Film deleted successfully.");
        }
        
        [HttpPut("mark-watched/{filmId}")]
        public IActionResult MarkAsWatched(int filmId, [FromQuery] Guid userId)
        {
            var result = _filmService.MarkAsWatched(filmId, userId);

            if (result)
            {
                return Ok("Film marked as watched.");
            }
            else
            {
                return NotFound("Film not found for the user.");
            }
        }
        
        [HttpGet("watchlist")]
        public ActionResult<List<DtoFilmDetails>> GetWatchlist([FromQuery] Guid userId)
        {
            try
            {
                var watchlist = _filmService.GetWatchlist(userId);
                return Ok(watchlist);
            }
            catch (Exception ex)
            {
                return NotFound(ex.Message);
            }
        }
    }
}