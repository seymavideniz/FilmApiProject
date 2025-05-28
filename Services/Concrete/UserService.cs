using FilmProject.Database;
using FilmProject.DTO;
using FilmProject.Models;
using FilmProject.Services.Abstract;
using Microsoft.EntityFrameworkCore;

namespace FilmProject.Services.Concrete;

public class UserService : IUserService
{
    private readonly AppDbContext _context;

    public UserService(AppDbContext context)
    {
        _context = context;
    }

    public async Task<User?> SignUpAsync(DtoSignUp dtoSignUp)
    {
        var existingUser = await _context.User.FirstOrDefaultAsync(u => u.Email == dtoSignUp.Email);
        if (existingUser != null)
        {
            return null; 
        }

        var newUser = new User
        {
            FirstName = dtoSignUp.Name,
            LastName = dtoSignUp.LastName,
            UserName = dtoSignUp.UserName,
            Email = dtoSignUp.Email,
            PasswordHash = BCrypt.Net.BCrypt.HashPassword(dtoSignUp.Password),
            CreatedAt = DateTime.UtcNow,
        };

        _context.User.Add(newUser);
        await _context.SaveChangesAsync();

        return newUser;
    }

    public Task<ApiResponse<string>> SingUpAsync(DtoSignUp dtoSignUp)
    {
        throw new NotImplementedException();
    }

    Task<ApiResponse<string>> IUserService.SignInAsync(DtoSignIn dtoSignIn)
    {
        throw new NotImplementedException();
    }

    Task<ApiResponse<string>> IUserService.UpdateUserAsync(Guid userId, DtoUpdateUser dtoUpdateUser)
    {
        throw new NotImplementedException();
    }

    public async Task<User?> SignInAsync(DtoSignIn dtoSignIn)
    {
        var user = await _context.User.FirstOrDefaultAsync(u => u.Email == dtoSignIn.Email);
        if (user == null) return null;

        bool passwordValid = BCrypt.Net.BCrypt.Verify(dtoSignIn.Password, user.PasswordHash);
        if (!passwordValid) return null;

        return user;
    }


    public async Task<bool> UpdateUserAsync(Guid userId, DtoUpdateUser dtoUpdateUser)
    {
        var user = await _context.User.FindAsync(userId);
        if (user == null) return false;

        user.FirstName = dtoUpdateUser.FirstName;
        user.LastName = dtoUpdateUser.LastName;
        user.UpdatedAt = DateTime.UtcNow;

        await _context.SaveChangesAsync();

        return true;
    }
}