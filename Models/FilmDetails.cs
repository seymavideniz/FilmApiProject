namespace FilmProject.Models;

using System.ComponentModel.DataAnnotations;

public class FilmDetails
{
    [Key]
    public Guid Guid { get; set; }
    
    [Required]
    public int UserId { get; set; }
    
    [Required]
    public int MovieId { get; set; }
    
    [Required]
    [Range(1, 10)]
    public double Rating { get; set; }
    
    public string? Note { get; set; }

    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
}