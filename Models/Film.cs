namespace FilmProject.Models
{
    public class Film
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string Cast { get; set; }
        public string Producer { get; set; }

        public double Rating { get; set; } // update edilemez
        public double Duration { get; set; }
        public DateOnly ReleaseDate { get; set; }

        public int CategoryId { get; set; } // foreign key
        public Category Category { get; set; }
    }
}