namespace FilmProject.DTO;

public class DtoFilteredFilms
{
    public int FilmId { get; set; }
    public string Title { get; set; }
    public string Description { get; set; }
    public string Producer { get; set; }
    public DateOnly ReleaseDate { get; set; }
    public double Duration { get; set; }
    public int CategoryId { get; set; }
    public string Cast { get; set; }
    public double Rating { get; set; }
}