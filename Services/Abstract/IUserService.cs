using FilmProject.DTO;
using FilmProject.Models;

namespace FilmProject.Services.Abstract;

public interface IUserService
{
    Task<ApiResponse<string>> SingUpAsync(DtoSignUp dtoSignUp);
    Task<ApiResponse<string>> SignInAsync(DtoSignIn dtoSignIn);
    Task<ApiResponse<string>> UpdateUserAsync(Guid userId, DtoUpdateUser dtoUpdateUser);
}