using FilmProject.Models;
using FilmProject.DTO;

namespace FilmProject.Services.Abstract;

public interface IFilmDetailsService
{
    void AddFilmDetails(FilmDetails filmDetails);

    void AddRating(DtoAddRating ratingDto);
}