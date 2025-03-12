namespace FilmProject.Models;

using System.ComponentModel.DataAnnotations;

public class FilmDetails
{
    [Key]
    public Guid Id { get; set; }
    
    [Required]
    public Guid UserId { get; set; }
    
    [Required]
    public int MovieId { get; set; }
    
    [Required]
    [Range(1, 10)]
    public double Rating { get; set; }
    
    public string? Note { get; set; }

    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    
    //User ve Film İlişkilendirmesi şuanda yok bunu eklemenin bir yolunu bul
}