namespace FilmProject.Models
{
    public class ApiResponse<IResponse>
    {
        public string? Error { get; set; }
        public string? Message { get; set; }
        public IResponse Data { get; set; }
    }
}