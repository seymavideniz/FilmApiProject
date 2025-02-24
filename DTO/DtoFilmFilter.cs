namespace FilmProject.DTO;

public class DtoFilmFilter
{
    public string MovieName { get; set; }
    public int? minDuration { get; set; }
    public DateOnly? ReleseYear { get; set; }
    public string ReleaseYearFilter { get; set; }
    public int PageNumber { get; set; } = 1;
    public int PageSize { get; set; } = 3;
}