using FilmProject.Models;
using FilmProject.Database;
using FilmProject.DTO;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace FilmProject.Services;

public class UserService
{
    private readonly AppDbContext _context;

    public UserService(AppDbContext context)
    {
        _context = context;
    }

    public async Task<string> SingUpAsync(DtoSignUp dtoSignUp)
    {
        var existingUser = await _context.User.FirstOrDefaultAsync(u => u.Email == dtoSignUp.Email);

        if (existingUser != null)
        {
            return "Email already exists";  
        }
        
        var newUser = new User
        {
            FirstName = dtoSignUp.Name,
            LastName = dtoSignUp.LastName,
            Email = dtoSignUp.Email,
            PasswordHash = BCrypt.Net.BCrypt.HashPassword(dtoSignUp.Password),
            CreatedAt = DateTime.UtcNow,
            UpdatedAt = DateTime.UtcNow,
        };

        _context.User.Add(newUser);
        await _context.SaveChangesAsync();

        return "User created successful!";
    }

    public async Task<string> SignInAsync(DtoSignIn dtoSignIn)
    {
        var existingUser = await _context.User.FirstOrDefaultAsync(u => u.Email == dtoSignIn.Email);

        if (existingUser == null)
        {
            return "Email does not exists";
        }

        if (!BCrypt.Net.BCrypt.Verify(dtoSignIn.Password, existingUser.PasswordHash))
        {
            return "Incorrect password";
        }
        return "Sign in successful!";
    }
}