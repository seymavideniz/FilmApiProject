namespace FilmProject.Models
{
    public class RetApi<T>
    {
        public string Error { get; set; }
        public string Message { get; set; }
        public T Data { get; set; }
    }
}