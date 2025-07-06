using FilmProject.DTO;
using FilmProject.Models;

public interface IFilmService
{
    List<DtoFilteredFilms> GetAllFilms();
    List<DtoFilteredFilms> GetFilteredFilms(DtoFilmsQuery dto);
    void AddFilm(DtoAddFilm filmDto);
    void UpdateFilm(int id, DtoUpdateFilm newFilm);
    void DeleteFilm(int id);
    List<Category> GetCategoriesByIds(List<int> categoryIds);
}