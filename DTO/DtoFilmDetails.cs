namespace FilmProject.DTO;

public class DtoFilmDetails
{
    public int FilmId { get; set; }
    public string Title { get; set; }
    public string Description { get; set; }
    public double AvgRating { get; set; }
    public List<DtoUserReview> UserReviews { get; set; }
    public int CategoryId { get; set; }
    public string Cast { get; set; }
    public string Producer { get; set; }
    public double Duration { get; set; }
    public DateOnly ReleaseDate { get; set; }
}