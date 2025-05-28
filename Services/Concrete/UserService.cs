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

    public async Task<RetApi<string>> SingUpAsync(DtoSignUp dtoSignUp)
    {
        try
        {
            var existingUser = await _context.User.FirstOrDefaultAsync(u => u.Email == dtoSignUp.Email);

            if (existingUser != null)
            {
                return new RetApi<string>
                {
                    Error = "Email Exists",
                    Message = "Email already exists",
                    Data = null
                };
            }

            var newUser = new User
            {
                FirstName = dtoSignUp.Name,
                LastName = dtoSignUp.LastName,
                Email = dtoSignUp.Email,
                PasswordHash = BCrypt.Net.BCrypt.HashPassword(dtoSignUp.Password),
                CreatedAt = DateTime.UtcNow,
            };

            _context.User.Add(newUser);
            await _context.SaveChangesAsync();

            return new RetApi<string>
            {
                Error = null,
                Message = "User created successfully!",
                Data = "Success"
            };
        }
        catch (Exception ex)
        {
            return new RetApi<string>
            {
                Error = "Exception",
                Message = ex.Message,
                Data = null
            };
        }
    }


    public async Task<RetApi<string>> SignInAsync(DtoSignIn dtoSignIn)
    {
        var existingUser = await _context.User.FirstOrDefaultAsync(u => u.Email == dtoSignIn.Email);

        if (existingUser == null)
        {
            return new RetApi<string>
            {
                Error = "Email does not exist",
                Message = "Please enter a valid email address.",
                Data = null
            };
        }

        if (!BCrypt.Net.BCrypt.Verify(dtoSignIn.Password, existingUser.PasswordHash))
        {
            return new RetApi<string>
            {
                Error = "Incorrect password",
                Message = "Incorrect password. Please try again.",
                Data = null
            };
        }

        return new RetApi<string>
        {
            Error = null,
            Message = "Sign in successful!",
            Data = "Success"
        };
    }

    public async Task<RetApi<string>> UpdateUserAsync(Guid userId, DtoUpdateUser dtoUpdateUser)
    {
        try
        {
            var user = await _context.User.FindAsync(userId);

            if (user == null)
            {
                return new RetApi<string>
                {
                    Error = "UserNotFound",
                    Message = "User not found.",
                    Data = null
                };
            }

            user.FirstName = dtoUpdateUser.FirstName;
            user.LastName = dtoUpdateUser.LastName;
            user.UpdatedAt = DateTime.UtcNow;

            await _context.SaveChangesAsync();

            return new RetApi<string>
            {
                Error = null,
                Message = "User updated successfully.",
                Data = "Success"
            };
        }
        catch (Exception ex)
        {
            return new RetApi<string>
            {
                Error = "Exception",
                Message = ex.Message,
                Data = null
            };
        }
    }
}