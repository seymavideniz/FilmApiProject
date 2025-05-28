namespace FilmProject.Responses;

public class FailedResponse: IResponse
{
    public string? Error { get; set; }
}