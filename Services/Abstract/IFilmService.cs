using FilmProject.DTO;
using FilmProject.Migrations;
using FilmProject.Models;

public interface IFilmService
{
    RetApi<List<DtoFilteredFilms>> GetAllFilms();

    //  RetApi<List<DtoFilteredFilms>> GetDetailsFiltered(); eklemeyi unuttum..
    RetApi<List<DtoFilteredFilms>> GetFilteredFilms(DtoFilmsQuery dto);
    RetApi<string> AddFilm(DtoAddFilm filmDto);
    RetApi<string> UpdateFilm(int id, DtoUpdateFilm newFilm);
    RetApi<string> DeleteFilm(int id);
    List<Category> GetCategoriesByIds(List<int> categoryIds);
}