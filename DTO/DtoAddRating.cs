using System.ComponentModel.DataAnnotations;

namespace FilmProject.DTO;

public class DtoAddRating
{
    [Required] 
    public Guid UserId { get; set; }
    
    [Required] 
    public int FimId { get; set; }
    
    [Required] 
    [Range(1, 10)]
    public double Rating { get; set; }
    
    public string? Note { get; set; }
}