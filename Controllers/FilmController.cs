using FilmProject.DTO;
using Microsoft.AspNetCore.Mvc;
using FilmProject.Services.Concrete;
using FilmProject.Models;

namespace FilmProject.Controllers
{
    [ApiController]
    [Route("api/films")]
    public class FilmController : ControllerBase
    {
        private readonly IFilmService _filmService;

        public FilmController(FilmService filmService)
        {
            _filmService = filmService;
        }

        [HttpGet]
        public ActionResult<ApiResponse<List<DtoFilteredFilms>>> GetAllFilms()
        {
            var result = _filmService.GetAllFilms();

            if (!string.IsNullOrEmpty(result.Error))
                return NotFound(result);

            return Ok(result);
        }

        [HttpGet("filtered")]
        public ActionResult<ApiResponse<List<DtoFilteredFilms>>> GetFilteredFilms([FromQuery] DtoFilmsQuery filmDto)
        {
            var result = _filmService.GetFilteredFilms(filmDto);

            if (!string.IsNullOrEmpty(result.Error))
                return NotFound(result);

            return Ok(result);
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
            var result = _filmService.AddFilm(filmdto);

            if (!string.IsNullOrEmpty(result.Error))
            {
                return BadRequest(result);
            }

            return Ok(result);
        }

        [HttpPut("{id}")]
        public IActionResult Update(int id, [FromBody] DtoUpdateFilm filmdto)
        {
            var result = _filmService.UpdateFilm(id, filmdto);
            if (!string.IsNullOrEmpty(result.Error))
            {
                return NotFound(result);
            }

            return Ok(result);
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            var result = _filmService.DeleteFilm(id);
            if (!string.IsNullOrEmpty(result.Error))
            {
                return NotFound(result);
            }

            return Ok(result);
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