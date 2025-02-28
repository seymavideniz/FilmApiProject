using FilmProject.Enum;

namespace FilmProject.DTO;

public class DtoFilmsQuery
{
    public FilterType FilterType { get; set; } = FilterType.None;
    public int? Year { get; set; }
    public string MovieName { get; set; }
    public int? MinDuration { get; set; }
    public DateOnly? ReleaseDate { get; set; }
    public int Page { get; set; } = 1;
    public int PageSize { get; set; } = 3;
}