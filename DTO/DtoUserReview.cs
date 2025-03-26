namespace FilmProject.DTO;

public class DtoUserReview
{
    public Guid UserId { get; set; }

    public string UserName { get; set; }

    public double Rating { get; set; }

    public string? Note { get; set; }
}