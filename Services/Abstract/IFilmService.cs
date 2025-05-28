using FilmProject.DTO;
using FilmProject.Migrations;
using FilmProject.Models;

public interface IFilmService 
{
    ApiResponse<List<DtoFilteredFilms>> GetAllFilms();
    DtoFilmDetails GetDetailsFiltered(int filmId); //response ekle
    ApiResponse<List<DtoFilteredFilms>> GetFilteredFilms(DtoFilmsQuery dto);
    ApiResponse<string> AddFilm(DtoAddFilm filmDto);
    ApiResponse<string> UpdateFilm(int id, DtoUpdateFilm newFilm);
    ApiResponse<string> DeleteFilm(int id);
    List<Category> GetCategoriesByIds(List<int> categoryIds); 
    List<DtoFilmDetails> GetWatchlist(Guid userId); //response ekle
    bool MarkAsWatched(int filmId, Guid userId); //response ekle
 
}