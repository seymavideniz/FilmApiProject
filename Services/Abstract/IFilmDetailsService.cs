using FilmProject.Models;
using FilmProject.DTO;

namespace FilmProject.Services.Abstract;

public interface IFilmDetailsService
{
    ApiResponse<string> AddFilmDetails(FilmDetails filmDetails);

    ApiResponse<string> AddRating(DtoAddRating ratingDto);
}