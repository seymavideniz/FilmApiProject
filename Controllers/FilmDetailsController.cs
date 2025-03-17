using FilmProject.DTO;
using FilmProject.Services.Abstract;
using Microsoft.AspNetCore.Mvc;

namespace FilmProject.Controllers;

[Route("api/[controller]")]
[ApiController]

public class FilmDetailsController : ControllerBase
{
    private readonly IFilmDetailsService _filmDetailsService;

    public FilmDetailsController(IFilmDetailsService filmDetailsService)
    {
        _filmDetailsService = filmDetailsService;
    }

    [HttpPost("GetFilmDetails")]
    public IActionResult AddRating([FromBody] DtoAddRating ratingDto)
    {
        _filmDetailsService.AddRating(ratingDto);
        return Ok("Rating added successfully!");
    }
}