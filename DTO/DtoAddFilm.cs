namespace FilmProject.DTO;

public class DtoAddFilm
{
    public string Title { get; set; }
    public string Description { get; set; }
    public string Cast { get; set; }
    public string Producer { get; set; }
    public double Duration { get; set; }
    public DateOnly ReleaseDate { get; set; }
        
    public int CategoryId { get; set; }  // foreign key
}