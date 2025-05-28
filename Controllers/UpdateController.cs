using Microsoft.AspNetCore.Mvc;
using FilmProject.Services;

namespace FilmProject.Controllers
{
    [ApiController]
    [Route("api/update")]
    public class UpdateController : ControllerBase
    {
        private readonly FilmUpdateService _filmUpdateService;

        public UpdateController(FilmUpdateService filmUpdateService)
        {
            _filmUpdateService = filmUpdateService;
        }

        [HttpPost]
        public async Task<IActionResult> Update()
        {
            await _filmUpdateService.UpdateFilmsAsync();
            return Ok("Filmler TMDB'den başarıyla çekildi ve güncellendi.");
        }
    }
}