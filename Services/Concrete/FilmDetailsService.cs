using FilmProject.Database;
using FilmProject.Models;
using FilmProject.DTO;
using FilmProject.Services.Abstract;

namespace FilmProject.Services.Concrete;

public class
    FilmDetailsService : IFilmDetailsService
{
    private readonly AppDbContext _context;

    public FilmDetailsService(AppDbContext context)
    {
        _context = context;
    }

    public void AddFilmDetails(FilmDetails filmDetails)
    {
        _context.FilmDetails.Add(filmDetails);
        _context.SaveChanges();
    }


    public void AddRating(DtoAddRating ratingDto)
    {
        var existingRating =
            _context.FilmDetails.FirstOrDefault(f => f.UserId == ratingDto.UserId && f.FilmId == ratingDto.FilmId);

        if (existingRating != null)
        {
            existingRating.Rating = ratingDto.Rating;
            existingRating.Note = ratingDto.Note;
            existingRating.UpdatedAt = DateTime.UtcNow;
        }
        else
        {
            var newRating = new FilmDetails()
            {
                UserId = ratingDto.UserId,
                FilmId = ratingDto.FilmId,
                Rating = ratingDto.Rating,
                Note = ratingDto.Note,
                CreatedAt = DateTime.UtcNow,
            };

            _context.FilmDetails.Add(newRating);
        }

        _context.SaveChanges();
    }
}