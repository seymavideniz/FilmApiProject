using FilmProject.Database;
using FilmProject.Models;
using FilmProject.DTO;
using FilmProject.Enum;

namespace FilmProject.Services;

public class FilmService
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

    public List<DtoFilteredFilms> GetFilteredFilms(FilterType? filterType = null, int? year = null, string movieName = null, int? minDuration = null,
        DateOnly? releaseDate = null, int pageNumber = 1, int pageSize = 3) //düzelt.
    {
        var query = _context.Films.AsQueryable();

        if (!string.IsNullOrEmpty(movieName))
        {
            query = query.Where(f => f.Title.Contains(movieName));
        }

        if (minDuration.HasValue)
        {
            query = query.Where(f => f.Duration >= minDuration);
        }

        if (filterType.HasValue && year.HasValue) //düzelt.
        {
            if (filterType == FilterType.Before)
            {
                query = query.Where(f => f.ReleaseDate.Year <= year.Value);
            }

            if (filterType == FilterType.After)
            {
                query = query.Where(f => f.ReleaseDate.Year >= year.Value);
            }
        }


        var skipCount = (pageNumber - 1) * pageSize;
        var films = query.Skip(skipCount).Take(pageSize).ToList();

        var filteredFilmsDto = films.Select(f => new DtoFilteredFilms
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

        return filteredFilmsDto;
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