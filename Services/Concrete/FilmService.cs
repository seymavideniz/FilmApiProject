using FilmProject.Database;
using FilmProject.DTO;
using FilmProject.Enum;
using FilmProject.Models;
using FilmProject.Services.Abstract;

namespace FilmProject.Services.Concrete;

public class FilmService : IFilmService
{
    private readonly AppDbContext _context;

    public FilmService(AppDbContext context)
    {
        _context = context;
    }

    public List<DtoFilteredFilms> GetAllFilms()
    {
        return _context.Films.Select(f => new DtoFilteredFilms
        {
            FilmId = f.Id,
            Title = f.Title,
            Description = f.Description,
            Producer = f.Producer,
            ReleaseDate = f.ReleaseDate,
            Duration = f.Duration,
            CategoryId = f.CategoryId,
            Cast = f.Cast,
            Rating = f.Rating
        }).ToList();
    }

    public List<DtoFilteredFilms> GetFilteredFilms(DtoFilmsQuery dto)
    {
        var query = _context.Films.AsQueryable();

        if (!string.IsNullOrEmpty(dto.MovieName))
        {
            query = query.Where(f => f.Title.Contains(dto.MovieName));
        }

        if (dto.MinDuration.HasValue)
        {
            query = query.Where(f => f.Duration >= dto.MinDuration.Value);
        }

        if (dto.FilterType == FilterType.Before)
        {
            query = query.Where(f => f.ReleaseDate.Year <= dto.Year);
        }
        else if (dto.FilterType == FilterType.After)
        {
            query = query.Where(f => f.ReleaseDate.Year >= dto.Year);
        }

        if (!string.IsNullOrEmpty(dto.MovieName))
        {
            query = query.OrderBy(f => f.Title.IndexOf(dto.MovieName, StringComparison.OrdinalIgnoreCase)); //index of vedalaşalım artık
        }
        else
        {
            query = query.OrderBy(f => f.Title);
        }

        var skipCount = (dto.Page - 1) * dto.PageSize;
        var films = query.Skip(skipCount).Take(dto.PageSize).ToList();


        return films.Select(f => new DtoFilteredFilms
        {
            FilmId = f.Id,
            Title = f.Title,
            Description = f.Description,
            Producer = f.Producer,
            ReleaseDate = f.ReleaseDate,
            Duration = f.Duration,
            CategoryId = f.CategoryId,
            Cast = f.Cast,
            Rating = f.Rating
        }).ToList();
    }


    public void AddFilm(DtoAddFilm filmDto)
    {
        var film = new Film
        {
            Title = filmDto.Title,
            Description = filmDto.Description,
            CategoryId = filmDto.CategoryId,
            Producer = filmDto.Producer,
            ReleaseDate = filmDto.ReleaseDate,
            Rating = 0,
            Cast = filmDto.Cast,
            Duration = filmDto.Duration,
        };

        _context.Films.Add(film);
        _context.SaveChanges();
    }

    public void UpdateFilm(int id, DtoUpdateFilm newFilm)
    {
        var filmToUpdate = _context.Films.Find(id);
        if (filmToUpdate == null)
        {
            return;
        }

        filmToUpdate.Title = newFilm.Title;
        filmToUpdate.Description = newFilm.Description;
        filmToUpdate.Duration = newFilm.Duration;
        filmToUpdate.ReleaseDate = newFilm.ReleaseDate;
        filmToUpdate.Cast = newFilm.Cast;
        filmToUpdate.Producer = newFilm.Producer;

        filmToUpdate.CategoryId = newFilm.CategoryId;

        _context.Update(filmToUpdate);
        _context.SaveChanges();
    }

    public void DeleteFilm(int id)
    {
        var film = _context.Films.Find(id);
        if (film == null)
        {
            return;
        }

        _context.Films.Remove(film);
        _context.SaveChanges();
    }


    public List<Category> GetCategoriesByIds(List<int> categoryIds)
    {
        return _context.Categories.Where(c => categoryIds.Contains(c.CategoryId)).ToList();
    }
}