using FilmProject.Database;
using FilmProject.DTO;
using FilmProject.Enum;
using FilmProject.Models;
using Microsoft.EntityFrameworkCore;

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
            FilmId = f.FilmID,
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

        if (dto.MinDuration.HasValue)
        {
            query = query.Where(f => f.Duration >= dto.MinDuration.Value);
        }

        if (dto.FilterType == FilterType.Before)
        {
            query = query.Where(f => f.ReleaseDate <= dto.ReleaseDate);
        }
        else if (dto.FilterType == FilterType.After)
        {
            query = query.Where(f => f.ReleaseDate >= dto.ReleaseDate);
        }

        if (dto.SortByMovieName)
        {
            query = query.OrderBy(f => f.Title);
        }

        int skipCount = (dto.Page - 1) * dto.PageSize;
        var films = query.Skip(skipCount).Take(dto.PageSize).ToList();

        return films.Select(f => new DtoFilteredFilms
        {
            FilmId = f.FilmID,
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

    public DtoFilmDetails GetDetailsFiltered(int filmId)
    {
        var film = _context.Films
            .Include(f => f.FilmDetails)
            .ThenInclude(fd => fd.User)
            .FirstOrDefault(f => f.FilmID == filmId);

        if (film == null)
        {
            throw new Exception("Film not found");
        }

        double averageRating = film.FilmDetails.Any()
            ? film.FilmDetails.Average(r => r.Rating)
            : 0;

        var userReviews = film.FilmDetails.Select(r => new DtoUserReview
        {
            UserName = r.User.UserName,
            Rating = r.Rating,
            Note = r.Note
        }).ToList();

        return new DtoFilmDetails
        {
            FilmId = film.FilmID,
            Title = film.Title,
            Description = film.Description,
            Cast = film.Cast,
            Producer = film.Producer,
            AvgRating = averageRating,
            Duration = film.Duration,
            ReleaseDate = film.ReleaseDate,
            CategoryId = film.CategoryId,
            UserReviews = userReviews
        };
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
            Duration = filmDto.Duration
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
    
    public bool MarkAsWatched(int filmId, Guid userId)
    {
        var filmDetail = _context.FilmDetails
            .FirstOrDefault(fd => fd.FilmId == filmId && fd.UserId == userId);
    
        if (filmDetail != null)
        {
            filmDetail.Watched = true;
            filmDetail.UpdatedAt = DateTime.UtcNow;

            _context.SaveChanges();
            return true;
        }
        else // else gereksiz zaten yukarıda return var ve film detail null gelebilir bu göz önünde bulundurulmalı
        {
            return false;
        }
    }
    public List<DtoFilmDetails> GetWatchlist(Guid userId)
    {
        var watchlist = _context.FilmDetails
            .Where(fd => fd.UserId == userId && fd.Watched == true)
            .Select(fd => new DtoFilmDetails
            {
                FilmId = fd.Film.FilmID,
                Title = fd.Film.Title,
                Description = fd.Film.Description,
                AvgRating = fd.Film.FilmDetails.Any() 
                    ? fd.Film.FilmDetails.Average(r => r.Rating) 
                    : 0,
                CategoryId = fd.Film.CategoryId,
                Cast = fd.Film.Cast,
                Producer = fd.Film.Producer,
                Duration = fd.Film.Duration,
                ReleaseDate = fd.Film.ReleaseDate
            }).ToList();

        return watchlist;
    }
}