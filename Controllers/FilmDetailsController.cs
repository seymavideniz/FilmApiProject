using FilmProject.DTO;
using FilmProject.Services.Abstract;
using Microsoft.AspNetCore.Mvc;
using FilmProject.Models;

namespace FilmProject.Controllers;

[Route("api/[controller]")]
[ApiController]
public class FilmDetailsController : ControllerBase
{
    private readonly IFilmDetailsService _filmDetailsService;

    [HttpPost("AddFilmDetails")]
    public IActionResult AddFilmDetails([FromBody] FilmDetails filmDetails)
    {
        var result = _filmDetailsService.AddFilmDetails(filmDetails);
        if (result.Error == null)
        {
            return Ok(result.Message);
        }

        return BadRequest(result.Message);
    }


    [HttpPost("GetFilmDetails")]
    public IActionResult AddRating([FromBody] DtoAddRating ratingDto)
    {
        var result = _filmDetailsService.AddRating(ratingDto);

        if (result.Error != null)
        {
            return BadRequest(new { error = result.Error, message = result.Message });
        }

        return Ok(new { message = result.Message });
    }
}