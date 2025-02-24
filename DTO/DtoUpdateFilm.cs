namespace FilmProject.DTO;

public class DtoUpdateFilm
{
    public string Title { get; set; }
    public string Description { get; set; }
    public int Duration { get; set; }
    public DateOnly ReleaseDate { get; set; }
    public  string Cast { get; set; } 
    public string Producer { get; set; } 
    
    public int CategoryId { get; set; }
}