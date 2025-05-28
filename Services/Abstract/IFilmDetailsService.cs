using FilmProject.Models;
using FilmProject.DTO;

namespace FilmProject.Services.Abstract;

public interface IFilmDetailsService
{
    RetApi<string> AddFilmDetails(FilmDetails filmDetails);

    RetApi<string> AddRating(DtoAddRating ratingDto);
}