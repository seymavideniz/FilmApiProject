namespace FilmProject.Models;

using System.ComponentModel.DataAnnotations;

public class User
{
    [Key] 
    public Guid ID { get; set; }

    [Required] 
    public string FirstName { get; set; }

    [Required] 
    public string LastName { get; set; }

    [Required] 
    [EmailAddress] 
    public string Email { get; set; }

    [Required] 
    public string PasswordHash { get; set; }

    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

    public DateTime UpdatedAt { get; set; }
    
    public FilmDetails Ratings { get; set; }
}