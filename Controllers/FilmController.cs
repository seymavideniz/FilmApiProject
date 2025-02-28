using FilmProject.DTO;
using FilmProject.Services;
using Microsoft.AspNetCore.Mvc;
using FilmProject.Enum;

namespace FilmProject.Controllers
{
    [ApiController]
    [Route("api/films")]
    public class FilmController : ControllerBase
    {
        private readonly FilmService _filmService;

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
    }
}